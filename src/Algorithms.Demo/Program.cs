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
            Console.WriteLine("Hello World!");

            _dictionary = new Dictionary<string, List<TimeSpan>>();

            var random = new Random();
            var numbers = (new int[10000]).Select(x => random.Next(0, 10000)).ToArray();

            RecordSortTime("Linq", new LinqSorter<int>(), numbers);

            RecordSortTime("QuickSort", new QuickSorter<int>(), numbers);

            RecordSortTime("BubbleSort", new BubbleSorter<int>(), numbers);

            WriteResults();
        }

        private static void RecordSortTime(string key, ISorter<int> sorter, IEnumerable<int> enumerable)
        {
            var hasKey = _dictionary.ContainsKey(key);
            var results = hasKey ? _dictionary[key] : new List<TimeSpan>();

            for (var i = 1; i < 51; i++) 
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
            Console.WriteLine(" ------------ \r\n");
            foreach (var key in _dictionary.Keys) 
            {
                var results = _dictionary[key];

                if (!results.Any()) 
                {
                    Console.WriteLine("{0} - Successful: 0\r\n", key);
                    continue;
                }

                var fastestTime = results.OrderBy(x => x).First();
                var slowestTime = results.OrderByDescending(x => x).First();

                var averageTime = TimeSpan.FromTicks(results.Sum(x => x.Ticks) / results.Count);

                // Attempts faster than the average
                var aboveAvergageCount = results.Where(x => x < averageTime).Count();

                var template = "{0} - Successful: {1}, > Avg: {2}, Avg: {3}, Fastest: {4}, Slowest: {5}\r\n";
                Console.WriteLine(template, key,
                    results.Count,
                    aboveAvergageCount,
                    GetTime(averageTime),
                    GetTime(fastestTime), 
                    GetTime(slowestTime)
                );

            }
            Console.WriteLine(" ------------ ");
        }

        private static string GetTime(TimeSpan time) 
        {
            if (time.Seconds > 0)
            {
                return $"{time.TotalSeconds : 0.000} secs";
            }

            return $"{time.TotalMilliseconds} ms";
        }
    }
}
