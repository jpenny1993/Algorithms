using System;
using System.Collections.Generic;

namespace Algorithms.Search
{
    public class LinearSearcher<T> : ISearcher<T> where T : IComparable<T>
    {
        public int IndexOf(IList<T> source, T item)
        {
            for (int index = 0; index < source.Count; index++)
            {
                if (source[index].CompareTo(item) == 0)
                {
                    return index;
                }
            }

            return -1;
        }
    }
}
