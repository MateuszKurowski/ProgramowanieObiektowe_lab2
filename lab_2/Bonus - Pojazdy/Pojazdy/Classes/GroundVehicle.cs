using System;

namespace Pojazdy
{
    /// <summary>
    /// Pojazd lądowy
    /// </summary>
    public class GroundVehicle : BaseVehicle, IGroundEnvironment
    {
        /// <summary>
        /// Rodzaj pojazdu
        /// </summary>
        protected override string _Type { get; } = "Lądowy";

        /// <summary>
        /// Ilośc kół
        /// </summary>
        public int Wheels { get; init; }

        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="wheels">Ilość kół</param>
        /// <param name="mark">Marka</param>
        /// <param name="isEngine">Czy pojazd silnikowy?</param>
        /// <param name="enginePower">Moc silinika wyrażona </param>
        /// <param name="typeOfFuel">Typ paliwa</param>
        public GroundVehicle(int wheels, string mark, bool isEngine, int? enginePower = null, IVehicle.TypeOfFuel typeOfFuel = IVehicle.TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            Wheels = wheels;
            SetEnvironment(IVehicle.Environment.Ziemia);
        }

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