using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Algorithms.Demo.Sort;
using Algorithms.Search;
using Algorithms.Sort;

namespace Algorithms.Demo
{
    class Program
    {
        private static Random _random;
        private static IDictionary<string, List<TimeSpan>> _dictionary;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Up!\r\n");

            _dictionary = new Dictionary<string, List<TimeSpan>>();
            _random = new Random();

            if (AskAction("Test Search Algorithms"))
                TestSearchAlgorithms();

            if (AskAction("Test Sort Algorithms"))
                TestSortAlgorithms();

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }

        private static int ReadIntAbs(string message, int defaultValue, int minValue = 0) 
        {
            Console.Write("{0} [{1}]> ", message, defaultValue);
            var input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input) &&
                int.TryParse(input, out var number)) 
            {
                number = Math.Abs(number);
                return number < minValue ? minValue : number;
            }

            return defaultValue;
        }

        private static bool AskAction(string message) 
        {
            do
            {
                Console.Write("{0} [Y/N]> ", message);
                var input = Console.ReadKey();
                Console.WriteLine("\r\n");

                if (input.Key == ConsoleKey.Y) return true;
                if (input.Key == ConsoleKey.N) return false;
            } while (true);
        }

        private static IList<int> GenerateTestSample(int size, int min, int max) 
        {
            return (new int[size])
                .Select(x => _random.Next(min, max))
                .ToArray();
        }

        private static string GetTime(TimeSpan time)
        {
            if (time.Seconds > 0)
            {
                return $"{time.TotalSeconds: 0.000} secs";
            }

            return $"{time.TotalMilliseconds} ms";
        }

        private static void RecordSearchTime(string key, ISearcher<int> searcher, int iterations, IEnumerable<int> enumerable)
        {
            var hasKey = _dictionary.ContainsKey(key);
            var results = hasKey ? _dictionary[key] : new List<TimeSpan>();

            IList<int> array = enumerable.ToArray();
            for (var i = 1; i < iterations + 1; i++)
            {
                Console.WriteLine("Begin Test: {0} v{1}", key, i);

                var targetIndex = _random.Next(0, array.Count);
                var target = array[targetIndex];
                Console.WriteLine("Target: {0}/{1}, Value: {2}", targetIndex, array.Count, target);

                Expression<Func<int, bool>> predicate = x => x == target;
                Console.WriteLine("Starting timer...");
                var timer = new Stopwatch();

                timer.Start();
                var matchIndex = searcher.FindIndex(array, predicate);
                timer.Stop();

                Console.WriteLine("Time Taken: {0}", GetTime(timer.Elapsed));

                if (matchIndex > -1)
                {
                    Console.WriteLine("First Match: {0}/{1}, Value: {2}", matchIndex, array.Count, array[matchIndex]);
                }
                else 
                {
                    Console.WriteLine("First Match: Not Found...");
                }

                // Perform a linear search to find all values
                var expectedIndicies = new List<int>();
                var evaluateFunc = predicate.Compile();
                for (var index = 0; index < array.Count; index++)
                {
                    if (evaluateFunc.Invoke(array[index]))
                    {
                        expectedIndicies.Add(index);
                    }
                }

                var matchIndices = searcher.FilterIndices(array, predicate).OrderBy(x => x).ToArray();
                Console.WriteLine("Total Matches: {0}, All Matches: [{1}]", matchIndices.Length, string.Join(", ", matchIndices));

                // Check all matching items are found
                var searchSuccess = expectedIndicies.Count == matchIndices.Length;
                if (searchSuccess)
                    for (var j = 0; j < expectedIndicies.Count; j++)
                    {
                        if (expectedIndicies[j] != matchIndices[j])
                        {
                            searchSuccess = false;
                            break;
                        }
                    }

                if (searchSuccess)
                {
                    Console.WriteLine("Valid Search: Yes");
                    results.Add(timer.Elapsed);
                }
                else
                {
                    Console.WriteLine("Valid Search: No");
                }


                Console.WriteLine("End Test: {0} v{1}\r\n", key, i);
            }

            if (!hasKey)
                _dictionary.Add(key, results);
        }

        private static void RecordSortTime(string key, ISorter<int> sorter, int iterations, IEnumerable<int> enumerable)
        {
            var hasKey = _dictionary.ContainsKey(key);
            var results = hasKey ? _dictionary[key] : new List<TimeSpan>();

            for (var i = 1; i < iterations + 1; i++) 
            {
                Console.WriteLine("Begin Test: {0} v{1}", key, i);
                IList<int> array = enumerable.ToArray();

                Console.WriteLine("Starting timer...");
                var timer = new Stopwatch();

                var testSortAscending = i % 2 == 0;
                if (testSortAscending)
                {
                    timer.Start();
                    sorter.SortAscending(ref array);
                    timer.Stop();
                }
                else 
                {
                    timer.Start();
                    sorter.SortDescending(ref array);
                    timer.Stop();
                }

                Console.WriteLine("Time Taken: {0}", GetTime(timer.Elapsed));

                var sample = array.Skip(100).Take(10);
                Console.WriteLine("Sample Results: {0}", string.Join(", ", sample));

                var sortSuccess = true;
                for (var j = 1; j < array.Count; j++) 
                {
                    if (testSortAscending && array[j - 1] > array[j])
                    {
                        sortSuccess = false;
                        break;
                    }
                    else if (!testSortAscending && array[j - 1] < array[j])
                    {
                        sortSuccess = false;
                        break;
                    }
                }

                if (sortSuccess)
                {
                    Console.WriteLine("Sorted: Yes");
                    results.Add(timer.Elapsed);
                }
                else 
                {
                    Console.WriteLine("Sorted: No");
                }

                Console.WriteLine("End Test: {0} v{1}\r\n", key, i);
            }

            if (!hasKey)
                _dictionary.Add(key, results);
        }

        private static void TestSearchAlgorithms()
        {
            int minValue = ReadIntAbs("Min value", 0);
            int maxValue = ReadIntAbs("Max value", 10000, minValue);
            int testSize = ReadIntAbs("Array size", 10000, 10);
            int iterations = ReadIntAbs("Iterations per test", 50, 1);
            Console.WriteLine();

            var numbers = GenerateTestSample(testSize, minValue, maxValue);

            if (AskAction("Linear Search"))
                RecordSearchTime("Linear Search", new LinearSearcher<int>(), iterations, numbers);

            WriteResults();
        }

        private static void TestSortAlgorithms()
        {
            int minValue = ReadIntAbs("Min value", 0);
            int maxValue = ReadIntAbs("Max value", 10000, minValue);
            int testSize = ReadIntAbs("Array size", 10000, 10);
            int iterations = ReadIntAbs("Iterations per test", 50, 1);
            Console.WriteLine();

            var numbers = GenerateTestSample(testSize, minValue, maxValue);

            if (AskAction("Linq OrderBy"))
                RecordSortTime("Linq OrderBy", new LinqSorter<int>(), iterations, numbers);

            if (AskAction("Quick Sort"))
                RecordSortTime("Quick Sort", new QuickSorter<int>(), iterations, numbers);

            if (AskAction("Heap Sort"))
                RecordSortTime("Heap Sort", new HeapSorter<int>(), iterations, numbers);

            if (AskAction("Merge Sort"))
                RecordSortTime("Merge Sort", new MergeSorter<int>(), iterations, numbers);

            if (AskAction("Insertion Sort"))
                RecordSortTime("Insertion Sort", new InsertionSorter<int>(), iterations, numbers);

            if (AskAction("Selection Sort"))
                RecordSortTime("Selection Sort", new SelectionSorter<int>(), iterations, numbers);

            if (AskAction("Bubble Sort"))
                RecordSortTime("Bubble Sort", new BubbleSorter<int>(), iterations, numbers);

            WriteResults();
        }

        private static void WriteResults()
        {
            if (!_dictionary.Keys.Any())
                return;

            Console.WriteLine(" ------------ ");
            Console.WriteLine("    Results   ");
            Console.WriteLine(" ------------ \r\n");
            foreach (var key in _dictionary.Keys) 
            {
                var results = _dictionary[key];

                if (!results.Any()) 
                {
                    Console.WriteLine(" {0, -13} \t was not succesful.\r\n", key);
                    continue;
                }

                var fastestTime = results.OrderBy(x => x).First();
                var slowestTime = results.OrderByDescending(x => x).First();

                var averageTime = TimeSpan.FromTicks(results.Sum(x => x.Ticks) / results.Count);

                // Attempts faster than the average
                var aboveAvergageCount = results.Where(x => x < averageTime).Count();

                var template = " {0, -13} \t Avg: {1}, Fastest: {2}, Slowest: {3}, {4}/{5} attempts faster than avg.\r\n";
                Console.WriteLine(template, key,
                    GetTime(averageTime),
                    GetTime(fastestTime), 
                    GetTime(slowestTime),
                    aboveAvergageCount,
                    results.Count
                );

            }

            Console.WriteLine(" ------------ \r\n");
            _dictionary.Clear();
        }
    }
}
