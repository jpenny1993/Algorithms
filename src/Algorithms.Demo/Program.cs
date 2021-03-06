﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Algorithms.Demo.Search;
using Algorithms.Demo.Sort;
using Algorithms.Search;
using Algorithms.Sort;

namespace Algorithms.Demo
{
    class Program
    {
        private static int _minValue, _maxValue, _testSize, _iterations;
        private static Random _random;
        private static IDictionary<string, List<TimeSpan>> _dictionary;

        static void Main(string[] args)
        {
            _dictionary = new Dictionary<string, List<TimeSpan>>();
            _random = new Random();

            Console.WriteLine("Setup Tests!\r\n");
            _minValue = ReadIntAbs("Min value", 0);
            _maxValue = ReadIntAbs("Max value", 10000, _minValue);
            _testSize = ReadIntAbs("Array size", 10000, 10);
            _iterations = ReadIntAbs("Iterations per test", 50, 1);
            Console.WriteLine();

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

            return $"{time.TotalMilliseconds: 0.0000} ms";
        }

        private static void RecordSearchTime(string key, ISearcher<int> searcher, IEnumerable<int> enumerable)
        {
            var hasKey = _dictionary.ContainsKey(key);
            var results = hasKey ? _dictionary[key] : new List<TimeSpan>();

            IList<int> array = enumerable.ToArray();
            for (var i = 1; i < _iterations + 1; i++)
            {
                Console.WriteLine("Begin Test: {0} v{1}", key, i);

                var targetIndex = _random.Next(0, array.Count);
                var target = array[targetIndex];
                Console.WriteLine("Target: {0}/{1}, Value: {2}", targetIndex, array.Count, target);

                Console.WriteLine("Starting timer...");
                var timer = new Stopwatch();

                timer.Start();
                var matchIndex = searcher.IndexOf(array, target);
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
                var expectedIndicies = array
                    .Select((value, index) => (value, index))
                    .Where(x => x.value == target)
                    .Select(x => x.index)
                    .ToArray();

                if (matchIndex > -1 && expectedIndicies.Contains(matchIndex))
                {
                    Console.WriteLine("Valid Search: Yes");
                    results.Add(timer.Elapsed);
                }
                else
                {
                    Console.WriteLine("Valid Search: No");
                }

                Console.WriteLine("Could have been any of: [{0}]", string.Join(", ", expectedIndicies));


                Console.WriteLine("End Test: {0} v{1}\r\n", key, i);
            }

            if (!hasKey)
                _dictionary.Add(key, results);
        }

        private static void RecordSortTime(string key, ISorter<int> sorter, IEnumerable<int> enumerable)
        {
            var hasKey = _dictionary.ContainsKey(key);
            var results = hasKey ? _dictionary[key] : new List<TimeSpan>();

            for (var i = 1; i < _iterations + 1; i++) 
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
            var sortedTestSample = GenerateTestSample(_testSize, _minValue, _maxValue).OrderBy(x => x);

            if (AskAction("Collection.IndexOf"))
                RecordSearchTime("Collection.IndexOf", new DefaultSearcher<int>(), sortedTestSample);

            if (AskAction("Linear Search"))
                RecordSearchTime("Linear Search", new LinearSearcher<int>(), sortedTestSample);

            if (AskAction("Binary Search"))
                RecordSearchTime("Binary Search", new BinarySearcher<int>(), sortedTestSample);

            if (AskAction("Ternary Search"))
                RecordSearchTime("Ternary Search", new TernarySearcher<int>(), sortedTestSample);

            if (AskAction("Jump Search"))
                RecordSearchTime("Jump Search", new JumpSearcher<int>(), sortedTestSample);

            if (AskAction("Interpolation Search"))
                RecordSearchTime("Interpolation Search", new InterpolationSearcher(), sortedTestSample);

            if (AskAction("Exponential Search"))
                RecordSearchTime("Exponential Search", new ExponentialSearcher(), sortedTestSample);

            WriteResults();
        }

        private static void TestSortAlgorithms()
        {
            var testSample = GenerateTestSample(_testSize, _minValue, _maxValue);
            
            if (AskAction("Linq OrderBy"))
                RecordSortTime("Linq OrderBy", new LinqSorter<int>(), testSample);

            if (AskAction("Quick Sort"))
                RecordSortTime("Quick Sort", new QuickSorter<int>(), testSample);

            if (AskAction("Heap Sort"))
                RecordSortTime("Heap Sort", new HeapSorter<int>(), testSample);

            if (AskAction("Merge Sort"))
                RecordSortTime("Merge Sort", new MergeSorter<int>(), testSample);

            if (AskAction("Insertion Sort"))
                RecordSortTime("Insertion Sort", new InsertionSorter<int>(), testSample);

            if (AskAction("Selection Sort"))
                RecordSortTime("Selection Sort", new SelectionSorter<int>(), testSample);

            if (AskAction("Bubble Sort"))
                RecordSortTime("Bubble Sort", new BubbleSorter<int>(), testSample);

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
                    Console.WriteLine(" {0, -15} - was not succesful.\r\n", key);
                    continue;
                }

                var fastestTime = results.OrderBy(x => x).First();
                var slowestTime = results.OrderByDescending(x => x).First();

                var averageTime = TimeSpan.FromTicks(results.Sum(x => x.Ticks) / results.Count);

                // Attempts faster than the average
                var aboveAvergageCount = results.Where(x => x < averageTime).Count();

                var template = " {0, -15} - Avg: {1}, Fastest: {2}, Slowest: {3}, {4}/{5} attempts faster than avg.\r\n";
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
