using System;

namespace Pojazdy
{
    /// <summary>
    /// Pojazd wodny
    /// </summary>
    public class WaterWehicle : BaseVehicle, IWaterEnvironment
    {
        /// <summary>
        /// Rodzaj pojazdu
        /// </summary>
        private readonly string _Type = "Wodny";

        /// <summary>
        /// Wyporność pojazdu
        /// </summary>
        public int Displacement { get; init; }

        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="displacement">Wyporność pojazdu</param>
        /// <param name="mark">Marka</param>
        /// <param name="isEngine">Czy pojazd silnikowy?</param>
        /// <param name="enginePower">Moc silnika wyrażona w koniach mechanicznych</param>
        /// <param name="typeOfFuel">Typ paliwa</param>
        /// <exception cref="ArgumentException">Podano niewłaściwe paliwo, pojazdy wodne silnikowe używają tylko oleju</exception>
        public WaterWehicle(int displacement, string mark, bool isEngine, int? enginePower = null, TypeOfFuel typeOfFuel = TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            Displacement = displacement;
            if (isEngine && Fuel != TypeOfFuel.Olej)
                throw new ArgumentException("Podano niewłaściwe paliwo. Pojazdy wodne z silnikiem jeżdżą tylko na olej.");
            SetEnvironment(Environment.Woda);
        }

        #region Konwertery jednostek
        /// <summary>
        /// Przekonwertowanie mil na godzinę na kilmetry na godzinę
        /// </summary>
        /// <param name="miles">Mile na godzinę</param>
        /// <returns>Kilometry na godzinę</returns>
        public static double ConvertMilesPerHourToKilometeresPerHour(double miles) => Math.Round(miles * ConvertersData.MileNaGodzine, 2);

        /// <summary>
        /// Przekonwertowanie mil na godzinę na kilmetry na godzinę
        /// </summary>
        /// <param name="miles">Mile na godzinę</param>
        /// <returns>Kilometry na godzinę</returns>
        public static double ConvertMilesPerHourToKilometeresPerHour(int miles) => Math.Round(Convert.ToDouble(miles) * ConvertersData.MileNaGodzine, 2);

        /// <summary>
        /// Przekonwertowanie kilometrów na godzinę na mile na godzinę
        /// </summary>
        /// <param name="kilometers">Kilometry na godzinę</param>
        /// <returns>Mile na godzinę</returns>
        public static double ConvertKilometeresPerHourToMilesPerHour(double kilometers) => Math.Round(kilometers / ConvertersData.MileNaGodzine, 2);

        /// <summary>
        /// Przekonwertowanie kilometrów na godzinę na mile na godzinę
        /// </summary>
        /// <param name="kilometers">Kilometry na godzinę</param>
        /// <returns>Mile na godzinę</returns>
        public static double ConvertKilometeresPerHourToMilesPerHour(int kilometers) => Math.Round(Convert.ToDouble(kilometers) / ConvertersData.MileNaGodzine, 2);
        #endregion

        /// <summary>
        /// Wyświetla dane o pojeździe
        /// </summary>
        /// <returns>Strin opisujący atrybuty danego pojazdu</returns>
        public override string ToString()
        {
            var description = $"Typ obiektu: {GetType()}, /n Rodzaj pojazdu: {_Type}, /n Środowisko: {environment}, /n Stan: {VehicleState}, /n Minimalne prędkość: {MinSpeed}, /n Maksymalna prędkość: {MaxSpeed}, /n Aktualna prędkość: {GetSpeed()} {SpeedUnit}, /n Marka: {Mark}, /n Ilość kół: {Displacement}";
            if (IsEngine)
                description += $", /n Czy silnikowy: Tak, /n Moc: {EnginePower} KM, /n Rodzaj paliwa: {Fuel}";
            else
                description += ", /n Czy silnikowy: Nie";
            Console.WriteLine(description);
            return description;
        }
    }
}