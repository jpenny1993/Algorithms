using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sort
{
    public class SelectionSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IEnumerable<T> source)
        {
            IList<T> collection = source.ToArray();
            Sort(ref collection);
            return collection;
        }

        public void Sort(ref IList<T> source) => SelectionSort(ref source, source.Count);

        public void SelectionSort(ref IList<T> source, int length) 
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
                    if (source[minIndex].CompareTo(source[sortIndex]) > 0) 
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
