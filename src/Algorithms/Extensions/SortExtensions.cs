using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Sort;

namespace Algorithms.Extensions
{
    public static class SortExtensions
    {
        /// <summary>
        /// Enumerates all elements into a collection
        /// and then sorts the collection in ascending order.
        /// </summary>
        /// <param name="enumerable">An enumerable collection of elements</param>
        /// <param name="sorter">A sort algorithm provider</param>
        /// <returns>A sorted collection</returns>
        public static IList<T> SortAscending<T>(this IEnumerable<T> enumerable, ISorter<T> sorter) where T : IComparable<T>
        {
            IList<T> collection = enumerable.ToArray();
            sorter.SortAscending(ref collection);
            return collection;
        }

        /// <summary>
        /// Enumerates all elements into a collection
        /// and then sorts the collection in descending order.
        /// </summary>
        /// <param name="enumerable">An enumerable collection of elements</param>
        /// <param name="sorter">A sort algorithm provider</param>
        /// <returns>A sorted collection</returns>
        public static IList<T> SortDescending<T>(this IEnumerable<T> enumerable, ISorter<T> sorter) where T : IComparable<T>
        {
            IList<T> collection = enumerable.ToArray();
            sorter.SortDescending(ref collection);
            return collection;
        }

        public static IList<T> BubbleSortAsc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortAscending(enumerable, new BubbleSorter<T>());

        public static IList<T> BubbleSortDesc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortDescending(enumerable, new BubbleSorter<T>());

        public static IList<T> HeapSortAsc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortAscending(enumerable, new HeapSorter<T>());

        public static IList<T> HeapSortDesc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortDescending(enumerable, new HeapSorter<T>());

        public static IList<T> InsertionSortAsc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortAscending(enumerable, new InsertionSorter<T>());

        public static IList<T> InsertionSortDesc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortDescending(enumerable, new InsertionSorter<T>());

        public static IList<T> QuickSortAsc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortAscending(enumerable, new QuickSorter<T>());

        public static IList<T> QuickSortDesc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortDescending(enumerable, new QuickSorter<T>());

        public static IList<T> SelectionSortAsc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortAscending(enumerable, new SelectionSorter<T>());
        public static IList<T> SelectionSortDesc<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
            => SortDescending(enumerable, new SelectionSorter<T>());
    }
}
