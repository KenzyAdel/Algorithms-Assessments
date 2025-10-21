using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class PROBLEM_CLASS
    {
        #region YOUR CODE IS HERE 

        public enum SOLUTION_TYPE { NAIVE, EFFICIENT };
        public static SOLUTION_TYPE solType = SOLUTION_TYPE.EFFICIENT;

        //Your Code is Here:
        //==================
        /// <summary>
        /// find the TOTAL NUMBER of different ways that numbers can be combined from K 'numbers' to be equal to ‘N’ 
        /// </summary>
        /// <param name="N">target number</param>
        /// <param name="numbers">array of possible numbers to be used [1-based]</param>
        /// <returns>total number of combinations from "numbers" that sum-up to "N"</returns>
        static public int RequiredFunction(int N, int[] numbers)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            
            // As we will process almost all numbers we need to use a bottom-up approach
            int size = numbers.Length - 1;

            //DP_array will include the number of solutions that sums from 0 to N 
            int[] DP_array = new int[N + 1];

            //This case means that we need the sum = 0 and this will happen if we take no elements (BASE CASE)
            DP_array[0] = 1;

            bool awlRakam = true;

            foreach (int elemValue in numbers)
            {
                // The first element will be skipped to make the logic 1_BASED
                switch (awlRakam)
                {
                    case false:
                    {
                        // The current number will be skipped if it's > the desired sum
                        if (elemValue > N) { continue;}

                        /* Create a temporary array that clones the dp array to avoid overwriting the original DP_array unnecassarily
                            - If we updated dp directly in the loop, this will lead to permutations like[1, 2] and[2, 1] being counted as separate 
                            which will lead to overcounting. we just care about the combination itself but order isn't a matter.*/

                        int[] tempArray = new int[N + 1];
                        tempArray = (int[])DP_array.Clone();

                        int current = elemValue; 
                        do
                        {
                            tempArray[current] += DP_array[current - elemValue];
                            current++;
                        } while (current <= N); 
                        
                        DP_array = tempArray;
                        break;
                    }
                    case true:
                    {
                        awlRakam = false;
                        break;
                    }
                } 
            }
            return DP_array[N] ;

            // The total complexity is O(N × K) where k =  Number of elements in numbers array without the first element
        }
        #endregion
    }
}
