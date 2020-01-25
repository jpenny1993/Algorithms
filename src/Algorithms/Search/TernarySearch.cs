using System;
using System.Collections.Generic;

namespace Algorithms.Search
{
    public class TernarySearcher<T> : ISearcher<T> where T : IComparable<T>
    {
        public int IndexOf(IList<T> source, T item)
        {
            int left = 0, right = source.Count - 1;
            do
            {
                // Split the array into 3 regions and get the midpoints
                int leftMid = left + (right - left) / 3;
                int rightMid = right - (right - left) / 3;

                // Check check for the item
                var leftComparison = source[leftMid].CompareTo(item);
                if (leftComparison == 0)
                {
                    return leftMid;
                }

                var rightComparison = source[rightMid].CompareTo(item);
                if (rightComparison == 0)
                {
                    return rightMid;
                }

                // Reduce to the region containing the item
                if (leftComparison > 0)
                {
                    // The item is between left and leftMid
                    right = leftMid - 1;
                }
                else if (rightComparison < 0)
                {
                    // The item is between rightMid and right
                    left = rightMid + 1;
                }
                else
                {
                    // The item is between leftMid and rightMid 
                    left = leftMid + 1;
                    right = rightMid - 1;
                }
            } while (right >= left);

            // Not found
            return -1;
        }
    }
}
