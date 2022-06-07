using System;

namespace FiguryLib
{
    public class Sfera : Figura3D, IMierzalna2D
    {
        public Punkt3D O { get; set; }
        public double R { get; set; }

        public double Pole => 4 * Math.PI * Math.Pow(R, 2);

        public Sfera()
        {
            R = 0;
            O = Punkt3D.ZERO;
        }

        public Sfera(Punkt3D srodek, double promien, string nazwa = "") : base(nazwa)
        {
            O = srodek;
            R = promien;
        }

        public override void Przesun(double dx, double dy, double dz)
            => O = new Punkt3D(O.X + dx, O.Y + dy, O.Z + dz);

        public override void Przesun(Wektor3D kierunek)
            => O = new Punkt3D(O.X + kierunek.X, O.Y + kierunek.Y, O.Z + kierunek.Z);

        public void Skaluj(double wspSkalowania)
            => R *= Math.Pow(R, wspSkalowania);

        public virtual string ToString(Format format)
            => $"{base.ToString()}, {ToString()}, Pole = {Pole}";

        public override string ToString()
            => $"{GetType().Name}({O}, {R})";

        public static explicit operator Okrag2D(Sfera sfera)
            => new Okrag2D(new Punkt2D(sfera.O.X, sfera.O.Y), sfera.R);
    }
}