using System.Collections.Generic;

namespace Algorithms.Search
{
    public class InterpolationSearcher : ISearcher<int>
    {
        public int IndexOf(IList<int> source, int item)
        {
            int low = 0, high = (source.Count - 1);

            while (low <= high && item >= source[low] && item <= source[high])
            {
                // We've run out of search space
                if (low == high)
                {
                    // It's the last item
                    if (source[low] == item)
                    {
                        return low;
                    }

                    // Not found
                    return -1;
                }

                // Suggest the item position based on the items at the edges of the array
                int pos = low + (((high - low) / (source[high] - source[low] + 1)) * (item - source[low]));

                // Item found
                if (source[pos] == item)
                {
                    return pos;
                }

                if (source[pos] < item)
                {
                    // The item is in high region
                    low = pos + 1;
                }
                else
                {
                    // The item is in the low region 
                    high = pos - 1;
                }
            }

            // Not found
            return -1;
        }
    }
}
