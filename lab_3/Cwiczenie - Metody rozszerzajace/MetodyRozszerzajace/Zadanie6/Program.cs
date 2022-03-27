using Extensions;

namespace Zadanie6
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