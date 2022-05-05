using System;

namespace WorkshopLib
{
    /// <summary>
    /// Czas
    /// </summary>
    /// <seealso cref="System.IEquatable&lt;WorkshopLib.Time&gt;" />
    /// <seealso cref="System.IComparable&lt;WorkshopLib.Time&gt;" />
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        #region Declarations
        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;

        /// <summary>
        /// Godzina
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Hours</exception>
        public byte Hours
        {
            get => _hours;
            init
            {
                if (value is > 24 or < 0)
                    throw new ArgumentOutOfRangeException("Hours");
                _hours = value;
            }
        }

        /// <summary>
        /// Minuta
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Minutes</exception>
        public byte Minutes
        {
            get => _minutes;
            init
            {
                if (value is > 60 or < 0)
                    throw new ArgumentOutOfRangeException("Minutes");
                _minutes = value;
            }
        }

        /// <summary>
        /// Sekunda
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Seconds</exception>
        public byte Seconds
        {
            get => _seconds;
            init
            {
                if (value is > 60 or < 0)
                    throw new ArgumentOutOfRangeException("Seconds");
                _seconds = value;
            }
        }

        /// <summary>
        /// Tablica z członami czasu w postaci double
        /// </summary>
        public double[] TimeValues
        {
            get => new double[3] { Hours, Minutes, Seconds };
        }
        #endregion

        #region Constructors        
        /// <summary>
        /// Inicjalizuje nową instancje struktury <see cref="Time"/>.
        /// </summary>
        /// <param name="hours">Godziny</param>
        public Time(byte hours) : this()
        {
            Minutes = 0;
            Seconds = 0;
            Hours = hours;
        }

        /// <summary>
        /// Inicjalizuje nową instancje struktury <see cref="Time"/>.
        /// </summary>
        /// <param name="hours">Godziny</param>
        /// <param name="minutes">Minuty</param>
        public Time(byte hours, byte minutes) : this()
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = 0;
        }

        /// <summary>
        /// Inicjalizuje nową instancje struktury <see cref="Time"/>.
        /// </summary>
        /// <param name="hours">Godziny</param>
        /// <param name="minutes">Minuty</param>
        /// <param name="seconds">Sekundy</param>
        public Time(byte hours, byte minutes, byte seconds) : this()
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        /// <summary>
        /// Inicjalizuje nową instancje struktury <see cref="Time"/>.
        /// </summary>
        /// <param name="time">Czas podany w formacie hh:mm:ss</param>
        /// <exception cref="System.ArgumentException">Podaną nieobsługiwany format czasu. Proszę podać format hh:mm:ss!</exception>
        public Time(string time) : this()
        {
            var timeTab = time.Split(':');
            if (timeTab.Length != 3 || !byte.TryParse(timeTab[0], out var hours) || !byte.TryParse(timeTab[1], out var minutes) || !byte.TryParse(timeTab[2], out var seconds))
                throw new ArgumentException("Podaną nieobsługiwany format czasu. Proszę podać format hh:mm:ss!");

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }
        #endregion

        /// <summary>
        /// Przeciążenie metody ToString
        /// </summary>
        /// <returns>
        /// <see cref="System.String" /> pokazujący godzinę.
        /// </returns>
        public override string ToString() => $"{Hours:00}:{Minutes:00}:{Seconds:00}";

        #region Operators        
        /// <summary>
        /// Dodaje czas
        /// </summary>
        /// <param name="time1">Pierwszy parametr czasu.</param>
        /// <param name="time2">Drugi parametr czasu.</param>
        /// <returns>
        /// Nową strukturę sumującą oba parametry.
        /// </returns>
        public static Time operator +(Time time1, Time time2)
        {
            var temp = time1.TimeValues[2] + time2.TimeValues[2];
            var seconds = temp % 60;

            var minutes = Math.Truncate(temp / 60);
            temp = minutes + time1.TimeValues[1] + time2.TimeValues[1];
            minutes = temp % 60;

            var hours = Math.Truncate(temp / 60);
            temp = hours + time1.TimeValues[0] + time2.TimeValues[0];
            hours = temp % 24;

            //var days = Math.Truncate(temp / 24);
            //var allHours = temp;

            return new(byte.Parse(hours.ToString()), byte.Parse(minutes.ToString()), byte.Parse(seconds.ToString()));
        }

        /// <summary>
        /// Odejmuje czas
        /// </summary>
        /// <param name="time1">Pierwszy parametr czasu.</param>
        /// <param name="time2">Drugi parametr czasu.</param>
        /// <returns>
        /// Nową strukturę odejmując pierwszy parametr od drugiego.
        /// </returns>
        public static Time operator -(Time time1, Time time2)
        {
            var temp = Math.Abs(time1.TimeValues[2] - time2.TimeValues[2]);
            var seconds = temp % 60;

            var minutes = Math.Truncate(temp / 60);
            temp = Math.Abs(minutes + time1.TimeValues[1] + time2.TimeValues[1]);
            minutes = temp % 60;

            var hours = Math.Truncate(temp / 60);
            temp = Math.Abs(hours + time1.TimeValues[0] + time2.TimeValues[0]);
            hours = temp % 24;

            //var days = Math.Truncate(temp / 24);
            //var allHours = temp;

            return new(byte.Parse(hours.ToString()), byte.Parse(minutes.ToString()), byte.Parse(seconds.ToString()));
        }

        /// <summary>
        /// Dodaje czas
        /// </summary>
        /// <param name="timePeriod">Parametr czasu do dodania</param>
        /// <returns>Nową strutkurę czasu</returns>
        public Time Plus(TimePeriod timePeriod) => Time.Plus(this, timePeriod);

        /// <summary>
        /// Pluses the specified time.
        /// </summary>
        /// <param name="time">Obiekt czasu</param>
        /// <param name="timePeriod">Przedział czasu do dodania</param>
        /// <returns>Nową strukturę czasu do której dodano przedział</returns>
        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            var temp = time.TimeValues[2] + timePeriod.TimePeriodValues[2];
            var seconds = temp % 60;

            var minutes = Math.Truncate(temp / 60);
            temp = minutes + time.TimeValues[1] + timePeriod.TimePeriodValues[1];
            minutes = temp % 60;

            var hours = Math.Truncate(temp / 60);
            temp = hours + time.TimeValues[0] + timePeriod.TimePeriodValues[0];
            hours = temp % 24;

            //var days = Math.Truncate(temp / 24);
            //var allHours = temp;

            return new(byte.Parse(hours.ToString()), byte.Parse(minutes.ToString()), byte.Parse(seconds.ToString()));
        }

        /// <summary>
        /// Odejmuje czas
        /// </summary>
        /// <param name="time">Obiekt czasu</param>
        /// <param name="timePeriod">Przedział czasu do odjęcia</param>
        /// <returns>Nową strukturę czasu od której odjęto przedział</returns>
        public static Time Minus(Time time, TimePeriod timePeriod)
        {
            var temp = Math.Abs(time.TimeValues[2] - timePeriod.TimePeriodValues[2]);
            var seconds = temp % 60;

            var minutes = Math.Truncate(temp / 60);
            temp = Math.Abs(minutes + time.TimeValues[1] + timePeriod.TimePeriodValues[1]);
            minutes = temp % 60;

            var hours = Math.Truncate(temp / 60);
            temp = Math.Abs(hours + time.TimeValues[0] + timePeriod.TimePeriodValues[0]);
            hours = temp % 24;

            //var days = Math.Truncate(temp / 24);
            //var allHours = temp;

            return new(byte.Parse(hours.ToString()), byte.Parse(minutes.ToString()), byte.Parse(seconds.ToString()));
        }

        /// <summary>
        /// Zwielokrotnienie czasu o podany mnożnik
        /// </summary>
        /// <param name="time">Obiekt czasu.</param>
        /// <param name="multiplier">Mnożnik.</param>
        /// <returns>
        /// Nową strukturę zwięszkoną o mnożnik
        /// </returns>
        public static Time operator *(Time time, int multiplier)
        {
            var temp = Math.Abs(time.TimeValues[2] * multiplier);
            var seconds = temp % 60;

            var minutes = Math.Truncate(temp / 60);
            temp = Math.Abs(minutes + time.TimeValues[1] * multiplier);
            minutes = temp % 60;

            var hours = Math.Truncate(temp / 60);
            temp = Math.Abs(hours + time.TimeValues[0] * multiplier);
            hours = temp % 24;

            //var days = Math.Truncate(temp / 24);
            //var allHours = temp;

            return new(byte.Parse(hours.ToString()), byte.Parse(minutes.ToString()), byte.Parse(seconds.ToString()));
        }
        #endregion

        #region Equals        
        /// <summary>
        /// Porównanie z innym obiektem czasu
        /// </summary>
        /// <param name="other">Obiekt czasu do porównania</param>
        public bool Equals(Time other)
        {
            if (ReferenceEquals(this, other)) return true;
            return TimeValues[0] == other.TimeValues[0] && TimeValues[1] == other.TimeValues[1] && TimeValues[2] == other.TimeValues[2];
        }

        /// <summary>
        /// Porównanie z innym obiektem
        /// </summary>
        /// <param name="obj">Obiekt do porównania</param>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is Time) return Equals((Time)obj);
            else return false;
        }

        /// <summary>
        /// Porównanie z innym obiektem czasu
        /// </summary>
        /// <param name="other">Obiekt czasu do porównania</param>
        bool IEquatable<Time>.Equals(Time other) => Equals(other);

        /// <summary>
        /// Porówuje obiekty czasu z innym obiektem czasu
        /// </summary>
        /// <param name="obj1">Obbiekt czasu do porównania</param>
        /// <param name="obj2">Obbiekt czasu do porównania</param>
        public static bool Equals(Time obj1, Time obj2) => obj1.Equals(obj2);

        /// <summary>
        /// Przeciążenie operatora porównania
        /// </summary>
        public static bool operator ==(Time t1, Time t2) => Equals(t1, t2);

        /// <summary>
        /// Przeciążenie operatora nierówności
        /// </summary>
        public static bool operator !=(Time t1, Time t2) => !(t1 == t2);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();
        #endregion

        #region CompareTo        
        /// <summary>
        /// Sprawdza który obiekt jest większy czasowo.
        /// </summary>
        int IComparable<Time>.CompareTo(Time other) => CompareTo(other);

        /// <summary>
        /// Sprawdza który obiekt jest większy czasowo.
        /// </summary>
        public int CompareTo(Time other)
        {
            if (Equals(other)) return 0;
            if (TimeValues[0] != other.TimeValues[0]) return TimeValues[0].CompareTo(other.TimeValues[0]);
            if (TimeValues[1] != other.TimeValues[1]) return TimeValues[1].CompareTo(other.TimeValues[1]);
            return TimeValues[2].CompareTo(other.TimeValues[2]);
        }

        /// <summary>
        /// Przeciążenie operatora większości
        /// </summary>
        public static bool operator >(Time t1, Time t2) => t1.CompareTo(t2) == 1 ? true : false;

        /// <summary>
        /// Przeciążenie operatora większy lub równy
        /// </summary>
        public static bool operator >=(Time t1, Time t2) => t1.CompareTo(t2) >= 0 ? true : false;

        /// <summary>
        /// Przeciążenie operatora mniejszości
        /// </summary>
        public static bool operator <(Time t1, Time t2) => t1.CompareTo(t2) == -1 ? true : false;

        /// <summary>
        /// Przeciążenie operatora mniejszy lub równy
        /// </summary>
        public static bool operator <=(Time t1, Time t2) => t1.CompareTo(t2) <= 0 ? true : false;
        #endregion
    }
}