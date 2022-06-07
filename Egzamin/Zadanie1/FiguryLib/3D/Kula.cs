using System;

namespace FiguryLib
{
    public class Kula : Sfera, IMierzalna2D, IMierzalna3D
    {
        double IMierzalna3D.Objetosc => 4 / 3 * Math.PI * Math.Pow(R, 3);

        public Kula() : base() { }

        public Kula(Punkt3D srodek, double promien, string nazwa = "") : base (srodek, promien, nazwa)
        {
            R = promien;
            O = srodek;
        }
    }
}