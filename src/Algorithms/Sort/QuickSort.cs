using System;
using System.Collections.Generic;

namespace Algorithms.Sort
{
    public class QuickSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void SortAscending(ref IList<T> source) => QuickSort(ref source, 0, source.Count - 1, 1);

        public void SortDescending(ref IList<T> source) => QuickSort(ref source, 0, source.Count - 1, -1);

        private static void QuickSort(ref IList<T> source, int low, int high, int direction)
        {
            if (low < high)
            {
                // Pick a pivot point
                var pivotIndex = Partition(ref source, low, high, direction);

                // Sort the elements to the left of the pivot
                QuickSort(ref source, low, pivotIndex - 1, direction);
                
                // Sort the elements to the right of the pivot
                QuickSort(ref source, pivotIndex + 1, high, direction);
            }
        }

        private static int Partition(ref IList<T> source, int low, int high, int direction)
        {
            // Start the pivot at the edge of the collection
            var pivot = source[high];
            var left = low;
            T temp;

            for (var index = low; index < high; index++)
            {
                // If the element is right of the pivot
                if (pivot.CompareTo(source[index]) == direction)
                {
                    // Swap the posiition of the elements
                    temp = source[left];
                    source[left] = source[index];
                    source[index] = temp;
                    left++;
                }
            }

            // Swap the posiition of the pivot and the next pivot
            temp = source[left];
            source[left] = source[high];
            source[high] = temp;

            // Return the next pivot point
            return left;
        }
    }
}
