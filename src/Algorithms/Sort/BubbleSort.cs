using System;
using System.Collections.Generic;

namespace Algorithms.Sort
{
    public class BubbleSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void SortAscending(ref IList<T> source) => BubbleSort(ref source, source.Count, 1);

        public void SortDescending(ref IList<T> source) => BubbleSort(ref source, source.Count, -1);

        private static void BubbleSort(ref IList<T> source, int length, int direction) 
        {
            T temp;
            int sortIndex, sortSize = length - 1, lastSortedIndex = 0;

            // Iterate though every element in the list
            for (var index = 0; index < length - 1; index++)
            {
                // Compare every unsorted element to the one next to it
                for (sortIndex = 0; sortIndex < sortSize; sortIndex++)
                {
                    // If the elements are not in order
                    if (source[sortIndex].CompareTo(source[sortIndex + 1]) == direction)
                    {
                        // Swap the two elements
                        temp = source[sortIndex];
                        source[sortIndex] = source[sortIndex + 1];
                        source[sortIndex + 1] = temp;

                        // Mark the element as sorted
                        lastSortedIndex = sortIndex;
                    }
                }

                // Reduce the size of the next sort iteration 
                // by the elements that are already correctly sorted
                sortSize = lastSortedIndex;
            }
        }
    }
}
