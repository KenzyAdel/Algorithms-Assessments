using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        //Your Code is Here:
        //==================
        /// <summary>
        /// Quick Insertion Sort: to speedup quicksort is to simply not sort arrays of size less than or equal certain threshold, 
        /// then use insertion sort on the entire array
        /// </summary>
        /// <param name="numbers">Array to be sorted</param>
        /// <param name="N">The array size</param>
        /// <param name="threshold">The quick insertion threshold at which to stop the quicksort</param>
        /// <returns>The sorted array</returns>
        static public float[] RequiredFunction(float[] numbers, int N, int threshold)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            if (N <= 1) return numbers;
            void Swap(float[] sortArray, int firstValueIndex, int secondValueIndex)
            {
                //This condition is to handle when the rightPtr is equal to the pivot value index, so no need to apply the function logic
                if (firstValueIndex == secondValueIndex) return;
                float temp = sortArray[firstValueIndex];
                sortArray[firstValueIndex] = sortArray[secondValueIndex];
                sortArray[secondValueIndex] = temp;
            }
            int Partition(float[] sortArray, int firstElement, int lastElement)
            {

                int midIndex = firstElement + (lastElement - firstElement) / 2;

                if (sortArray[firstElement] > sortArray[lastElement])
                    Swap(sortArray, firstElement, lastElement);

                if (sortArray[midIndex] > sortArray[lastElement])
                    Swap(sortArray, midIndex, lastElement);

                //Now the pivot value is the midean of the array
                Swap(sortArray, midIndex, firstElement);

                float PivotValue = sortArray[firstElement];
                int leftPtr = firstElement + 1;
                int rightPtr = lastElement;

                while (true)
                {
                    while (leftPtr <= rightPtr && sortArray[leftPtr] <= PivotValue)
                    {
                        leftPtr++;
                    }
                    while (rightPtr >= leftPtr && sortArray[rightPtr] >= PivotValue)
                    {
                        rightPtr--;
                    }
                    if (rightPtr < leftPtr)
                    {
                        break;
                    }
                    else
                    {
                        Swap(sortArray, leftPtr, rightPtr);
                    }
                }
                Swap(sortArray, firstElement, rightPtr);
                return rightPtr;
            }
            if (threshold > 1)
            {
                void Insertion_Sort(float[] sortArray, int leftptr, int rightptr)
                {
                    for (int currentIndex = leftptr + 1; currentIndex <= rightptr; currentIndex++)
                    {
                        float key = sortArray[currentIndex];
                        int correctKeyIndex = currentIndex - 1;
                        while (correctKeyIndex >= leftptr && sortArray[correctKeyIndex] > key)
                        {
                            sortArray[correctKeyIndex + 1] = sortArray[correctKeyIndex];
                            correctKeyIndex--;
                        }
                        sortArray[correctKeyIndex + 1] = key;
                    }

                }
                void Quick_Insertion_Sort(float[] sortArray, int leftptr, int rightptr, int thr)
                {

                    if (rightptr - leftptr + 1 <= threshold)
                    {
                        Insertion_Sort(sortArray, leftptr, rightptr);
                        return;
                    }
                    if (leftptr < rightptr)
                    {
                        int correctPivotIndex = Partition(sortArray, leftptr, rightptr);
                        Quick_Insertion_Sort(sortArray, leftptr, correctPivotIndex - 1, threshold);
                        Quick_Insertion_Sort(sortArray, correctPivotIndex + 1, rightptr, threshold);
                    }
                }
                Quick_Insertion_Sort(numbers, 0, N - 1, threshold);
            }
            else
            {
                void Quick_sort(float[] sortArray, int leftptr, int rightptr)
                {
                    if (leftptr < rightptr)
                    {
                        int correctPivotIndex = Partition(sortArray, leftptr, rightptr);
                        Quick_sort(sortArray, leftptr, correctPivotIndex - 1);
                        Quick_sort(sortArray, correctPivotIndex + 1, rightptr);
                    }
                }
                Quick_sort(numbers, 0, N - 1);
            }
            return numbers;
        }
        #endregion
    }
}
