using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sort
{
    public class InsertionSorter<T> : ISorter<T> where T : struct, IComparable<T>
    {
        public IList<T> Sort(IEnumerable<T> source)
        {
            IList<T> collection = source.ToArray();
            Sort(ref collection);
            return collection;
        }

        public void Sort(ref IList<T> source) => InsertionSort(ref source);

        public void InsertionSort(ref IList<T> source) 
        {
            T element;
            int j;

            // Iterate though every element in the list
            for (var x = 1; x < source.Count; x++)
            {
                element = source[x];
                j = x - 1;

                // Move all larger elements past the current element
                while (j >= 0 && source[j].CompareTo(element) > 0)
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
