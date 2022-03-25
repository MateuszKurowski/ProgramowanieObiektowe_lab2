using System;

namespace wellFormedTypes
{
    class Pracownik : IEquatable<Pracownik>, IComparable<Pracownik>
    {
        private string _Nazwisko;
       public string Nazwisko { 
            get { return _Nazwisko; } 
            set { _Nazwisko = value.Trim(); } 
       }
       public DateTime DataZatrudnienia { 
            get { return _DataZatrudnienia; }
            set{ 
                if (value > DateTime.Now)
                    throw new ArgumentException();
                else _DataZatrudnienia = value;
            } 
        }
     private DateTime _DataZatrudnienia;
       public decimal Wynagrodzenie { 
            get { return _Wynagrodzenie; } 
            set { _Wynagrodzenie = (value < 0) ? 0 : value; } 
       }
       private decimal _Wynagrodzenie;

       public int CzasZatrudnienia => (DateTime.Now - DataZatrudnienia).Days / 30;

        public Pracownik()
        {
            Nazwisko = "Anonim";
            DataZatrudnienia = DateTime.Now;
            Wynagrodzenie = 0m;
        }
        public Pracownik(string nazwisko, DateTime dataZatrudnienia, decimal wynagrodzenie)
        {
            Nazwisko = nazwisko;
            DataZatrudnienia = dataZatrudnienia;
            Wynagrodzenie = wynagrodzenie;
        }

        public override string ToString()
        {
            return $"({Nazwisko}, {DataZatrudnienia:d MMM yyyy} ({CzasZatrudnienia}), {Wynagrodzenie} PLN)";
        }

        public bool Equals(Pracownik other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            
            return (Nazwisko == other.Nazwisko &&
                    DataZatrudnienia == other.DataZatrudnienia &&
                    Wynagrodzenie == other.Wynagrodzenie);
        }

        public override bool Equals(object obj)
        {
            if (obj is Pracownik)
            {
                return Equals((Pracownik)obj);
            }
            else return false;
        }
        
        public override int GetHashCode() => (Nazwisko, DataZatrudnienia, Wynagrodzenie).GetHashCode();

        public static bool Equals(Pracownik p1, Pracownik p2)
        {
            if ((p1 is null) && (p2 is null)) return true;
            if ((p1 is null)) return false;

            return p1.Equals(p2);
        }

        public static bool operator ==(Pracownik p1, Pracownik p2) => Equals(p1, p2);
        public static bool operator !=(Pracownik p1, Pracownik p2) => !(p1 == p2);

        public int CompareTo(Pracownik other)
        {
            if (other is null) return 1;
            if (this.Equals(other)) return 0;
            if (this.Nazwisko != other.Nazwisko) return this.Nazwisko.CompareTo(other.Nazwisko);

            if (!this.DataZatrudnienia.Equals(other.DataZatrudnienia)) return this.DataZatrudnienia.CompareTo(other.DataZatrudnienia);

            return this.Wynagrodzenie.CompareTo(other.Wynagrodzenie);
        }
    }
}