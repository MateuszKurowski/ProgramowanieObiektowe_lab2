using System;

namespace FiguryLib
{
    public class Okrag2D : Figura2D, IMierzalna1D
    {
        public double R { get; set; }
        public Punkt2D O { get; set; }
        public double Dlugosc => 2 * Math.PI * R;

        public Okrag2D()
        {
            R = 0;
            O = Punkt2D.ZERO;
        }

        public Okrag2D(Punkt2D srodek, double promien = 0, string nazwa = "") : base(nazwa)
        {
            O = srodek;
            R = promien;
        }

        public virtual string ToString(Format format)
            => $"{base.ToString()}, {ToString()}, Długość = {Dlugosc:0.##}";

        public override string ToString()
            => $"{GetType().Name}({O}, {R})";

        public void Skaluj(double wspSkalowania)
            => R *= Math.Pow(R, wspSkalowania);

        public override void Przesun(double dx, double dy)
            => O = new Punkt2D(O.X + dx, O.Y + dy);

        public override void Przesun(Wektor2D kierunek)
            => O = new Punkt2D(O.X + kierunek.X, O.Y + kierunek.Y);
    }
}
