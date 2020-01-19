using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sort
{
    public class BubbleSorter<T> : ISorter<T> where T : struct, IComparable<T>
    {
        public IList<T> Sort(IEnumerable<T> source)
        {
            IList<T> collection = source.ToArray();
            Sort(ref collection);
            return collection;
        }

        public void Sort(ref IList<T> source) => BubbleSort(ref source, source.Count);

        public void BubbleSort(ref IList<T> source, int length) 
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
                    if (source[sortIndex].CompareTo(source[sortIndex + 1]) > 0)
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
