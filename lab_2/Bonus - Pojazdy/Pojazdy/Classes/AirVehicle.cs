using System;

namespace Pojazdy
{
    /// <summary>
    /// Pojazd powietrzny
    /// </summary>
    public class AirVehicle : BaseVehicle, IAirEnvironment
    {
        /// <summary>
        /// Rodzaj pojazdu
        /// </summary>
        protected override string _Type => "Powietrzny";

        #region Stan
        /// <summary>
        /// Uruchomienie pojazdu
        /// </summary>
        public override void Start()
        {
            if (VehicleState is IVehicle.State.Leci or IVehicle.State.Jedzie)
                return;
            StartTheVehicle(IVehicle.State.Jedzie);
        }

        /// <summary>
        /// Odlot pojazdu z środowiska lądowego do środowiska powietrznego
        /// </summary>
        public void Fly()
        {
            if (VehicleState is IVehicle.State.Leci)
                return;
            if (VehicleState is IVehicle.State.Stoi)
            {
                Console.WriteLine("Aby pojazd mógł latać należy go najpierw uruchomić. Rozpoczynam sekwencje uruchomienia..");
                StartTheVehicle(IVehicle.State.Jedzie);
                SetSpeed(EnvironmentLimitSpeedKmPerH.PowietrzeMin);
            }
            else
            {
                if (GetSpeed() < EnvironmentLimitSpeedKmPerH.PowietrzeMin)
                    SetSpeed(EnvironmentLimitSpeedKmPerH.PowietrzeMin);
            }

            Console.WriteLine("Pojazd wzniósł się do góry..");
            SetEnvironment(IVehicle.Environment.Powietrze, GetSpeed());
            VehicleState = IVehicle.State.Leci;
        }

        /// <summary>
        /// Lądowanie pojazdu z środowiska powietrznego na środowisko lądowe
        /// </summary>
        public void Land()
        {
            if (VehicleState is IVehicle.State.Jedzie or IVehicle.State.Stoi)
                return;
            Console.WriteLine("Lądowanie..");
            SetSpeed(MinSpeed);
            SetEnvironment(IVehicle.Environment.Ziemia, GetSpeed());
            VehicleState = IVehicle.State.Jedzie;
        }

        /// <summary>
        /// Zatrzymanie pojazdu
        /// </summary>
        public override void Stop()
        {
            if (VehicleState is IVehicle.State.Stoi)
                return;
            if (VehicleState is IVehicle.State.Leci)
            {
                Console.WriteLine("Nie można zatrzymać pojazdu w powietrzu, należy najpierw wylądować. Rozpoczynam sekwencje lądowania..");
                Land();
            }
            SetStop();
            VehicleState = IVehicle.State.Stoi;
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
        public AirVehicle(int wheels, string mark, bool isEngine, int? enginePower = null, IVehicle.TypeOfFuel typeOfFuel = IVehicle.TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            Wheels = wheels;
            SetEnvironment(IVehicle.Environment.Ziemia);
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
            Console.WriteLine();
            var description = $"Opis pojazdu: \r\n Typ obiektu: {GetType()}, \r\n Rodzaj pojazdu: {_Type}, \r\n Środowisko: {environment}, \r\n Stan: {VehicleState}, \r\n Minimalna prędkość: {MinSpeed}, \r\n Maksymalna prędkość: {MaxSpeed}, \r\n Aktualna prędkość: {GetSpeed()} {SpeedUnit}, \r\n Marka: {Mark}, \r\n Ilość kół: {Wheels}";
            if (IsEngine)
                description += $", \r\n Czy silnikowy: Tak, \r\n Moc: {EnginePower} KM, \r\n Rodzaj paliwa: {Fuel}";
            else
                description += ", \r\n Czy silnikowy: Nie";
            Console.WriteLine(description);
            Console.WriteLine();
            return description;
        }
    }
}