using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pojazdy
{
    public class WaterWehicle : BaseVehicle, IWaterEnvironment
    {
        private readonly string _Type = "Wodny";
        public WaterWehicle(int displacement, string mark, bool isEngine, int? enginePower = null) : base(mark, isEngine, enginePower)
        {
            Displacement = displacement;
            if (isEngine && Fuel != TypeOfFuel.Olej)
                throw new ArgumentException("Podano niewłaściwe paliwo. Pojazdy wodne z silnikiem jeżdżą tylko na olej.");
            SetEnvironment(Environment.Woda);
        }

        public int Displacement { get; init; }
        public static double ConvertMilesPerHourToKilometeresPerHour(double miles) => Math.Round(miles * 1.6093123, 2);
        public static double ConvertMilesPerHourToKilometeresPerHour(int miles) => Math.Round(Convert.ToDouble(miles) * 1.6093123, 2);
        public static double ConvertKilometeresPerHourToMilesPerHour(double kilometers) => Math.Round(kilometers / 1.6093123, 2);
        public static double ConvertKilometeresPerHourToMilesPerHour(int kilometers) => Math.Round(Convert.ToDouble(kilometers) / 1.6093123, 2);

        public override string ToString()
        {
            var description = $"Typ obiektu: {GetType()}, /n Rodzaj pojazdu: {_Type}, /n Środowisko: {environment}, /n Stan: {VehicleState}, /n Minimalne prędkość: {MinSpeed}, /n Maksymalna prędkość: {MaxSpeed}, /n Aktualna prędkość: {GetSpeed()}, /n Marka: {Mark}, /n Ilość kół: {Displacement}";
            if (IsEngine)
                description += $", /n Czy silnikowy: Tak, /n Moc: {EnginePower} KM, /n Rodzaj paliwa: {Fuel}";
            else
                description += ", /n Czy silnikowy: Nie";
            Console.WriteLine(description);
            return description;
        }
    }
}