using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace BoxLib
{
    public enum UnitOfMeasure
    {
        milimeter,
        centimeter,
        meter
    }

    public sealed class Box : IFormattable, IEquatable<Box>, IEnumerable//, IComparable<Box>
    {
        #region zadanie 1        
        /// <summary>
        /// Długość pudełka
        /// </summary>
        private readonly double _A;
        /// <summary>
        /// Szerokość pudełka
        /// </summary>
        private readonly double _B;
        /// <summary>
        /// Wysokość pudełka
        /// </summary>
        private readonly double _C;

        /// <summary>
        /// Ustawia długośc pudełka
        /// </summary>
        private double A { init { _A = ValidateLengthBeforeSave(value, "A"); } }

        /// <summary>
        /// Ustawia szerokość pudełka
        /// </summary>
        private double B { init { _B = ValidateLengthBeforeSave(value, "B"); } }

        /// <summary>
        /// Ustawia wysokość pudełka
        /// </summary>
        private double C { init { _C = ValidateLengthBeforeSave(value, "C"); } }

        /// <summary>
        /// Zwraca długość pudełka
        /// </summary>
        /// <param name="unit">Jednostka miary</param>
        /// <returns>Długość pudełka</returns>
        /// <exception cref="System.ArgumentException">Nieznana jednostka miary.</exception>
        public double GetA(UnitOfMeasure unit = UnitOfMeasure.meter) => GetValue(_A, unit);

        /// <summary>
        /// Zwraca szerokość pudełka
        /// </summary>
        /// <param name="unit">Jednostka miary</param>
        /// <returns>Szerokość pudełka</returns>
        /// <exception cref="System.ArgumentException">Nieznana jednostka miary.</exception>
        public double GetB(UnitOfMeasure unit = UnitOfMeasure.meter) => GetValue(_B, unit);

        /// <summary>
        /// Zwraca wysokość pudełka
        /// </summary>
        /// <param name="unit">Jednostka miary</param>
        /// <returns>Wysokość pudełka</returns>
        /// <exception cref="System.ArgumentException">Nieznana jednostka miary.</exception>
        public double GetC(UnitOfMeasure unit = UnitOfMeasure.meter) => GetValue(_C, unit);

        /// <summary>
        /// Zwraca wartość w odpowiedniej jednostce miary
        /// </summary>
        /// <param name="value">Wartość długości</param>
        /// <param name="unit">Jednostka miary</param>
        private double GetValue(double value, UnitOfMeasure unit)
        {
            return unit switch
            {
                UnitOfMeasure.meter => Math.Round(value, 3),
                UnitOfMeasure.centimeter => Math.Round(value / 0.01, 3),
                UnitOfMeasure.milimeter => Math.Round(value / 0.001, 3),
                _ => throw new ArgumentException("Nieznana jednostka miary."),
            };
        }

        private readonly double[] _Edges;

        /// <summary>
        /// Jednostka wymiaru pudełka
        /// </summary>
        public UnitOfMeasure Unit { get; init; } = UnitOfMeasure.meter;
        #endregion

        #region zadanie 2
        public Box(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if (a is null)
                A = 0.1;
            if (b is null)
                B = 0.1;
            if (c is null)
                C = 0.1;

            Unit = unit;
            if (a is not null)
                A = (double)a;
            if (b is not null)
                B = (double)b;
            if (c is not null)
                C = (double)c;

            _Edges = new double[] { _A, _B, _C };
        }
        #endregion

        #region zadanie 3
        /// <summary>
        /// Na podstawie jednostki miary klasy zamienia podaną długość na metry
        /// </summary>
        /// <param name="value">Długość</param>
        /// <returns>Długość w metrach</returns>
        private double ValidateLengthBeforeSave(double value, string boxName = null)
        {
            if ((Unit == UnitOfMeasure.milimeter || Unit == UnitOfMeasure.centimeter) && value <= 0.1)
                throw new ArgumentOutOfRangeException(boxName, "Wymiar pudełka nie może być ujemny!");
            if (Unit == UnitOfMeasure.meter && value <= 0 )
                throw new ArgumentOutOfRangeException(boxName, "Wymiar pudełka nie może być ujemny!");

            var meters = ConvertToMeters(value, Unit);
            if (meters >= 10) throw new ArgumentOutOfRangeException(boxName, "Wymiar pudełka nie może być ujemny!");
            return meters;
        }

        public static double ConvertToMeters(double value, UnitOfMeasure unit)
        {
            return unit switch
            {
                UnitOfMeasure.milimeter => Math.Round(value / 1000, 3, MidpointRounding.ToZero),
                UnitOfMeasure.centimeter => Math.Round(value / 100, 3, MidpointRounding.ToZero),
                UnitOfMeasure.meter => Math.Round(value, 3, MidpointRounding.ToZero),
                _ => throw new ArgumentException("Nieznana jednostka miary."),
            };
        }
        #endregion

        #region zadanie 4
        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            //if (string.IsNullOrEmpty(format)) throw new FormatException("Podano błędny format miary!");
            if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;

            return format?.ToLower() switch
            {
                "m" => $"{_A:0.000} m × {_B:0.000} m × {_C:0.000} m",
                "cm" => $"{_A * 100:0.0} cm × {_B * 100:0.0} cm × {_C * 100:0.0} cm",
                "mm" => $"{_A * 1000:0.0} mm × {_B * 1000:0.0} mm × {_C * 1000} mm",
                _ => $"{_A:0.000} m × {_B:0.000} m × {_C:0.000} m"
                //_ => throw new FormatException("Podano błędny format miary!"),
            };
        }
        public string ToString(string format)
        {
            return format?.ToLower() switch
            {
                "m" => $"{_A:0.000} m × {_B:0.000} m × {_C:0.000} m",
                "cm" => $"{_A * 100:0.0} cm × {_B * 100:0.0} cm × {_C * 100:0.0} cm",
                "mm" => $"{_A * 1000:0} mm × {_B * 1000:0} mm × {_C * 1000:0} mm",
                null => $"{_A:0.000} m × {_B:0.000} m × {_C:0.000} m",
                _ => throw new FormatException("Podano błędny format miary!"),
            };
        }

        public override string ToString() => $"{_A:0.000} m × {_B:0.000} m × {_C:0.000} m";

        
        #endregion

        #region zadanie 5        
        /// <summary>
        /// Oblicza objętość pudełka, w przypadku jednostki innej niż metry konwertuje na metry
        /// </summary>
        /// <value>
        /// Objętość pudełka w metrach sześciennych (m^3)
        /// </value>
        public double Volume {
            get {
                double a, b, c;
                if (Unit != UnitOfMeasure.meter)
                {
                    a = ConvertToMeters(_A, Unit);
                    b = ConvertToMeters(_B, Unit);
                    c = ConvertToMeters(_C, Unit);
                }
                else
                {
                    a = _A;
                    b = _B;
                    c = _C;
                }
                return Math.Round(a * b * c, 9);
            } }
        #endregion

        #region zadanie 6
        /// <summary>
        /// Oblicza pole pudełka, w przypadku jednostki innej niż metry konwertuje na metry
        /// </summary>
        /// <value>
        /// Pole pudełka w metrach kwadratowych (m^2)
        /// </value>
        public double Field
        {
            get
            {
                double a, b, c;
                if (Unit != UnitOfMeasure.meter)
                {
                    a = ConvertToMeters(_A, Unit);
                    b = ConvertToMeters(_B, Unit);
                    c = ConvertToMeters(_C, Unit);
                }
                else
                {
                    a = _A;
                    b = _B;
                    c = _C;
                }
                return Math.Round((2 * a) * (2 * b) * (2 * c), 6);
            }
        }
        #endregion

        #region zadanie 7
        bool IEquatable<Box>.Equals(Box other)
        {
            return this.Equals(other);
        }

        public bool Equals(Box other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.Unit == UnitOfMeasure.meter && Unit == UnitOfMeasure.meter)
            {
                return GetA() == other.GetA() && GetB() == other.GetB() && GetC() == other.GetC();
            }
            else if (other.Unit == UnitOfMeasure.meter)
            {
                var tempA = ConvertToMeters(GetA(), Unit);
                var tempB = ConvertToMeters(GetB(), Unit);
                var tempC = ConvertToMeters(GetC(), Unit);

                return tempA == other.GetA() && tempB == other.GetB() && tempC == other.GetC();
            }
            else if (Unit == UnitOfMeasure.meter)
            {
                var tempA = ConvertToMeters(other.GetA(), other.Unit);
                var tempB = ConvertToMeters(other.GetB(), other.Unit);
                var tempC = ConvertToMeters(other.GetC(), other.Unit);

                return tempA == GetA() && tempB == GetB() && tempC == GetC();
            }
            else
            {
                var tempA = ConvertToMeters(GetA(), Unit);
                var tempB = ConvertToMeters(GetB(), Unit);
                var tempC = ConvertToMeters(GetC(), Unit);
                var tempOtherA = ConvertToMeters(other.GetA(), other.Unit);
                var tempOtherB = ConvertToMeters(other.GetB(), other.Unit);
                var tempOtherC = ConvertToMeters(other.GetC(), other.Unit);

                return tempA == tempOtherA && tempB == tempOtherB && tempC == tempOtherC;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is Box) return Equals((Box)obj);
            else return false;
        }

        public static bool Equals(Box obj1, Box obj2)
        {
            if (obj1 is null && obj2 is null) return true;
            if (obj1 is null || obj2 is null) return false;

            return obj1.Equals(obj2);
        }

        public override int GetHashCode() => (GetA(), GetB(), GetC(), Unit).GetHashCode();

        /// <summary>
        /// Przeciążenie operatora porównania
        /// </summary>
        /// <param name="box1">Pierwsze pudełko</param>
        /// <param name="box2">Drugie pudełko</param>
        /// <returns>
        /// Czy pudełka są równe
        /// </returns>
        public static bool operator ==(Box box1, Box box2) => Equals(box1, box2);
        /// <summary>
        /// Przeciążenie operatora nierówności
        /// </summary>
        /// <param name="box1">Pierwsze pudełko</param>
        /// <param name="box2">Drugie pudełko</param>
        /// <returns>
        /// Czy pudełka nie są równe
        /// </returns>
        public static bool operator !=(Box box1, Box box2) => !(box1 == box2);
        #endregion

        #region zadanie 8        
        /// <summary>
        /// Przeciążenie operatora dodawania pudełek
        /// </summary>
        /// <param name="box1">Pierwsze pudełko</param>
        /// <param name="box2">Drugie pudełko</param>
        /// <returns>
        /// Pudełko o najmniejszych możliwych krawędziach które pomieści oba pudełka
        /// </returns>
        public static Box operator +(Box box1, Box box2)
        {
            var firstBoxEdges = new double[] { box1.GetA(), box1.GetB(), box1.GetC() }.OrderByDescending(edge => edge).ToArray();
            var secondBoxEdges = new double[] { box2.GetA(), box2.GetB(), box2.GetC() }.OrderByDescending(edge => edge).ToArray();
            var a = firstBoxEdges[0] >= secondBoxEdges[0] ? firstBoxEdges[0] : secondBoxEdges [0];
            var b = firstBoxEdges[1] >= secondBoxEdges[1] ? firstBoxEdges[1] : secondBoxEdges[1];
            var c = firstBoxEdges[2] + secondBoxEdges[2];
            return new Box(a, b, c);
        }
        #endregion

        #region zadanie 9        
        /// <summary>
        /// Konwersja jawna z <see cref="Box"/> na <see cref="System.Double[]"/>.
        /// </summary>
        /// <param name="box">Pudełko</param>
        /// <returns>
        /// Tablica długości krawędzi pudełka
        /// </returns>
        public static explicit operator double[](Box box)
        {
            return new double[] { box.GetA(), box.GetB(), box.GetC() };
        }

        /// <summary>
        /// Konwersja niejawna z <see cref="ValueTuple{System.Int32, System.Int32, System.Int32}"/> na <see cref="Box"/>.
        /// </summary>
        /// <param name="values">Długości wyrażone w minimetrach</param>
        /// <returns>
        /// Pudełko
        /// </returns>
        public static implicit operator Box(ValueTuple<int, int, int> values)
        {
            return new Box(values.Item1, values.Item2, values.Item3, UnitOfMeasure.milimeter);
        }
        #endregion

        #region zadanie 10
        public double this[int i]
        {
            get
            {
                return i switch
                {
                    0 => GetA(),
                    1 => GetB(),
                    2 => GetC(),
                    _ => throw new ArgumentOutOfRangeException(i.ToString(), "Pudełko nie ma tyle wartości!"),
                };
            }
        }
        #endregion

        #region zadanie 11
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<double> GetEnumerator()
        {
            foreach (var parameter in _Edges)
            {
                yield return parameter;
            }
        }
        #endregion

        #region zadanie 12
        public static Box Parse(string expression)
        {
            if (expression is null) throw new FormatException();
            var expressionArray = expression.Split(' ');
            if (expressionArray.Length != 8) throw new FormatException();
            var unit = expressionArray.Last() switch
            {
                "m" => UnitOfMeasure.meter,
                "cm" => UnitOfMeasure.centimeter,
                "mm" => UnitOfMeasure.milimeter,
                _ => throw new FormatException("Podano błędną jednostkę!"),
            };
            if (!double.TryParse(expressionArray[0], out var a))
                throw new FormatException("Podano błędny pierwszy parametr!");
            if (!double.TryParse(expressionArray[3], out var b))
                throw new FormatException("Podano błędny drugi parametr!");
            if (!double.TryParse(expressionArray[6], out var c))
                throw new FormatException("Podano błędny trzeci parametr!");

            return new Box(a, b, c, unit);
        }
        #endregion

        #region zadanie 13
        // Pamiętaj o zapewnieniu pełnej niezmienniczości obiektom klasy Pudelko oraz o zapieczętowaniu klasy.
        #endregion

        #region zadanie 14
        // Utwórz testy jednostkowe (unit tests) 
        #endregion

        #region zadanie 15
        // Metoda rozszerzająca
        #endregion

        #region zadanie 16

        public static void Comparison<Box>(List<Box> boxList, Comparison<Box> comparison)
        {
            int n = boxList.Count;

            do
            {
                for (int i = 0; i < n - 1; i++)
                {
                    if (comparison(boxList[i], boxList[i + 1]) > 0)
                        SwapElements(boxList, i, i + 1);
                }
                n--;
            } while (n > 1);
        }

        private static void SwapElements<Box>(List<Box> boxList, int firstIndex, int secondIndex)
        {
            Contract.Requires(boxList != null);
            Contract.Requires(firstIndex >= 0 && firstIndex < boxList.Count);
            Contract.Requires(secondIndex >= 0 && secondIndex < boxList.Count);
            if (firstIndex == secondIndex)
                return;

            (boxList[secondIndex], boxList[firstIndex]) = (boxList[firstIndex], boxList[secondIndex]);
        }
        #endregion
    }
}