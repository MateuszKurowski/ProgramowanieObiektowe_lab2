using System;

namespace Pojazdy
{
    public abstract class BaseVahicle : IVehicle
    {
        #region Enums
        /// <summary>
        /// Stan pojazdu
        /// </summary>
        public enum State
        {
            Stoi,
            Jedzie
        }

        /// <summary>
        /// Środowisko pojazdu
        /// </summary>
        public enum Environment
        {
            Ziemia,
            Powietrze,
            Woda,
            ZiemiaWoda,
            PowietrzeWoda
        }

        /// <summary>
        /// Limity prędkości obowiązujące w poszczególnych środowiskach wyrażone w kilometrach na godzine (km/h)
        /// </summary>
        public static class EnvironmentLimitSpeedKmPerH
        {
            public static double ZiemiaMin { get; } = 1;
            public static double ZiemiaMax { get; } = 350;
            public static double WodaMin { get; } = Math.Round(1.6093123, 2);
            public static double WodaMax { get; } = Math.Round(64.372492, 2);
            public static double PowietrzeMin { get; } = 72;
            public static double PowietrzeMax { get; } = 720;
        }

        /// <summary>
        /// Typ paliwa
        /// </summary>
        public enum TypeOfFuel
        {
            Benzyna,
            Olej,
            LPG,
            Prad,
            Diesel,
            Brak
        }

        /// <summary>
        /// Jednostki prędkości
        /// </summary>
        public static class SpeedUnits
        {
            public const string
                KilometryNaGodzine = "km/h",
                MileMorskie = "Węzeł",
                MetryNaSekunde = "m/s";
        }
        #endregion

        private State state = State.Stoi;

        protected abstract string SpeedUnit { get; set; }
        protected abstract double MinSpeed { get; set; }
        protected abstract double MaxSpeed { get; set; }
        private double _Speed;

        public abstract Environment environment { get; }

        public void SetSpeed(int speed)
        {
            if (speed > MinSpeed && speed < MaxSpeed)
            {
                if (speed > _Speed)
                    Console.WriteLine("Przyśpieszam..");
                else if (speed < MinSpeed)
                    Console.WriteLine("Zwalniam..");
                else
                    return;
                _Speed = speed;
            }
            else
            {
                Console.WriteLine($"Podana prędkość ({speed}) jest dostępna w tym środowisku. Proszę stosować się do limitów: ");
                Console.WriteLine();
                CheckTheSpeedLimitsOfTheEnvironment();
                Console.WriteLine();
            }
        }

        public void Move()
        {
            if (state == State.Jedzie)
                return;
            SetState(State.Jedzie);
            SetSpeed(MinSpeed);
        }

        public void Stop()
        {
            if (state == State.Stoi)
                return;
            SetState(State.Stoi);
            _Speed = 0;
        }

        private void SetState(State state)
        {
            this.state = state;
        }

        public void CheckTheSpeedLimitsOfTheEnvironment()
        {
            Console.WriteLine("--- Speed Limits ---");
            Console.WriteLine($"Min speed is: {MinSpeed}");
            Console.WriteLine($"Max speed is: {MaxSpeed}");
        }

        public bool IsEngine { get; init; }

        public int? EnginePower { get; init; }

        public string Mark { get; init; }

        public TypeOfFuel Fuel { get; init; }

        public BaseVahicle(string mark, bool isEngine, int? enginePower = null, TypeOfFuel typeOfFuel = TypeOfFuel.Brak)
        {
            if (isEngine && (enginePower <= 0 || typeOfFuel == TypeOfFuel.Brak))
            {
                throw new Exception("W przypadku urządzeń napędzanym silnikiem należy uzupełnić moc silnika oraz typ paliwa.");
            }
            Mark = mark;
            IsEngine = isEngine;
            EnginePower = enginePower;
            Fuel = typeOfFuel;
        }
    }
}