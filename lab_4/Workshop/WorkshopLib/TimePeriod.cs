using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopLib
{
    /// <summary>
    /// Przedział czasu
    /// </summary>
    /// <seealso cref="System.IEquatable&lt;WorkshopLib.TimePeriod&gt;" />
    /// <seealso cref="System.IComparable&lt;WorkshopLib.TimePeriod&gt;" />
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        #region Declarations
        private readonly long _time;

        /// <summary>
        /// Zewnętrzna deklaracja h:mm:ss
        /// </summary>
        public string ExternalRepresentation
        {
            get
            {
                var leftTime = Time;
                var tempTime = leftTime / 3600;
                var hours = tempTime.ToString("D1")?.Split('.')[0];
                leftTime -= (long.Parse(hours) * 3600);
                tempTime = leftTime / 60;
                var minutes = tempTime.ToString("D1")?.Split('.')[0];
                leftTime -= (long.Parse(minutes) * 60);
                var seconds = leftTime.ToString("D1");

                return $"{hours}:{minutes}:{seconds}";
            }
        }

        /// <summary>
        /// Tablica z członami czasu w postaci double
        /// </summary>
        public double[] TimePeriodValues
        {
            get
            {
                var leftTime = Time;
                var tempTime = leftTime / 3600;
                var hours = tempTime.ToString("D1")?.Split('.')[0];
                leftTime -= (long.Parse(hours) * 3600);
                tempTime = leftTime / 60;
                var minutes = tempTime.ToString("D1")?.Split('.')[0];
                leftTime -= (long.Parse(minutes) * 60);
                var seconds = leftTime.ToString("D1");

                return new double[3] { Convert.ToDouble(hours), Convert.ToDouble(minutes), Convert.ToDouble(seconds) };
            }
        }

        /// <summary>
        /// Czas w sekundach
        /// </summary>
        public long Time
        {
            get => _time;
            init
            {
                if (value < 0) throw new ArgumentOutOfRangeException("Time");
                _time = value;
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Inicjalizuje nową instancje struktury <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="seconds">Sekundy</param>
        public TimePeriod(long seconds) : this()
        {
            Time = seconds;
        }

        /// <summary>
        /// Inicjalizuje nową instancje struktury <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="hours">Godziny</param>
        /// <param name="minutes">Minuty</param>
        public TimePeriod(long hours, long minutes) : this()
        {
            if (hours < 0 || minutes < 0) throw new ArgumentOutOfRangeException("Podano ujemny czas!");

            Time = CalculateSeconds(hours, minutes);
        }

        /// <summary>
        /// Inicjalizuje nową instancje struktury <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="hours">Godziny</param>
        /// <param name="minutes">Minuty</param>
        /// <param name="seconds">Sekundy</param>
        public TimePeriod(long hours, long minutes, long seconds) : this()
        {
            if (hours < 0 || minutes < 0 || seconds < 0) throw new ArgumentOutOfRangeException("Podano ujemny czas!");

            Time = CalculateSeconds(hours, minutes, seconds);
        }

        /// <summary>
        /// Inicjalizuje nową instancje struktury <see cref="TimePeriod"/>.
        /// </summary>
        /// <param name="time">Czas podany w formacie hh:mm:ss</param>
        /// <exception cref="System.ArgumentException">Podaną nieobsługiwany format czasu. Proszę podać format hh:mm:ss!</exception>
        public TimePeriod(string time) : this()
        {
            var timeTab = time.Split(':');
            if (timeTab.Length != 3 || !long.TryParse(timeTab[0], out var hours) || !long.TryParse(timeTab[1], out var minutes) || !long.TryParse(timeTab[2], out var seconds))
                throw new ArgumentException("Podaną nieobsługiwany format czasu. Proszę podać format h:mm:ss!");
            if (hours < 0 || minutes < 0 || seconds < 0) throw new ArgumentOutOfRangeException("Podano ujemny czas!");

            Time = CalculateSeconds(hours, minutes, seconds);
        }
        #endregion

        /// <summary>
        /// Przeciążenie metody ToString
        /// </summary>
        /// <returns>
        /// <see cref="System.String" /> pokazujący godzinę.
        /// </returns>
        public override string ToString()
        {
            if (Time == 0) return "0:00:00";

            var leftTime = Time;
            var tempTime = leftTime / 3600;
            var hours = tempTime.ToString("D1")?.Split('.')[0];
            leftTime -= (long.Parse(hours) * 3600);
            tempTime = leftTime / 60;
            var minutes = tempTime.ToString("D2")?.Split('.')[0];
            leftTime -= (long.Parse(minutes) * 60);
            var seconds = leftTime.ToString("D2");

            return $"{hours}:{minutes}:{seconds}";
        }

        #region Mathematic
        /// <summary>
        /// Przeciążanie operatora dodawania
        /// </summary>
        public static TimePeriod operator +(TimePeriod timePeriod1, TimePeriod timePeriod2) => new(timePeriod1.Time + timePeriod2.Time);

        /// <summary>
        /// Przeciążanie operatora odejmowania
        /// </summary>
        public static TimePeriod operator -(TimePeriod timePeriod1, TimePeriod timePeriod2) => new(Math.Abs(timePeriod1.Time - timePeriod2.Time));

        /// <summary>
        /// Dodaje czas
        /// </summary>
        /// <param name="otherTimePeriod">Obiekt przedziału czasu do dodania</param>
        /// <returns>Nową strukturę przedziału czasu do której dodano przedział czasu.</returns>
        public TimePeriod Plus(TimePeriod otherTimePeriod) => this + otherTimePeriod;

        /// <summary>
        /// Dodaje czas
        /// </summary>
        /// <param name="timePeriod1">Obiekt przedziału czasu</param>
        /// <param name="timePeriod2">Obiekt przedziału czasu</param>
        /// <returns>Nową strukturę przedziału czasu do której dodano przedział czasu.</returns>
        public static TimePeriod Plus(TimePeriod timePeriod1, TimePeriod timePeriod2) => timePeriod1 + timePeriod2;

        /// <summary>
        /// Odejmuje czas
        /// </summary>
        /// <param name="otherTimePeriod">Obiekt przedziału czasu do odjęcia</param>
        /// <returns>Nową strukturę przedziału czasu od której odjęto przedział czasu.</returns>
        public TimePeriod Minus(TimePeriod otherTimePeriod) => this - otherTimePeriod;

        /// <summary>
        /// Odejmuje czas
        /// </summary>
        /// <param name="timePeriod1">Obiekt przedziału czasu</param>
        /// <param name="timePeriod2">Obiekt przedziału czasu</param>
        /// <returns>Nową strukturę przedziału czasu od której odjęto przedział czasu.</returns>
        public static TimePeriod Minus(TimePeriod timePeriod1, TimePeriod timePeriod2) => timePeriod1 - timePeriod2;

        /// <summary>
        /// Zwielokrotnienie przedziału czasu o podany mnożnik
        /// </summary>
        /// <param name="timePeriod">Obiekt przedziału czasu.</param>
        /// <param name="multiplier">Mnożnik.</param>
        /// <returns>
        /// Nową strukturę zwięszkoną o mnożnik
        /// </returns>
        public static TimePeriod operator *(TimePeriod timePeriod, int multiplier) => new(timePeriod.Time * multiplier);

        /// <summary>
        /// Zwielokrotnienie przedziału czasu o podany mnożnik
        /// </summary>
        /// <param name="multiplier">Mnożnik.</param>
        /// <returns>
        /// Nową strukturę zwięszkoną o mnożnik
        /// </returns>
        public TimePeriod Multiply(int multiplier) => this * multiplier;
        #endregion

        private static long CalculateSeconds(long hours = 0, long minutes = 0, long seconds = 0) => (hours * 3600) + (minutes * 60) + seconds;

        #region Equals
        /// <summary>
        /// Porównanie z innym obiektem przedziału czasu
        /// </summary>
        /// <param name="other">Obiekt przedziału czasu do porównania</param>
        public bool Equals(TimePeriod other)
        {
            if (ReferenceEquals(this, other)) return true;
            return TimePeriodValues[0] == other.TimePeriodValues[0] && TimePeriodValues[1] == other.TimePeriodValues[1] && TimePeriodValues[2] == other.TimePeriodValues[2];
        }

        /// <summary>
        /// Porównanie z innym obiektem
        /// </summary>
        /// <param name="obj">Obiekt do porównania</param>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is TimePeriod) return Equals((TimePeriod)obj);
            else return false;
        }

        /// <summary>
        /// Porównanie z innym obiektem przedziału czasu
        /// </summary>
        /// <param name="other">Obiekt czasu do porównania</param>
        bool IEquatable<TimePeriod>.Equals(TimePeriod other) => Equals(other);

        /// <summary>
        /// Porówuje obiekty przedziału czasu z innym obiektem czasu
        /// </summary>
        /// <param name="obj1">Obbiekt przedziału czasu do porównania</param>
        /// <param name="obj2">Obbiekt przedziału czasu do porównania</param>
        public static bool Equals(TimePeriod obj1, TimePeriod obj2) => obj1.Equals(obj2);

        /// <summary>
        /// Przeciążenie operatora porównania
        /// </summary>
        public static bool operator ==(TimePeriod t1, TimePeriod t2) => Equals(t1, t2);

        /// <summary>
        /// Przeciążenie operatora nierówności
        /// </summary>
        public static bool operator !=(TimePeriod t1, TimePeriod t2) => !(t1 == t2);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => (Time).GetHashCode();
        #endregion

        #region CompareTo
        /// <summary>
        /// Sprawdza który obiekt jest większy czasowo.
        /// </summary>
        int IComparable<TimePeriod>.CompareTo(TimePeriod other) => CompareTo(other);

        /// <summary>
        /// Sprawdza który obiekt jest większy czasowo.
        /// </summary>
        public int CompareTo(TimePeriod other)
        {
            if (Equals(other)) return 0;
            if (TimePeriodValues[0] != other.TimePeriodValues[0]) return TimePeriodValues[0].CompareTo(other.TimePeriodValues[0]);
            if (TimePeriodValues[1] != other.TimePeriodValues[1]) return TimePeriodValues[1].CompareTo(other.TimePeriodValues[1]);
            return TimePeriodValues[2].CompareTo(other.TimePeriodValues[2]);
        }

        /// <summary>
        /// Przeciążenie operatora większości
        /// </summary>
        public static bool operator >(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) == 1 ? true : false;

        /// <summary>
        /// Przeciążenie operatora większy lub równy
        /// </summary>
        public static bool operator >=(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) >= 0 ? true : false;

        /// <summary>
        /// Przeciążenie operatora mniejszości
        /// </summary>
        public static bool operator <(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) == -1 ? true : false;

        /// <summary>
        /// Przeciążenie operatora mniejszy lub równy
        /// </summary>
        public static bool operator <=(TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) <= 0 ? true : false;
        #endregion
    }
}