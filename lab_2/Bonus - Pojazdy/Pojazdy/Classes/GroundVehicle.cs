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