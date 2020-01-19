using System;
using System.Collections.Generic;

namespace Algorithms
{
    public interface ISorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Sorts the referenced collection.
        /// </summary>
        /// <param name="source">A collection of elements in memory</param>
        void Sort(ref IList<T> source);

        /// <summary>
        /// Enumerates all elements into a collection
        /// and then sorts the collection.
        /// </summary>
        /// <param name="source">An enumerable collection of elements</param>
        /// <returns>A sorted collection</returns>
        IList<T> Sort(IEnumerable<T> source);
    }
}
