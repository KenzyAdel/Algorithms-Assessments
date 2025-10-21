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
        public override string ProblemName { get { return "SchoolQuizVI"; } }

        public override void TryMyCode()
        {
            /* WRITE 4~6 DIFFERENT CASES FOR TRACE, EACH WITH
             * 1) SMALL INPUT SIZE
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT FROM THE FUNCTION
             * 4) PRINT THE CASE 
             */
            int N = 0;
            int output, expected;

            // 0 combinations
            N = 5;
            int[] arr1 = { 0, 3, 7 };
            expected = 0;
            output = PROBLEM_CLASS.RequiredFunction(N, arr1);
            PrintCase(N, arr1, output, expected);

            // 1 combinations
            N = 5;
            int[] arr2 = { 0, 1, 4, 3 };
            expected = 1;
            output = PROBLEM_CLASS.RequiredFunction(N, arr2);
            PrintCase(N, arr2, output, expected);

            // 1 combination
            N = 6;
            int[] arr3 = { 0, 1, 2, 3 };
            expected = 1;
            output = PROBLEM_CLASS.RequiredFunction(N, arr3);
            PrintCase(N, arr3, output, expected);

            // 3 combinations
            N = 18;
            int[] arr4 = { 0, 14, 7, 12, 21, 8, 6, 4, 11 };
            expected = 4;
            output = PROBLEM_CLASS.RequiredFunction(N, arr4);
            PrintCase(N, arr4, output, expected);

            // 2 combinations
            N = 10;
            int[] arr5 = { 0, 2, 13, 6, 8, 4 };
            expected = 2;
            output = PROBLEM_CLASS.RequiredFunction(N, arr5);
            PrintCase(N, arr5, output, expected);
        }

        Thread tstCaseThr;
        bool caseTimedOut;
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
            int K = 0;
            int[] arr = null;
            int output, actualResult;

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
                K = br.ReadInt32();
                arr = new int[K + 1];
                for (int j = 0; j <= K; j++)
                {
                    arr[j] = br.ReadInt32();
                }
                actualResult = br.ReadInt32();

                //Console.WriteLine("Reading: N = {0}, K = {1}, Res = {2}", N, K, actualResult);
                output = 0;
                caseTimedOut = true;
                caseException = false;
                Stopwatch sw = null;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            int sum = 0;
                            int numOfRep = 1;
                            PROBLEM_CLASS.solType = PROBLEM_CLASS.SOLUTION_TYPE.EFFICIENT;
                            sw = Stopwatch.StartNew();
                            for (int x = 0; x < numOfRep; x++)
                                output = PROBLEM_CLASS.RequiredFunction(N, arr);
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
                        timeOutInMillisec = 1000; //Large Value 
                    }
                    /*=========================================================*/
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }
                Console.WriteLine($"CASE#{i}: N = {N}, K = {K}, output = {output}, expected = {actualResult}, time in ms = {sw.ElapsedMilliseconds}, timeout = {timeOutInMillisec}");

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
                    if (level == HardniessLevel.Easy)
                    {
                        PrintCase(N, arr, output, actualResult);
                    }
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
        private static void PrintCase(int N, int[] arr, int output, int expected)
        {
            /* PRINT THE FOLLOWING
             * 1) INPUT
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT
             * 4) WHETHER IT'S CORRECT OR WRONG
             * */
            Console.WriteLine("N: {0}", N);

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Output = " + output);
            Console.WriteLine("Expected = " + expected);
            if (output == expected)
                Console.WriteLine("CORRECT");
            else
                Console.WriteLine("WRONG");
            Console.WriteLine();
        }

        
        #endregion

    }
}
