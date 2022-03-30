using System;

namespace Pojazdy
{
    /// <summary>
    /// Pojazd powietrzny
    /// </summary>
    public class AirVehicle : BaseVehicle, IAirEnvironment
    {
        /// <summary>
        /// Stany pojazdów
        /// </summary>
        new public enum State
        {
            Stoi,
            Jedzie,
            Leci
        }

        /// <summary>
        /// Rodzaj pojazdu
        /// </summary>
        protected override string _Type => "Powietrzny";

        #region Stan
        /// <summary>
        /// Stan pojazdu
        /// </summary>
        public new State VehicleState { get; protected set; }

        /// <summary>
        /// Uruchomienie pojazdu
        /// </summary>
        public override void Start()
        {
            if (VehicleState is State.Jedzie or State.Leci)
                return;
            VehicleState = State.Jedzie;
            Console.WriteLine("Pojazd ruszył..");
            SetSpeed(MinSpeed);
        }

        /// <summary>
        /// Odlot pojazdu z środowiska lądowego do środowiska powietrznego
        /// </summary>
        public void Fly()
        {
            if (VehicleState is State.Leci)
                return;
            if (VehicleState is State.Stoi)
            {
                Console.WriteLine("Aby pojazd mógł latać należy go najpierw uruchomić. Rozpoczynam sekwencje uruchomienia..");
                Start();
                SetSpeed(EnvironmentLimitSpeedKmPerH.PowietrzeMin);
            }
            else
                if (GetSpeed() < EnvironmentLimitSpeedKmPerH.PowietrzeMin)
                SetSpeed(EnvironmentLimitSpeedKmPerH.PowietrzeMin);

            Console.WriteLine("Pojazd wzniósł się do góry..");
            SetEnvironment(Environment.Powietrze, GetSpeed());
            VehicleState = State.Leci;
        }

        /// <summary>
        /// Lądowanie pojazdu z środowiska powietrznego na środowisko lądowe
        /// </summary>
        public void Land()
        {
            if (VehicleState is State.Jedzie or State.Stoi)
                return;
            Console.WriteLine("Lądowanie..");
            SetSpeed(MinSpeed);
            SetEnvironment(Environment.Ziemia, GetSpeed());
            VehicleState = State.Jedzie;
        }

        /// <summary>
        /// Zatrzymanie pojazdu
        /// </summary>
        public override void Stop()
        {
            if (VehicleState is State.Stoi)
                return;
            if (VehicleState is State.Leci)
            {
                Console.WriteLine("Nie można zatrzymać pojazdu w powietrzu, należy najpierw wylądować. Rozpoczynam sekwencje lądowania..");
                Land();
            }
            SetStop();
            VehicleState = State.Stoi;
            Console.WriteLine("Pojazd się zatrzymał..");
        }
        #endregion

        /// <summary>
        /// Ilość kół
        /// </summary>
        public int Wheels { get; init; }

        /// <summary>
        /// Domyslny konstruktor
        /// </summary>
        /// <param name="wheels">Ilość kół</param>
        /// <param name="mark">Marka</param>
        /// <param name="isEngine">Czy pojazd silnikowy</param>
        /// <param name="enginePower">Moc silnika wyrażona w koniach mechanicznych</param>
        /// <param name="typeOfFuel">Typ paliwa</param>
        public AirVehicle(int wheels, string mark, bool isEngine, int? enginePower = null, TypeOfFuel typeOfFuel = TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            Wheels = wheels;
            SetEnvironment(Environment.Ziemia);
        }

        #region Konwertery jednostek
        /// <summary>
        /// Przekonwertowanie metrów na sekundę na kilmetry na godzinę
        /// </summary>
        /// <param name="meters">Metry na sekundę</param>
        /// <returns>Kilometry na godzinę</returns>
        public static double ConvertMetersPerSecondToKilometeresPerHour(double meters) => Math.Round(meters * ConvertersData.MetryNaSekunde, 2);

        /// <summary>
        /// Przekonwertowanie metrów na sekundę na kilmetry na godzinę
        /// </summary>
        /// <param name="meters">Metry na sekundę</param>
        /// <returns>Kilometry na godzinę</returns>
        public static double ConvertMetersPerSecondToKilometeresPerHour(int meters) => Math.Round(Convert.ToDouble(meters) * ConvertersData.MetryNaSekunde, 2);

        /// <summary>
        /// Przekonwertowanie kilometrów na godzinę na metry na sekundę
        /// </summary>
        /// <param name="kilometers">Kilometry na godzinę</param>
        /// <returns>Metry na sekundę</returns>
        public static double ConvertKilometeresPerHourToMetersPerSecond(double kilometers) => Math.Round(kilometers / ConvertersData.MetryNaSekunde, 2);

        /// <summary>
        /// Przekonwertowanie kilometrów na godzinę na metry na sekundę
        /// </summary>
        /// <param name="kilometers">Kilometry na godzinę</param>
        /// <returns>Metry na sekundę</returns>
        public static double ConvertKilometeresPerHourToMetersPerSecond(int kilometers) => Math.Round(Convert.ToDouble(kilometers) / ConvertersData.MetryNaSekunde, 2);
        #endregion

        /// <summary>
        /// Wyświetla dane o pojeździe
        /// </summary>
        /// <returns>Strin opisujący atrybuty danego pojazdu</returns>
        public override string ToString()
        {
            var description = $"Typ obiektu: {GetType()}, /n Rodzaj pojazdu: {_Type}, /n Środowisko: {environment}, /n Stan: {VehicleState}, /n Minimalne prędkość: {MinSpeed}, /n Maksymalna prędkość: {MaxSpeed}, /n Aktualna prędkość: {GetSpeed()} {SpeedUnit}, /n Marka: {Mark}, /n Ilość kół: {Wheels}";
            if (IsEngine)
                description += $", /n Czy silnikowy: Tak, /n Moc: {EnginePower} KM, /n Rodzaj paliwa: {Fuel}";
            else
                description += ", /n Czy silnikowy: Nie";
            Console.WriteLine(description);
            return description;
        }
    }
}