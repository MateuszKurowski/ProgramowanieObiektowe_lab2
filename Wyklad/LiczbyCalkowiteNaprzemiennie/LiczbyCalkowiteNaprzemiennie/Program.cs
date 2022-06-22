using System;
using System.Collections.Generic;

namespace LiczbyCalkowiteNaprzemiennie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var x in LiczbyCalkowiteNaprzemiennie(10))
            {
                Console.WriteLine($"{x} ");
            }
        }

        public static IEnumerable<int> LiczbyCalkowiteNaprzemiennie(int n) // >= 0
        {
            for (var i = 0; i <= n; i++)
            {
                if (i % 2 == 0)
                    yield return i * (-1);
                else yield return i;
            }
        }
    }
}