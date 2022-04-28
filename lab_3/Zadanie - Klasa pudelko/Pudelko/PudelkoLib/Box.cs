using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace BoxLib
{
    public enum UnitOfMeasure
    {
        milimeter,
        centimeter,
        meter
    }

    public sealed class Box : IFormattable//, IEquatable<Box>, IEnumerable
    {
        #region zadanie 1
        private readonly double _A;
        private readonly double _B;
        private readonly double _C;

        /// <summary>
        /// Wysokość pudełka
        /// </summary>
        public double A
        {
            get { return _A; }
            init { _A = ValidateLengthBeforeSave(value, "A"); }
        }

        /// <summary>
        /// Szerokość pudełka
        /// </summary>
        public double B
        {
            get { return _B; }
            init { _B = ValidateLengthBeforeSave(value, "B"); }
        }

        /// <summary>
        /// Długość pudełka
        /// </summary>
        public double C
        {
            get { return _C; }
            init { _C = ValidateLengthBeforeSave(value, "C"); }
        }

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

        private double ConvertToMeters(double value, UnitOfMeasure unit)
        {
            return Unit switch
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
            if (string.IsNullOrEmpty(format)) throw new FormatException("Podano błędny format miary!");
            if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;

            return format.ToLower() switch
            {
                "m" => $"{A:0.000} m × {B:0.000} m × {C:0.000} m",
                "cm" => $"{A * 100:0.0} cm × {B * 100:0.0} cm × {C * 100:0.0} cm",
                "mm" => $"{A * 1000:0.0} mm × {B * 1000:0.0} mm × {C * 1000} mm",
                _ => throw new FormatException("Podano błędny format miary!"),
            };
        }
        public string ToString(string format)
        {
            return format?.ToLower() switch
            {
                "m" => $"{A:0.000} m × {B:0.000} m × {C:0.000} m",
                "cm" => $"{A * 100:0.0} cm × {B * 100:0.0} cm x {C * 100:0.0} cm",
                "mm" => $"{A * 1000:0.0} mm × {B * 1000:0.0} mm × {C * 1000} mm",
                _ => throw new FormatException("Podano błędny format miary!"),
            };
        }

        public override string ToString() => $"{A:0.000} m × {B:0.000} m × {C:0.000} m";
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
                    a = ConvertToMeters(A, Unit);
                    b = ConvertToMeters(B, Unit);
                    c = ConvertToMeters(C, Unit);
                }
                else
                {
                    a = A;
                    b = B;
                    c = C;
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
                    a = ConvertToMeters(A, Unit);
                    b = ConvertToMeters(B, Unit);
                    c = ConvertToMeters(C, Unit);
                }
                else
                {
                    a = A;
                    b = B;
                    c = C;
                }
                return Math.Round((2 * a) * (2 * b) * (2 * c), 6);
            }
        }
        #endregion

        #region zadanie 7
        #endregion

        #region zadanie 8
        #endregion

        #region zadanie 9
        #endregion

        #region zadanie 10
        #endregion

        #region zadanie 11
        #endregion

        #region zadanie 12
        #endregion

        #region zadanie 13
        #endregion

        #region zadanie 14
        #endregion

        #region zadanie 15
        #endregion

        #region zadanie 16
        #endregion
    }
}