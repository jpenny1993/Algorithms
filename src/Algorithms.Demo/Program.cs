using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Algorithms.Demo.Sort;
using Algorithms.Sort;

namespace Algorithms.Demo
{
    class Program
    {
        private static IDictionary<string, List<TimeSpan>> _dictionary;

        static void Main(string[] args)
        {
            Console.WriteLine("Test Sort Times!\r\n");

            _dictionary = new Dictionary<string, List<TimeSpan>>();
            var random = new Random();

            int minValue = ReadIntAbs("Min value", 0);
            int maxValue = ReadIntAbs("Max value", 10000, minValue);
            int testSize = ReadIntAbs("Array size", 10000, 10);
            int iterations = ReadIntAbs("Iterations per test", 50, 1);
            Console.WriteLine();

            // Create a random test sample
            var numbers = (new int[testSize]).Select(x => random.Next(minValue, maxValue)).ToArray();

            if (AskAction("Linq OrderBy"))
                RecordSortTime("Linq", new LinqSorter<int>(), iterations, numbers);

            if (AskAction("QuickSort"))
                RecordSortTime("QuickSort", new QuickSorter<int>(), iterations, numbers);

            if (AskAction("InsertionSort"))
                RecordSortTime("InsertionSort", new InsertionSorter<int>(), iterations, numbers);

            if (AskAction("SelectionSort"))
                RecordSortTime("SelectionSort", new SelectionSorter<int>(), iterations, numbers);

            if (AskAction("BubbleSort"))
                RecordSortTime("BubbleSort", new BubbleSorter<int>(), iterations, numbers);

            WriteResults();

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

        private static string GetTime(TimeSpan time)
        {
            if (time.Seconds > 0)
            {
                return $"{time.TotalSeconds: 0.000} secs";
            }

            return $"{time.TotalMilliseconds} ms";
        }

        private static void RecordSortTime(string key, ISorter<int> sorter, int iterations, IEnumerable<int> enumerable)
        {
            var hasKey = _dictionary.ContainsKey(key);
            var results = hasKey ? _dictionary[key] : new List<TimeSpan>();

            for (var i = 1; i < iterations + 1; i++) 
            {
                Console.WriteLine("Begin Test: {0} v{1}", key, i);
                IList<int> array = enumerable.Select(x => x).ToArray();

                Console.WriteLine("Starting timer...");
                var timer = new Stopwatch();
                timer.Start();
                sorter.Sort(ref array);
                timer.Stop();

                Console.WriteLine("Time Taken: {0}", GetTime(timer.Elapsed));

                var sample = array.Skip(100).Take(10);
                Console.WriteLine("Sample Results: {0}", string.Join(", ", sample));

                var sortSuccess = true;
                for (var j = 1; j < array.Count; j++) 
                {
                    if (array[j -1] > array[j]) 
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
                    Console.WriteLine(" {0, -13} \t Sort was not succesful.\r\n", key);
                    continue;
                }

                var fastestTime = results.OrderBy(x => x).First();
                var slowestTime = results.OrderByDescending(x => x).First();

                var averageTime = TimeSpan.FromTicks(results.Sum(x => x.Ticks) / results.Count);

                // Attempts faster than the average
                var aboveAvergageCount = results.Where(x => x < averageTime).Count();

                var template = " {0, -13} \t Avg: {1}, Fastest: {2}, Slowest: {3}, {4} attempts faster than avg.\r\n";
                Console.WriteLine(template, key,
                    GetTime(averageTime),
                    GetTime(fastestTime), 
                    GetTime(slowestTime),
                    aboveAvergageCount
                );

            }
            Console.WriteLine(" ------------ \r\n");
        }
    }
}
