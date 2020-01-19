using System;
using System.Collections.Generic;

namespace Algorithms.Sort
{
    public class HeapSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void SortAscending(ref IList<T> source) => HeapSort(ref source, source.Count, 1);

        public void SortDescending(ref IList<T> source) => HeapSort(ref source, source.Count, -1);

        // TODO: Implement Direction
        private static void HeapSort(ref IList<T> source, int length, int direction)
        {
            T temp;
            int i;

            for (i = (length / 2); i >= 0; i--)
            {
                SiftDown(ref source, i, length - 1, direction);
            }

            for (i = length - 1; i >= 1; i--)
            {
                // Swap
                temp = source[0];
                source[0] = source[i];
                source[i] = temp;

                SiftDown(ref source, 0, i - 1, direction);
            }
        }

        private static void SiftDown(ref IList<T> source, int root, int bottom, int direction)
        {
            int maxChild = root * 2 + 1;

            // Find the biggest child
            if (maxChild < bottom)
            {
                int otherChild = maxChild + 1;
                // Reversed for stability
                maxChild = (source[otherChild].CompareTo(source[maxChild]) == direction) ? otherChild : maxChild;
            }
            else
            {
                // Don't overflow
                if (maxChild > bottom) return;
            }

            // If we have the correct ordering, we are done.
            var order = source[root].CompareTo(source[maxChild]);
            if (order == 0 || order == direction) return;

            // Swap
            T temp = source[root];
            source[root] = source[maxChild];
            source[maxChild] = temp;

            // Tail queue recursion. Will be compiled as a loop with correct compiler switches.
            SiftDown(ref source, maxChild, bottom, direction);
        }
    }
}
