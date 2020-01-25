using System;
using System.Collections.Generic;

namespace Algorithms.Demo.Search
{
    public class DefaultSearcher<T> : ISearcher<T> where T : IComparable<T>
    {
        public int IndexOf(IList<T> source, T item) => source.IndexOf(item);
    }
}
