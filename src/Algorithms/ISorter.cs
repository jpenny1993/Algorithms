using System;
using System.Collections.Generic;

namespace Algorithms
{
    public interface ISorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Sorts the referenced collection in memory into ascending order.
        /// </summary>
        /// <param name="source">A collection of elements that implement IComparable</param>
        void SortAscending(ref IList<T> source);

        /// <summary>
        /// Sorts the referenced collection in memory into descending order.
        /// </summary>
        /// <param name="source">A collection of elements that implement IComparable</param>
        void SortDescending(ref IList<T> source);
    }
}
