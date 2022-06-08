using FiguryLib;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Figury
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("--- Proste testy konstrukcji i modyfikacji: Okrag2D");
            Okrag2D o1 = new Okrag2D();
            Console.WriteLine(o1);
            o1.Rysuj();
            Console.WriteLine(o1.ToString(Format.Pelny));
            o1.Nazwa = "o1";
            o1.O = new Punkt2D(2, 2);
            o1.R = 2;
            Console.WriteLine(o1.ToString(Format.Pelny));

            Console.WriteLine("\n--- Proste testy: Kolo2D");
            Kolo2D k1 = new Kolo2D(srodek: new Punkt2D(1, 1), promien: 2);
            k1.Rysuj();
            Console.WriteLine(k1.ToString(Format.Pelny));

            Console.WriteLine("--- --- rzutowanie koła na okrąg");
            Okrag2D o2 = (Okrag2D)k1;
            Console.WriteLine(o2.ToString(Format.Pelny));

            Console.WriteLine("--- --- konwersja koła na okrąg");
            Okrag2D o2_1 = k1.ToOkrag2D();
            Console.WriteLine(o2_1.ToString(Format.Pelny));

            Console.WriteLine("--- --- konwersja okręgu na koło");
            Kolo2D k2 = o1.ToKolo2D();
            Console.WriteLine(k2.ToString(Format.Pelny));

            Console.WriteLine("--- --- przesuwanie, skalowanie");
            k2.Przesun(1, 1);
            Console.WriteLine($"Po przesunięciu {k2.Nazwa}: {k2.ToString(Format.Pelny)}");

            k2.Skaluj(2);
            Console.WriteLine($"Po skalowaniu {k2.Nazwa}: {k2.ToString(Format.Pelny)}");


            Console.WriteLine("\n--- Proste testy: Sfera, Kula");
            Sfera s1 = new Sfera(new Punkt3D(1, -1, 0), promien: 2);
            Console.WriteLine(s1.ToString(Format.Pelny));
            Kula ku1 = new Kula();
            Console.WriteLine(ku1.ToString(Format.Pelny));
            ku1.R = 3;
            ku1.Rysuj();

            Kula ku2 = new Kula(srodek: Punkt3D.ZERO, promien: 1);
            Sfera s2 = ku2.ToSfera(); //konwersja kuli na sferę
            s2.R = 2;
            Console.WriteLine(s2.ToString(Format.Pelny));
            var ku3 = s2.ToKula(); //konwersja sfery na kulę

            var o3 = (Okrag2D)s2; //rzutowanie sfery na okrąg
            Console.WriteLine(o3.ToString(Format.Pelny));

            var o4 = (Okrag2D)ku2; //rzutowanie kuli na okrąg
            Console.WriteLine(o4.ToString(Format.Pelny));

            var k3 = (Kolo2D)ku2; //rzutowanie kuli na koło
            Console.WriteLine(o4.ToString(Format.Pelny));

            Console.WriteLine("\n ** Lista Figur **");
            List<Figura> lista = new List<Figura> { o1, k1, o2, o2_1, k2, s1, ku1, ku2, s2, ku3, o3, o4, k3 };

            var sredniaDlugoscList = new List<double>();
            var sumaPole = 0d;
            var maxObjetosc = 0d;
            foreach (var x in lista)
            {
                x.Rysuj();

                var mierzalna1D = x as IMierzalna1D;
                if (mierzalna1D != null)
                    sredniaDlugoscList.Add(mierzalna1D.Dlugosc);
                var mierzalna2D = x as IMierzalna2D;
                if (mierzalna2D != null)
                    sumaPole += mierzalna2D.Pole;
                var mierzalna3D = x as IMierzalna3D;
                if (mierzalna3D != null)
                {
                    if (maxObjetosc < mierzalna3D.Objetosc)
                        maxObjetosc = mierzalna3D.Objetosc;
                }

                //if (x is IMierzalna1D) sredniaDlugoscList.Add(((IMierzalna1D)x).Dlugosc);
                //if (x is IMierzalna2D) sumaPole += ((IMierzalna2D)x).Pole; ;
                //if (x is IMierzalna3D)
                //{
                //    if (maxObjetosc < ((IMierzalna3D)x).Objetosc)
                //        maxObjetosc = ((IMierzalna3D)x).Objetosc;
                //    Console.WriteLine();
                //    Console.WriteLine(((IMierzalna3D)x).Objetosc);
                //}
            }
            Console.WriteLine();
            Console.WriteLine($"Średnia długość figur = {sredniaDlugoscList.Average():0.##} ");
            Console.WriteLine($"Sumaryczne pole figur = {sumaPole:0.#} ");
            Console.WriteLine($"Objętość figury największej = {maxObjetosc:0.##} ");
        }
    }
}