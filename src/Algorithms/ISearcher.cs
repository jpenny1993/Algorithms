using System;
using System.Collections.Generic;

namespace Algorithms
{
    public interface ISearcher<T> where T : IComparable<T>
    {
        int IndexOf(IList<T> source, T item);
    }
}
