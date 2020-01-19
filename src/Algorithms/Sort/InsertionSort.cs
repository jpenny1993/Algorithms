using System;
using System.Collections.Generic;

namespace Algorithms.Sort
{
    public class InsertionSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void SortAscending(ref IList<T> source) => InsertionSort(ref source, 1);

        public void SortDescending(ref IList<T> source) => InsertionSort(ref source, -1);

        // TODO: Implement Direction
        public static void InsertionSort(ref IList<T> source, int direction) 
        {
            T element;
            int j;

            // Iterate though every element in the list
            for (var x = 1; x < source.Count; x++)
            {
                element = source[x];
                j = x - 1;

                // Move all larger elements past the current element
                while (j >= 0 && source[j].CompareTo(element) == direction)
                {
                    source[j + 1] = source[j];
                    j--;
                }

                // Insert the current element
                source[j + 1] = element;
            }
        }
    }
}
