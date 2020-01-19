# Algorithms
I wanted to write my own implementations of various search + sort algorithms given existing psuedo code.
At the moment I have only implemented the sort algorithms.

## Sort algorithms
While writing this I thought it would be interesting to compare different sorting algorithms to linq to entities
to see at what times it would be more appropriate to use a specific sorting algorithm.

### Rough performance metrics on integers
The following table is from 50 test iterations of the same cloned 10,000 item array (in debug mode).
Please take this with a grain of salt as I found that making this more generic added a small amount of overhead. 
If you wrote this as a static function to do a specific job then quick sort is slightly faster.


| Algorithm      | Avg          | Fastest     | Slowest     |
|:---------------|-------------:|------------:|------------:|
| Linq OrderBy   |   3.1217 ms  |   1.7268 ms |  14.8952 ms |
| Quick Sort     |   4.2011 ms  |   3.0471 ms |   7.0002 ms |
| Heap Sort      |   8.0557 ms  |   6.2929 ms |  10.3258 ms |
| Merge Sort     |  60.0008 ms  |  56.1709 ms |  64.2097 ms |
| Insertion Sort | 245.5693 ms  | 228.3379 ms | 288.7145 ms |
| Selection Sort |  358.244 ms  | 344.9076 ms | 400.0405 ms |
| Bubble Sort    |  839.0701 ms | 822.5390 ms | 873.4542 ms |

### Overhead from using extension methods
The following table is from 1000 test iterations of a randomly generated 50 item array (in debug mode).
System.Linq.OrderBy() vs my implementation of Quick Sort.

| Code                    | Avg       | Fastest   | Slowest   |
|:------------------------|----------:|----------:|----------:|
| Linq OrderBy            | 0.0209 ms | 0.0077 ms | 9.7531 ms |
| Enumerable extension    | 0.0250 ms | 0.0137 ms | 4.5683 ms |
| Inline with enumeration | 0.0166 ms | 0.0130 ms | 0.0727 ms |
| Algorithm only          | 0.0141 ms | 0.0100 ms | 0.0393 ms |


The following table is from 1000 Quick Sort test iterations of a randomly generated 100,0000 item array (in debug mode).
System.Linq.OrderBy() vs my implementation of Quick Sort.

| Code                    | Avg         | Fastest     | Slowest    |
|:------------------------|------------:|------------:|-----------:|
| Linq OrderBy            | 714.4982 ms | 560.6754 ms | 1.088 secs |
| Enumerable extension    | 699.8664 ms | 565.9213 ms | 1.087 secs |
| Inline with enumeration | 628.1511 ms | 560.8270 ms | 1.048 secs |
| Algorithm only          | 615.4799 ms | 545.2327 ms | 1.050 secs |

### Conclusion
You should be using linq to objects provided by microsoft to do your sorting.
After spending time writing all of these algorithms myself I had a look into what algorithm OrderBy uses. 
As it turns out OrderBy implements quick sort, so there are no real performance benefits from implementing a sort algorithm myself.
