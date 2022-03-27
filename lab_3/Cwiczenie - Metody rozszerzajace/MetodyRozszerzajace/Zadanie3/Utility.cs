using System.Collections.Generic;

namespace Extensions
{
    public static class Utility
    {
        public static string Dump<T>(this IList<T> lista)
        {
            var napis = default(string);
            foreach (var item in lista)
            {
                napis += " " + item + ",";
            }
            napis = napis.Remove(0, 1);
            napis = napis.Remove(napis.Length - 1);

            return "{" + napis + "}";
        }
    }
}