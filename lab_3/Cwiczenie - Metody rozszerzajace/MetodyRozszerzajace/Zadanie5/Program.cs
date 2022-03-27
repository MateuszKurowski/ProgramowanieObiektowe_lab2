using Extensions;

using System.Collections.Generic;

namespace Zadanie5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lista = new List<int> { 1, 2, 3, 4, 0, 7 };
            var mediana = lista.Mediana();
        }
    }
}