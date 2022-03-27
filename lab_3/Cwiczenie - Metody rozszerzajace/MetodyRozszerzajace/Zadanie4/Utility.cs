using System.Collections.Generic;

namespace Extensions
{
    public static class Utility
    {
        public static void PrintLn<T>(this IEnumerable<T> lista)
        {
            foreach (var element in lista)
            {
                System.Console.WriteLine(element);
            }
        }
    }
}