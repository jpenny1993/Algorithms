using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sort
{
    public class MergeSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IEnumerable<T> source)
        {
            IList<T> collection = source.ToArray();
            Sort(ref collection);
            return collection;
        }

        public void Sort(ref IList<T> source) 
        {
            if (source.Count % 2 != 0) 
            {
                // Must be able to split the collection into exactly 2 parts
                throw new OverflowException("Size of the collection is not power of 2.");
            }

            MergeSort(ref source, 0, source.Count - 1);
        }

        private static void MergeSort(ref IList<T> source, int left, int right)
        {
            int mid;
            if (right > left)
            {
                // Split the collection into 2 parts of the same size
                mid = (right + left) / 2;

                // Sort the first half & recurse
                MergeSort(ref source, left, mid);

                // Sort the second half & recurse
                MergeSort(ref source, (mid + 1), right);

                // Join the two lists back into one
                Merge(ref source, left, (mid + 1), right);
            }
        }

        private static void Merge(ref IList<T> source, int left, int mid, int right)
        {
            var temp = new T[source.Count];
            int i, eol, num, pos;
            eol = (mid - 1);
            pos = left;
            num = (right - left + 1);

            // Copy from either the left or right half to the temp collection
            while ((left <= eol) && (mid <= right))
            {
                if (source[left].CompareTo(source[mid]) <= 0)
                {
                    temp[pos++] = source[left++];
                }
                else
                {
                    temp[pos++] = source[mid++];
                }
            }

            // Copy all remaining items from the left subset
            while (left <= eol)
            {
                temp[pos++] = source[left++];
            }

            // Copy all remaining items from the right subset
            while (mid <= right)
            {
                temp[pos++] = source[mid++];
            }

            // Copy all items that were copied back to the source collection
            for (i = 0; i < num; i++)
            {
                source[right] = temp[right];
                right--;
            }
        }
    }
}
