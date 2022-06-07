using System;

namespace FiguryLib
{
    public class Kula : Sfera, IMierzalna2D, IMierzalna3D
    {
        public double Objetosc => 4 / 3 * Math.PI * Math.Pow(R, 3);

        public Kula() : base() { }

        public Kula(Punkt3D srodek, double promien, string nazwa = "") : base (srodek, promien, nazwa)
        {
            R = promien;
            O = srodek;
        }

        public override string ToString(Format format)
            => $"{base.ToString(format)}, Objętość = {Objetosc}";

        public static explicit operator Okrag2D(Kula kula)
            => new Okrag2D(new Punkt2D(kula.O.X, kula.O.Y), kula.R);

        public static explicit operator Kolo2D(Kula kula)
            => new Kolo2D(new Punkt2D(kula.O.X, kula.O.Y), kula.R);
    }
}