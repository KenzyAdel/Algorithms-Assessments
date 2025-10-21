using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "TaskSchedulingII"; } }

        public override void TryMyCode()
        {
            /* WRITE 4~6 DIFFERENT CASES FOR TRACE, EACH WITH
             * 1) SMALL INPUT SIZE
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT FROM THE FUNCTION
             * 4) PRINT THE CASE 
             */
            int N;
            double output, expected;

            //First case\
            N = 3;
            int[] r1 = { 0, 2, 4 };
            int[] p1 = { 5, 2, 3 };
            expected = 7;
            output = PROBLEM_CLASS.RequiredFunction((int[])r1.Clone(), (int[])p1.Clone());
            PrintCase(N, r1, p1, output, expected);

            //Second case
            N = 4;
            int[] r2 = { 0, 2, 3, 5 };
            int[] p2 = { 10, 2, 1, 3 };
            expected = 8.25f;
            output = PROBLEM_CLASS.RequiredFunction((int[])r2.Clone(), (int[])p2.Clone());
            PrintCase(N, r2, p2, output, expected);

            //Third case
            N = 4;
            int[] r3 = { 0, 1, 2, 3 };
            int[] p3 = { 4, 4, 4, 4 };
            expected = 10;
            output = PROBLEM_CLASS.RequiredFunction((int[])r3.Clone(), (int[])p3.Clone());
            PrintCase(N, r3, p3, output, expected);

            //Fourth case
            N = 6;
            int[] r4 = { 0, 2, 3, 5, 6, 7 };
            int[] p4 = { 10, 4, 1, 3, 2, 5 };
            expected = 12.33f;
            output = PROBLEM_CLASS.RequiredFunction((int[])r4.Clone(), (int[])p4.Clone());
            PrintCase(N, r4, p4, output, expected);

            //Fifth case
            N = 7;
            int[] r5 = { 0, 1, 2, 4, 6, 7, 9 };
            int[] p5 = { 3, 6, 4, 1, 2, 5, 3 };
            expected = 11.57f;
            output = PROBLEM_CLASS.RequiredFunction((int[])r5.Clone(), (int[])p5.Clone());
            PrintCase(N, r5, p5, output, expected);
        }

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            /* READ THE TEST CASES FROM THE SPECIFIED FILE, FOR EACH CASE DO:
             * 1) READ ITS INPUT & EXPECTED OUTPUT
             * 2) READ ITS EXPECTED TIMEOUT LIMIT (IF ANY)
             * 3) CALL THE FUNCTION ON THE GIVEN INPUT USING THREAD WITH THE GIVEN TIMEOUT 
             * 4) CHECK THE OUTPUT WITH THE EXPECTED ONE
             */

            int testCases;
            int N = 0;
            int[] r = null;
            int[] p = null;
            double output, actualResult;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);

            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
            bool readTimeFromFile = false;
            if (timeOutInMillisec == -1)
            {
                readTimeFromFile = true;
            }
            int i = 1;
            while (testCases-- > 0)
            {
                N = br.ReadInt32();
                r = new int[N];
                p = new int[N];
                for (int j = 0; j < N; j++)
                {
                    r[j] = br.ReadInt32();
                    p[j] = br.ReadInt32();
                }
                actualResult = br.ReadDouble();

                output = 0;
                caseTimedOut = true;
                caseException = false;
                Stopwatch sw = null;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            double sum = 0;
                            int numOfRep = 2;
                            sw = Stopwatch.StartNew();
                            for (int x = 0; x < numOfRep; x++)
                            {
                                output = PROBLEM_CLASS.RequiredFunction((int[])r.Clone(), (int[])p.Clone());
                                //output = GetMinimumAverageCompletionTimeBruteForce((int[])r.Clone(), (int[])p.Clone());
                            }
                            sw.Stop();
                        }
                        catch
                        {
                            caseException = true;
                            output = int.MinValue;
                        }
                        caseTimedOut = false;
                    });

                    if (readTimeFromFile)
                    {
                        timeOutInMillisec = br.ReadInt32();
                    }
                    /*LARGE TIMEOUT FOR SAMPLE CASES TO ENSURE CORRECTNESS ONLY*/
                    if (level == HardniessLevel.Easy)
                    {
                        timeOutInMillisec = 100; //Large Value 
                    }
                    /*=========================================================*/
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }
                Console.WriteLine($"N = {r.Length}, time in ms = {sw.ElapsedMilliseconds}, timeOutInMillisec = {timeOutInMillisec}");

                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
                    tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }
                else if (output == actualResult)    //Passed
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    wrongCases++;
                }

                i++;
            }
            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0));
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        /// <summary>
        /// Generate a file of test cases according to the specified params
        /// </summary>
        /// <param name="level">Easy or Hard</param>
        /// <param name="numOfCases">Required number of cases</param>
        /// <param name="includeTimeInFile">specify whether to include the expected time for each case in the file or not</param>
        /// <param name="timeFactor">factor to be multiplied by the actual time</param>
        public override void GenerateTestCases(HardniessLevel level, int numOfCases, bool includeTimeInFile = false, float timeFactor = 1)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private static void PrintCase(int N, int[] arr1, int[] arr2, double output, double expected)
        {
            /* PRINT THE FOLLOWING
             * 1) INPUT
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT
             * 4) WHETHER IT'S CORRECT OR WRONG
             * */

            Console.WriteLine("N: {0}", N);
            Console.WriteLine("Release times: ");
            for (int i = 0; i < N; i++)
            {
                Console.Write(arr1[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Processing times: ");
            for (int j = 0; j < N; j++)
            {
                Console.Write(arr2[j] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Output = " + output);
            Console.WriteLine("Expected = " + Math.Round(expected, 2));
            if (output == Math.Round(expected, 2))
                Console.WriteLine("CORRECT");
            else
                Console.WriteLine("WRONG");
            Console.WriteLine();
        }

        #endregion

    }
}
