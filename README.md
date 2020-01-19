# Algorithms
I wanted to write my own implementations of various search + sort algorithms given existing psuedo code.
At the moment I have only implemented the sort algorithms.

## Sort Algorithms
While writing this I thought it would be interesting to compare different sorting algorithms to linq to entities
to see at what times it would be more appropriate to use a specific sorting algorithm.

### Results after 50 Tests of an 10000 item array
Please take this with a grain of salt as I found that making this more generic added a small amount of overhead.


| Algorithm      | Avg          | Fastest     | Slowest     |
|:---------------|-------------:|------------:|------------:|
| Linq OrderBy   |   3.1217 ms  |   1.7268 ms |  14.8952 ms |
| Quick Sort     |   4.2011 ms  |   3.0471 ms |   7.0002 ms |
| Heap Sort      |   8.0557 ms  |   6.2929 ms |  10.3258 ms |
| Merge Sort     |  60.0008 ms  |  56.1709 ms |  64.2097 ms |
| Insertion Sort | 245.5693 ms  | 228.3379 ms | 288.7145 ms |
| Selection Sort |  358.244 ms  | 344.9076 ms | 400.0405 ms |
| Bubble Sort    |  839.0701 ms | 822.5390 ms | 873.4542 ms |
