using System;
using System.Linq;

namespace WynikiBiegacza
{
    class Program
    {
        static int Count() => throw new NotImplementedException();

        // ...

        static void Main(string[] args)
        {
            //    var testCase = "test4";
            //    var dataStringLine = "00:45,01:32,02:18,03:01,03:44,04:31,05:19,06:01,06:47,07:35";
            var testCase = Console.ReadLine();
            var dataStringLine = Console.ReadLine();
            var dataTab = dataStringLine.Split(',')
                            .Select(x => TimeSpan.Parse("0:" + x))
                            .ToList();
            for (int i = dataTab.Count - 1; i >= 1; i--)
            {
                dataTab[i] = dataTab[i] - dataTab[i - 1];
            }

            switch (testCase.ToLower())
            {
                case "test1":
                    Console.WriteLine(dataTab.Count);
                    break;
                case "test2":
                    var result = string.Empty;
                    foreach (var data in dataTab)
                    {
                        result += $"{data.Minutes:00}:{data.Seconds:00} ";
                    }
                    result = result.TrimEnd();
                    Console.WriteLine(result);
                    break;
                case "test3":
                    var minTime = dataTab.Min();
                    var indexes = Enumerable.Range(0, dataTab.Count)
                                 .Where(i => dataTab[i].TotalMinutes == minTime.TotalMinutes && dataTab[i].TotalSeconds == minTime.TotalSeconds)
                                 .ToList();
                    var resultTime = $"{minTime.Minutes:D2}:{minTime.Seconds:D2}";
                    var resultIndexes = string.Empty;
                    foreach (var index in indexes)
                    {
                        resultIndexes += $"{index + 1} ";
                    }
                    resultIndexes = resultIndexes.TrimEnd();
                    Console.WriteLine($"{resultTime} {resultIndexes}");
                    break;
                case "test4":
                    var maxTime = dataTab.Max();
                    var indexesMax = Enumerable.Range(0, dataTab.Count)
                                 .Where(i => dataTab[i].TotalMinutes == maxTime.TotalMinutes && dataTab[i].TotalSeconds == maxTime.TotalSeconds)
                                 .ToList();
                    var resultTimeMax = $"{maxTime.Minutes:D2}:{maxTime.Seconds:D2}";
                    var resultIndexesMax = string.Empty;
                    foreach (var index in indexesMax)
                    {
                        resultIndexesMax += $"{index + 1} ";
                    }
                    resultIndexes = resultIndexesMax.TrimEnd();
                    Console.WriteLine($"{resultTimeMax} {resultIndexes}");
                    break;
                case "test5":
                    var averageTime = TimeSpan.FromSeconds(Math.Ceiling(dataTab.Average(x => x.TotalSeconds)));
                    Console.WriteLine($"{averageTime.Minutes:00}:{averageTime.Seconds:00}");
                    break;
            }
        }
    }
}
