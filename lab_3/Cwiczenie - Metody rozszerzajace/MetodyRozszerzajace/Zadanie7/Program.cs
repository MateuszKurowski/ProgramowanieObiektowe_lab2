using Extensions;

using System;
using System.Collections.Generic;

namespace Zadanie7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var flaga = false;
            if (8.Between(1,9))
                flaga = true;
            if ("alibaba".Between("ala", "baba"))
                flaga = true;
            if (8.Between(1, 4))
                flaga = true;
            if ("cwany".Between("ala", "baba"))
                flaga = true;
        }
    }
}