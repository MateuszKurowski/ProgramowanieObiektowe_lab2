using System;

namespace Pojazdy
{
    public class GroundVehicle : BaseVehicle, IGroundEnvironment
    {
        private readonly string _Type = "Lądowy";

        public int Wheels { get; init; }

        public GroundVehicle(int wheels, string mark, bool isEngine, int? enginePower = null, TypeOfFuel typeOfFuel = TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            if (isEngine && (enginePower == null || typeOfFuel == TypeOfFuel.Brak))
                throw new ArgumentException();

            Wheels = wheels;
            SetEnvironment(Environment.Ziemia);
        }

        public override string ToString()
        {
            var description = $"Typ obiektu: {GetType()}, /n Rodzaj pojazdu: {_Type}, /n Środowisko: {environment}, /n Stan: {VehicleState}, /n Minimalne prędkość: {MinSpeed}, /n Maksymalna prędkość: {MaxSpeed}, /n Aktualna prędkość: {GetSpeed()}, /n Marka: {Mark}, /n Ilość kół: {Wheels}";
            if (IsEngine)
                description += $", /n Czy silnikowy: Tak, /n Moc: {EnginePower} KM, /n Rodzaj paliwa: {Fuel}";
            else
                description += ", /n Czy silnikowy: Nie";
            Console.WriteLine(description);
            return description;
        }
    }
}