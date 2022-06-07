using FiguryLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguryLib
{
    public class Sfera : Figura3D, IMierzalna2D
    {
        public Punkt3D O { get; set; }
        public double R { get; set; }

        public double Pole => Math.PI * Math.Pow(R, 2);

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
            => $"{base.ToString()}, {ToString()}, Pole = {Pole:0.##}";

        public override string ToString()
            => $"{GetType().Name}({O}, {R})";
    }
}