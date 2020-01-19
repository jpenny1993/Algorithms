using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Demo.Sort
{
    public class LinqSorter<T> : ISorter<T> where T : struct, IComparable<T>
    {
        public IList<T> Sort(IEnumerable<T> source)
        {
            return source.OrderBy(x => x).ToArray();
        }

        public void Sort(ref IList<T> source) 
        {
            source = source.OrderBy(x => x).ToArray();
        }
    }
}
