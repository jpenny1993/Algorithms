using System;
using System.Collections.Generic;

namespace Algorithms.Search
{
    public class ExponentialSearcher : ISearcher<int>
    {
        private readonly BinarySearcher<int> _binarySearch = new BinarySearcher<int>();

        public int IndexOf(IList<int> source, int item)
        {
            // Check the first item
            if (source[0] == item) 
            {
                return 0;
            }

            // Find range for binary search by repeated doubling
            var length = source.Count;
            int i = 1;
            while (i < length && source[i] <= item)
            {
                i *= 2;
            }

            // Call binary search for the found range.
            return _binarySearch.Search(source, i / 2, Math.Min(i, length), item);
        }
    }
}
