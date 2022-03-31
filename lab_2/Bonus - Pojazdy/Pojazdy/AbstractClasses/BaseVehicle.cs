using System;

namespace Pojazdy
{
    /// <summary>
    /// Podstawowa klasa abstrakcyjna pojazdu
    /// </summary>
    public abstract class BaseVehicle : IVehicle
    {
        #region Constants
        /// <summary>
        /// Limity prędkości w środowiskach
        /// </summary>
        public static class EnvironmentLimitSpeedKmPerH
        {
            public static double ZiemiaMin { get; } = 1;
            public static double ZiemiaMax { get; } = 350;
            public static double WodaMin { get; } = ConvertersData.MileNaGodzine * 1;
            public static double WodaMax { get; } = ConvertersData.MileNaGodzine * 40;
            public static double PowietrzeMin { get; } = ConvertersData.MetryNaSekunde * 20;
            public static double PowietrzeMax { get; } = ConvertersData.MetryNaSekunde * 200;
        }

        /// <summary>
        /// Jednostki prędkości
        /// </summary>
        public static class SpeedUnits
        {
            public const string
                KilometryNaGodzine = "km/h",
                MileMorskie = "mph (Knot (Węzły))",
                MetryNaSekunde = "m/s";
        }
        #endregion

        #region Prędkości
        /// <summary>
        /// Jednostka prędkości
        /// </summary>
        public string SpeedUnit { get; protected set; }

        /// <summary>
        /// Minimalna prędkość w obecnym środowisku
        /// </summary>
        public double MinSpeed { get; protected set; }

        /// <summary>
        /// Maksymalna prędkość w obecnym środowisku
        /// </summary>
        public double MaxSpeed { get; protected set; }

        /// <summary>
        /// Zamienia jednostki prędkości na kilometry na godzinę
        /// </summary>
        /// <exception cref="Exception">Nie ma takiego środowiska w definicji</exception>
        protected void ConvertSpeed()
        {
            switch (environment)
            {
                case IVehicle.Environment.Ziemia:
                    {
                        MainSpeed = GetSpeed();
                    }break;
                case IVehicle.Environment.Woda:
                    {
                        MainSpeed = WaterWehicle.ConvertMilesPerHourToKilometeresPerHour(GetSpeed());
                    }break;
                case IVehicle.Environment.Powietrze:
                    {
                        MainSpeed = AirVehicle.ConvertMetersPerSecondToKilometeresPerHour(GetSpeed());
                    }break;
                default:
                    throw new Exception("Napotkano nieznany błąd. Skontaktuj się z administratorem.");
            }
        }

        /// <summary>
        /// Obecna prędkość w ustandaryzowanych kilmetrach na godzinę
        /// </summary>
        public double MainSpeed { get; protected set; }

        /// <summary>
        /// Obecna prędkość
        /// </summary>
        private double _Speed;

        /// <summary>
        /// Zwraca obecną prędkość pojazdu
        /// </summary>
        /// <returns>Obecna prędkość</returns>
        public double GetSpeed() => _Speed;

        /// <summary>
        /// Zmienia prędkość pojazdu
        /// </summary>
        /// <param name="speed">Docelowa prędkość</param>
        public void SetSpeed(double speed)
        {
            if (speed >= MinSpeed && speed <= MaxSpeed)
            {
                if (speed > _Speed)
                    Console.WriteLine("Przyśpieszam..");
                else if (speed < MinSpeed)
                    Console.WriteLine("Zwalniam..");
                else
                    return;
                _Speed = speed;
                ConvertSpeed();
            }
            else
            {
                Console.WriteLine($"Podana prędkość ({speed}) NIE jest dostępna w tym środowisku. Proszę stosować się do limitów: ");
                Console.WriteLine();
                CheckTheSpeedLimitsOfTheEnvironment();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Wyświetla na konsolę dozwolone limity prędkości dla obecnego środowiska
        /// </summary>
        public void CheckTheSpeedLimitsOfTheEnvironment()
        {
            Console.WriteLine("--- Ograniczenia prędkości ---");
            Console.WriteLine($"Minimalna prędkość to: {MinSpeed}");
            Console.WriteLine($"Maksymalna prędkość to: {MaxSpeed}");
        }
        /// <summary>
        /// Wyświetla na konsolę dozwolone limity prędkości dla wybranego środowiska
        /// </summary>
        public static void CheckTheSpeedLimitsOfTheEnvironment(IVehicle.Environment environmentForLimit)
        {
            switch (environmentForLimit)
            {
                case IVehicle.Environment.Ziemia:
                    {
                        Console.WriteLine("--- Ograniczenia prędkości ---");
                        Console.WriteLine($"Minimalna prędkość to: {EnvironmentLimitSpeedKmPerH.ZiemiaMin}");
                        Console.WriteLine($"Maksymalna prędkość to: {EnvironmentLimitSpeedKmPerH.ZiemiaMax}");
                    }
                    break;

                case IVehicle.Environment.Powietrze:
                    {
                        Console.WriteLine("--- Ograniczenia prędkości ---");
                        Console.WriteLine($"Minimalna prędkość to: {EnvironmentLimitSpeedKmPerH.PowietrzeMin}");
                        Console.WriteLine($"Maksymalna prędkość to: {EnvironmentLimitSpeedKmPerH.PowietrzeMax}");
                    }
                    break;

                case IVehicle.Environment.Woda:
                    {
                        Console.WriteLine("--- Ograniczenia prędkości ---");
                        Console.WriteLine($"Minimalna prędkość to: {EnvironmentLimitSpeedKmPerH.WodaMin}");
                        Console.WriteLine($"Maksymalna prędkość to: {EnvironmentLimitSpeedKmPerH.WodaMax}");
                    }
                    break;

            }
        }
        #endregion

        #region Środowisko
        /// <summary>
        /// Obecne środowisko
        /// </summary>
        public IVehicle.Environment environment { get; protected set; }

        /// <summary>
        /// Zmienia środowisko oraz ustawia parametry dla danego środowiska
        /// </summary>
        /// <param name="environmentToSet">Docelowe środowisko</param>
        /// <param name="speed">Prędkość pojazdu przed zmianą środowiska</param>
        /// <exception cref="ArgumentOutOfRangeException">Prędkość wychodzi poza limity docelowego środowiska</exception>
        /// <exception cref="ArgumentException">Nie odnaleziono podanego środowiska</exception>
        protected void SetEnvironment(IVehicle.Environment environmentToSet, double? speed = null)
        {
            switch (environmentToSet)
            {
                case IVehicle.Environment.Ziemia:
                    {
                        environment = IVehicle.Environment.Ziemia;
                        MinSpeed = EnvironmentLimitSpeedKmPerH.ZiemiaMin;
                        MaxSpeed = EnvironmentLimitSpeedKmPerH.ZiemiaMax;
                        SpeedUnit = SpeedUnits.KilometryNaGodzine;
                        if (speed != null)
                        {
                            SetSpeed((double)speed);
                        }
                    }
                    break;
                case IVehicle.Environment.Woda:
                    {
                        environment = IVehicle.Environment.Woda;
                        MinSpeed = WaterWehicle.ConvertKilometeresPerHourToMilesPerHour(EnvironmentLimitSpeedKmPerH.WodaMin);
                        MaxSpeed = WaterWehicle.ConvertKilometeresPerHourToMilesPerHour(EnvironmentLimitSpeedKmPerH.WodaMax);
                        SpeedUnit = SpeedUnits.MileMorskie;
                        if (speed != null)
                        {
                            SetSpeed((double)speed);
                        }
                    }
                    break;
                case IVehicle.Environment.Powietrze:
                    {
                        environment = IVehicle.Environment.Powietrze;
                        MinSpeed = AirVehicle.ConvertKilometeresPerHourToMetersPerSecond(EnvironmentLimitSpeedKmPerH.PowietrzeMin);
                        MaxSpeed = AirVehicle.ConvertKilometeresPerHourToMetersPerSecond(EnvironmentLimitSpeedKmPerH.PowietrzeMax);
                        SpeedUnit = SpeedUnits.MetryNaSekunde;
                        if (speed != null)
                        {
                            SetSpeed((double)speed);
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("Nie odnaleziono podanego środowiska.");
            }
        }
        #endregion

        #region Stan
        /// <summary>
        /// Obecny stan pojazdu
        /// </summary>
        public virtual IVehicle.State VehicleState { get; protected set; } = IVehicle.State.Stoi;

        /// <summary>
        /// Uruchamia procedure uruchamiania pojazdu
        /// </summary>
        public virtual void Start() => StartTheVehicle(IVehicle.State.Jedzie);

        /// <summary>
        /// Uruchamia pojazd
        /// </summary>
        protected void StartTheVehicle(IVehicle.State state)
        {
            if ((VehicleState is IVehicle.State.Jedzie or IVehicle.State.Leci or IVehicle.State.Płynie) || state is IVehicle.State.Stoi)
                return;
            Console.WriteLine("Pojazd ruszył..");
            VehicleState = state;
            SetSpeed(MinSpeed);
        }

        /// <summary>
        /// Zatrzymuje pojazd
        /// </summary>
        public virtual void Stop()
        {
            if (VehicleState is IVehicle.State.Stoi)
                return;
            Console.WriteLine("Pojazd się zatrzymał..");
            VehicleState = IVehicle.State.Stoi;
            SetStop();
        }

        /// <summary>
        /// Podczas zatrzymania wymusza ustawienie prędkości na 0
        /// </summary>
        protected void SetStop() => _Speed = 0;
        #endregion

        /// <summary>
        /// Typ obiektu
        /// </summary>
        protected abstract string _Type { get; }

        /// <summary>
        /// Czy pojazd silnikowy?
        /// </summary>
        public bool IsEngine { get; init; }

        /// <summary>
        /// Moc silnika wyrażona w koniach mechanicznych, w przypadku braku silnika null
        /// </summary>
        public int? EnginePower { get; init; }

        /// <summary>
        /// Marka pojazdu
        /// </summary>
        public string Mark { get; init; }

        /// <summary>
        /// Typ paliwa
        /// </summary>
        public IVehicle.TypeOfFuel Fuel { get; init; }

        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="mark">Marka pojazdu</param>
        /// <param name="isEngine">Czy pojazd silnikowy?</param>
        /// <param name="enginePower">Moc silnika w koniach mechanicznych</param>
        /// <param name="typeOfFuel">Typ paliwa</param>
        /// <exception cref="Exception">Pojazd silnikowy musi mieć zdefiniowany typ paliwa oraz moc większą od zera</exception>
        protected BaseVehicle(string mark, bool isEngine, int? enginePower = null, IVehicle.TypeOfFuel typeOfFuel = IVehicle.TypeOfFuel.Brak)
        {
            if (isEngine && (enginePower is null || enginePower <= 0 || typeOfFuel is IVehicle.TypeOfFuel.Brak))
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