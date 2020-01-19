using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Demo.Sort
{
    public class LinqSorter<T> : ISorter<T> where T : struct, IComparable<T>
    {
        public void SortAscending(ref IList<T> source) 
        {
            source = source.OrderBy(x => x).ToArray();
        }

        public void SortDescending(ref IList<T> source)
        {
            source = source.OrderByDescending(x => x).ToArray();
        }
    }
}
