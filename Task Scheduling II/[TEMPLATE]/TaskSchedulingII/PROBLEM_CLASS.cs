using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE 

        enum SOLUTION_TYPE { NAIVE, EFFICIENT };
        static SOLUTION_TYPE solType = SOLUTION_TYPE.EFFICIENT;

        //Your Code is Here:
        //==================
        /// <summary>
        /// Given the N tasks, design an efficient solution to schedule them to minimize the average completion time of these processes.
        /// </summary>
        /// <param name="r">release time of each process</param>
        /// <param name="p">processing time of each process</param>
        /// <returns>min average completion time</returns>
        static public double RequiredFunction(int[] r, int[] p)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            if (r == null || p == null || r.Length == 0 || p.Length == 0)
                return 0.0;
            int ArrayLength = r.Length;
            List<Tuple<int, int, int>> allTasks = new List<Tuple<int, int, int>>();
            int[] completionTime = new int[ArrayLength];
            for (int k = 0; k < ArrayLength; k++)
            {
                allTasks.Add(new Tuple<int, int, int>(r[k], p[k], k));
            }

            //Sort task tuples based on release time ascendengly
            // Define the comparison method explicitly
            int taskComparison(Tuple<int, int, int> firstTask, Tuple<int, int, int> secondTask)
            {
                return firstTask.Item1.CompareTo(secondTask.Item1); // Compare by release time (Item1)
            }

            // Sort the list using the customized comparer
            allTasks.Sort(taskComparison);

            //Use the sorted set to apply the priority queue but with O(1) searching time
            //It compares the tuples by the processing time and if they are equal then it compares them by release time
            var shoretestTimePQ = new SortedSet<Tuple<int, int, int>>(
                Comparer<Tuple<int, int, int>>.Create((T1, T2) =>
                {
                    //Item1 = Remaining processing time
                    //Item2 = Original task index 
                    //Item3 = Release time 
                    if (T1.Item1 != T2.Item1)
                        return T1.Item1.CompareTo(T2.Item1); // Compare remaining time
                    else if (T1.Item3 != T2.Item3)
                        return T1.Item3.CompareTo(T2.Item3); // Compare release time if remaining time is equal
                    else
                        return T1.Item2.CompareTo(T2.Item2); // Compare original index if both are equal
                }));
            int timeDelwa2ty = 0;
            int taskindex = 0;
            // while there are tasks not released yet OR the pq still has task not finished
            while (shoretestTimePQ.Count > 0 || taskindex < ArrayLength)
            {
                // Add available tasks till now to the priority queue
                while (taskindex < ArrayLength && allTasks[taskindex].Item1 <= timeDelwa2ty)
                {
                    shoretestTimePQ.Add(new Tuple<int, int, int>(allTasks[taskindex].Item2, allTasks[taskindex].Item3, allTasks[taskindex].Item1));
                    taskindex++;
                }

                //Handles the idle time when No tasks are available
                if (shoretestTimePQ.Count == 0)
                {
                    timeDelwa2ty = allTasks[taskindex].Item1;
                }
                else
                {
                    var minProcTask = shoretestTimePQ.Min;
                    shoretestTimePQ.Remove(minProcTask);

                    //Accessing the elements of the minimum process tuple
                    int processingTime = minProcTask.Item1;
                    int minProcIndex = minProcTask.Item2;
                    int minProcrelease = minProcTask.Item3;

                    //This context will handle unnecessary preemptions
                    int nextArrival;
                    if (taskindex < ArrayLength)
                    {
                        nextArrival = allTasks[taskindex].Item1;
                    }
                    else
                    {
                        nextArrival = int.MaxValue;
                    }

                    if (nextArrival >= timeDelwa2ty + processingTime)
                    {
                        // The task will complete processing as no process came
                        timeDelwa2ty = timeDelwa2ty + processingTime;
                        completionTime[minProcIndex] = timeDelwa2ty;
                    }
                    else
                    {
                        //If a new process with a min processing time come, then preemption is needed
                        int executedTime = nextArrival - timeDelwa2ty;
                        int remainingProcTime = processingTime - executedTime;

                        shoretestTimePQ.Add(new Tuple<int, int, int>(remainingProcTime, minProcIndex, minProcrelease));

                        // Execute the new minimum process
                        timeDelwa2ty = nextArrival;
                    }
                }

            }
            long totalFinishingTime = 0;
            int index = 0;
            while (index < completionTime.Length)
            {
                //Calculating the total processing and waiting time 
                totalFinishingTime += completionTime[index];
                index++;
            }
            //Rounding the average value to the nearest hundredth
            return Math.Round(totalFinishingTime / (double)ArrayLength, 2);
        } 
        #endregion
    }
}
