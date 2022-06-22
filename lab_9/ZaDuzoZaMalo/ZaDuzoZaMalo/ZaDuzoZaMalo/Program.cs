using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using GraZaDuzoZaMalo.Model;

namespace AppGraZaDuzoZaMaloCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            (new KontrolerCLI()).Uruchom();
        }
    }
}
// https://github.com/wsei-csharp201/cs-lab-serializacja-szyfrowanie-GraZgadywanka
// TODO
// 1. Czas po zapisie
// 2. Zapis gry
// 3. Poprawienie wyświetalnia historii gry