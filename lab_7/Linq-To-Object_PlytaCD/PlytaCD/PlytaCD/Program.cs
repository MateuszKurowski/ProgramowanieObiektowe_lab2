using System;
using System.Linq;

namespace PlytaCD
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var dataStringLine = Console.ReadLine();
            //var dataStringLine = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";
            var dataTab = dataStringLine.Split(',').Select(x => TimeSpan.Parse("0:" + x)).ToList();

            var tracksCount = dataTab.Count;
            var averageTime = TimeSpan.FromSeconds(Math.Ceiling(dataTab.Average(x => x.TotalSeconds)));
            var sumTime = TimeSpan.FromSeconds(dataTab.Sum(x => x.TotalSeconds));

            var test1 = tracksCount.ToString();
            var test2 = $"{averageTime.Minutes:0}:{averageTime.Seconds:00}";
            var test3 = sumTime.Hours > 0 ? $"{sumTime.Hours:0}:{sumTime.Minutes:00}:{sumTime.Seconds:00}" : $"{sumTime.Minutes:0}:{sumTime.Seconds:00}";
            Console.WriteLine($"{test1} {test2} {test3}");
        }
    }
}