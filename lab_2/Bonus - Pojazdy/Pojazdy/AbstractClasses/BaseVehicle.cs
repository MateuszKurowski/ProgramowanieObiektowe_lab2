using System;

namespace Pojazdy
{
    public abstract class BaseVehicle : IVehicle
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
                MileMorskie = "mph (Węzły)",
                MetryNaSekunde = "m/s";
        }
        #endregion

        public virtual State VehicleState { get; protected set; } = State.Stoi;

        public string SpeedUnit { get; protected set; }
        public double MinSpeed { get; protected set; }
        public double MaxSpeed { get; protected set; }
        public double MainSpeed
        {
            get
            {
                switch (environment)
                {
                    case Environment.Ziemia:
                        {
                            return GetSpeed();
                        }
                    case Environment.Woda:
                        {
                            return WaterWehicle.ConvertMilesPerHourToKilometeresPerHour(GetSpeed());
                        }
                    case Environment.Powietrze:
                        {
                            return AirVehicle.ConvertMetersPerSecondToKilometeresPerHour(GetSpeed());
                        }
                    default:
                        throw new Exception("Napotkano nieznany błąd. Skontaktuj się z administratorem.");
                }
            }
        }

        private double _Speed;
        public Environment environment { get; protected set; }

        public double GetSpeed() => _Speed;

        public void SetSpeed(double speed)
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
                Console.WriteLine($"Podana prędkość ({speed}) NIE jest dostępna w tym środowisku. Proszę stosować się do limitów: ");
                Console.WriteLine();
                CheckTheSpeedLimitsOfTheEnvironment();
                Console.WriteLine();
            }
        }

        public virtual void Start()
        {
            if (VehicleState == State.Jedzie)
                return;
            VehicleState = State.Jedzie;
            SetSpeed(MinSpeed);
        }

        public virtual void Stop()
        {
            if (VehicleState == State.Stoi)
                return;
            VehicleState = State.Stoi;
            _Speed = 0;
        }

        protected void SetStop() => _Speed = 0;

        public void CheckTheSpeedLimitsOfTheEnvironment()
        {
            Console.WriteLine("--- Ograniczenia prędkości ---");
            Console.WriteLine($"Minimalna prędkość to: {MinSpeed}");
            Console.WriteLine($"Maksymalna prędkość to: {MaxSpeed}");
        }

        public bool IsEngine { get; init; }

        public int? EnginePower { get; init; }

        public string Mark { get; init; }

        public TypeOfFuel Fuel { get; init; }

        protected BaseVehicle(string mark, bool isEngine, int? enginePower = null, TypeOfFuel typeOfFuel = TypeOfFuel.Brak)
        {
            if (isEngine && (enginePower is null || enginePower <= 0 || typeOfFuel == TypeOfFuel.Brak))
            {
                throw new Exception("W przypadku urządzeń napędzanym silnikiem należy uzupełnić moc silnika oraz typ paliwa.");
            }
            Mark = mark;
            IsEngine = isEngine;
            EnginePower = enginePower;
            Fuel = typeOfFuel;
        }

        protected void SetEnvironment(Environment environmentToSet, double? speed = null)
        {
            switch (environmentToSet)
            {
                case Environment.Ziemia:
                    {
                        environment = Environment.Ziemia;
                        MinSpeed = EnvironmentLimitSpeedKmPerH.ZiemiaMin;
                        MaxSpeed = EnvironmentLimitSpeedKmPerH.ZiemiaMax;
                        SpeedUnit = SpeedUnits.KilometryNaGodzine;
                        if (speed != null)
                        {
                            if (speed > MinSpeed && speed < MaxSpeed)
                                SetSpeed((double)speed);
                            else throw new ArgumentOutOfRangeException(nameof(speed));
                        }
                    }break;
                case Environment.Woda:
                    {
                        environment = Environment.Woda;
                        MinSpeed = WaterWehicle.ConvertKilometeresPerHourToMilesPerHour(EnvironmentLimitSpeedKmPerH.WodaMin);
                        MaxSpeed = WaterWehicle.ConvertKilometeresPerHourToMilesPerHour(EnvironmentLimitSpeedKmPerH.WodaMax);
                        SpeedUnit = SpeedUnits.MileMorskie;
                        if (speed != null)
                        {
                            if (speed > MinSpeed && speed < MaxSpeed)
                                SetSpeed((double)speed);
                            else throw new ArgumentOutOfRangeException(nameof(speed));
                        }
                    }
                    break;
                case Environment.Powietrze:
                    {
                        environment = Environment.Powietrze;
                        MinSpeed = AirVehicle.ConvertKilometeresPerHourToMetersPerSecond(EnvironmentLimitSpeedKmPerH.WodaMin);
                        MaxSpeed = AirVehicle.ConvertKilometeresPerHourToMetersPerSecond(EnvironmentLimitSpeedKmPerH.WodaMax);
                        SpeedUnit = SpeedUnits.MetryNaSekunde;
                        if (speed != null)
                        {
                            if (speed > MinSpeed && speed < MaxSpeed)
                                SetSpeed((double)speed);
                            else throw new ArgumentOutOfRangeException(nameof(speed));
                        }
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}