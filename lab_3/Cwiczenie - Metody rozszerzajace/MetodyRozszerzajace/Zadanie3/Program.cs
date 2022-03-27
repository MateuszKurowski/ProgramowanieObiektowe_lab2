using Extensions;

using System;
using System.Collections.Generic;

namespace Zadanie3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lista = new List<int> { 0, 1, 2, 3, 4 };
            Console.WriteLine(lista.Dump());
        }
    }
}