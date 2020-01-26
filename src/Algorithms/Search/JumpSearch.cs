using System;
using System.Collections.Generic;

namespace Algorithms.Search
{
    public class JumpSearcher<T> : ISearcher<T> where T : IComparable<T>
    {
        public int IndexOf(IList<T> source, T item)
        {
            // Find the block size to be jumped 
            var n = source.Count;
            var step = (int)Math.Floor(Math.Sqrt(n));

            // Find the block that should contain the item
            int prev = 0;
            while (source[Math.Min(step, n) - 1].CompareTo(item) < 0)
            {
                prev = step;
                step += (int)Math.Floor(Math.Sqrt(n));

                if (prev >= n)
                {
                    return -1;
                }
            }

            // Do a linear search through the block for the item 
            while (source[prev].CompareTo(item) < 0)
            {
                prev++;

                // We reached the next block or end of the array 
                if (prev == Math.Min(step, n))
                {
                    return -1;
                }
            }

            // We found the item
            if (source[prev].CompareTo(item) == 0)
            {
                return prev;
            }

            return -1;
        }
    }
}
