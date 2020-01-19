using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sort
{
    public class HeapSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IEnumerable<T> source)
        {
            IList<T> collection = source.ToArray();
            Sort(ref collection);
            return collection;
        }

        public void Sort(ref IList<T> source) => HeapSort(ref source, source.Count);

        private static void HeapSort(ref IList<T> source, int length)
        {
            T temp;
            int i;

            for (i = (length / 2); i >= 0; i--)
            {
                SiftDown(ref source, i, length - 1);
            }

            for (i = length - 1; i >= 1; i--)
            {
                // Swap
                temp = source[0];
                source[0] = source[i];
                source[i] = temp;

                SiftDown(ref source, 0, i - 1);
            }
        }

        private static void SiftDown(ref IList<T> source, int root, int bottom)
        {
            int maxChild = root * 2 + 1;

            // Find the biggest child
            if (maxChild < bottom)
            {
                int otherChild = maxChild + 1;
                // Reversed for stability
                maxChild = (source[otherChild].CompareTo(source[maxChild]) > 0) ? otherChild : maxChild;
            }
            else
            {
                // Don't overflow
                if (maxChild > bottom) return;
            }

            // If we have the correct ordering, we are done.
            if (source[root].CompareTo(source[maxChild]) >= 0) return;

            // Swap
            T temp = source[root];
            source[root] = source[maxChild];
            source[maxChild] = temp;

            // Tail queue recursion. Will be compiled as a loop with correct compiler switches.
            SiftDown(ref source, maxChild, bottom);
        }
    }
}
