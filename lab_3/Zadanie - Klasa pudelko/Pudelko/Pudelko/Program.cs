using System;

namespace Pudelko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = "2.500 m × 9.321 m × 0.100 m";
            var testArray = test.Split(' ');

            Console.WriteLine(testArray.Length);
        }
    }
}
