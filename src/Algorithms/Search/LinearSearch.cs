using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Algorithms.Search
{
    public class LinearSearcher<T> : ISearcher<T>
    {
        public IEnumerable<T> Filter(IList<T> source, Expression<Func<T, bool>> predicate)
        {
            return FilterIndices(source, predicate)
                .Select(index => source[index]);
        }

        public IEnumerable<int> FilterIndices(IList<T> source, Expression<Func<T, bool>> predicate)
        {
            var evaluateFunc = predicate.Compile();
            for (int index = 0; index < source.Count; index++)
            {
                if (evaluateFunc.Invoke(source[index]))
                {
                    yield return index;
                }
            }
        }

        public T Find(IList<T> source, Expression<Func<T, bool>> predicate)
        {
            var index = FindIndex(source, predicate);
            if (index > -1)
            {
                return source[index];
            }

            return default(T);
        }

        public int FindIndex(IList<T> source, Expression<Func<T, bool>> predicate)
        {
            var evaluateFunc = predicate.Compile();
            for (int index = 0; index < source.Count; index++)
            {
                if (evaluateFunc.Invoke(source[index]))
                {
                    return index;
                }
            }

            return -1;
        }
    }
}
