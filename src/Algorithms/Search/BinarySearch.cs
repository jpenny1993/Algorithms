using System;
using System.Collections.Generic;
using System.Linq;

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
                mid = left + (int)Math.Floor((double)(right - left) / 2);
                var comparison = source[mid].CompareTo(item);

                if (comparison > 0)
                {
                    right = mid - 1;
                }
                else if (comparison < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    return mid;
                }
            } while (left <= right);

            return -1;        
        }
    }
}
