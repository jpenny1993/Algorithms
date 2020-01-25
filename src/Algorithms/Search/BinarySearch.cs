using System;
using System.Collections.Generic;

namespace Algorithms.Search
{
    public class BinarySearcher<T> : ISearcher<T> where T : IComparable<T>
    {
        public int IndexOf(IList<T> source, T item)
        {
            int right = source.Count - 1;
            int left = 0, mid;
            do
            {
                // Split the collection into 2 regions
                mid = left + (right - left) / 2;
                var comparison = source[mid].CompareTo(item);

                if (comparison > 0)
                {
                    // The item is in the left region
                    right = mid - 1;
                }
                else if (comparison < 0)
                {
                    // The item is in the right region
                    left = mid + 1;
                }
                else
                {
                    // We found the item
                    return mid;
                }
            } while (left <= right);

            // Not found
            return -1;        
        }
    }
}
