using System;
using System.Collections.Generic;

namespace Algorithms.Sort
{
    public class SelectionSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void SortAscending(ref IList<T> source) => SelectionSort(ref source, source.Count, 1);

        public void SortDescending(ref IList<T> source) => SelectionSort(ref source, source.Count, -1);

        // TODO: Implement Direction
        public static void SelectionSort(ref IList<T> source, int length, int direction) 
        {
            T temp;
            int minIndex;

            // Iterate through every element in the list
            for (var index = 0; index < length; index++)
            {
                minIndex = index;

                // Find the smallest element in the unsorted portion of the array.
                for (var sortIndex = index; sortIndex < length; sortIndex++)
                {
                    if (source[minIndex].CompareTo(source[sortIndex]) == direction) 
                    {
                        minIndex = sortIndex;
                    }
                }

                // Swap the smallest element with the current element.
                if (minIndex != index)
                {
                    temp = source[index];
                    source[index] = source[minIndex];
                    source[minIndex] = temp;
                }
            }
        }
    }
}
