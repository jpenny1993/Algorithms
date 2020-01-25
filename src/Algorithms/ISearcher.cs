using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Algorithms
{
    public interface ISearcher<T>
    {
        /// <summary>
        /// Searches the referenced collection for the all elements that matches the provided predicate.
        /// </summary>
        /// <param name="source">A collection of elements</param>
        /// <param name="predicate">An expression that returns successfully when the item is found.</param>
        /// <returns>An enumerable of matching items</returns>
        IEnumerable<T> Filter(IList<T> source, Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Searches the referenced collection for the all elements that matches the provided predicate.
        /// </summary>
        /// <param name="source">A collection of elements</param>
        /// <param name="predicate">An expression that returns successfully when the item is found.</param>
        /// <returns>An enumerable of matching indices</returns>
        IEnumerable<int> FilterIndices(IList<T> source, Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Searches the referenced collection for the element that matches the provided predicate.
        /// </summary>
        /// <param name="source">A collection of elements</param>
        /// <param name="predicate">An expression that returns successfully when the item is found.</param>
        /// <returns>Item or null</returns>
        T Find(IList<T> source, Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Searches the referenced collection for the element that matches the provided predicate.
        /// </summary>
        /// <param name="source">A collection of elements</param>
        /// <param name="predicate">An expression that returns successfully when the item is found.</param>
        /// <returns>The index of the element or -1</returns>
        int FindIndex(IList<T> source, Expression<Func<T, bool>> predicate);
    }
}
