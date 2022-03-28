using System;

namespace Pojazdy
{
    public class AirVehicle : BaseVehicle, IAirEnvironment
    {
        private readonly string _Type = "Powietrzny";
        public AirVehicle(int wheels, string mark, bool isEngine, int? enginePower = null, TypeOfFuel typeOfFuel = TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            if (isEngine && (enginePower == null || typeOfFuel == TypeOfFuel.Brak))
                throw new ArgumentException();

            Wheels = wheels;
            SetEnvironment(Environment.Ziemia);
        }

        public new State VehicleState { get; protected set; }

        public override void Start()
        {
            if (VehicleState is State.Jedzie or State.Leci)
                return;
            VehicleState = State.Jedzie;
            Console.WriteLine("Pojazd ruszył..");
            SetSpeed(MinSpeed);
        }

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
                if(GetSpeed() < EnvironmentLimitSpeedKmPerH.PowietrzeMin)
                SetSpeed(EnvironmentLimitSpeedKmPerH.PowietrzeMin);

            Console.WriteLine("Pojazd wzniósł się do góry..");
            SetEnvironment(Environment.Powietrze, GetSpeed());
            VehicleState = State.Leci;
        }

        public void Land()
        {
            if (VehicleState is State.Jedzie or State.Stoi)
                return;
            Console.WriteLine("Lądowanie..");
            SetSpeed(MinSpeed);
            SetEnvironment(Environment.Ziemia, GetSpeed());
            VehicleState = State.Jedzie;
        }

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

        new public enum State
        {
            Stoi,
            Jedzie,
            Leci
        }

        public int Wheels { get; init; }

        public static double ConvertMetersPerSecondToKilometeresPerHour(double meters) => Math.Round(meters * 3.6, 0);
        public static double ConvertMetersPerSecondToKilometeresPerHour(int meters) => Math.Round(Convert.ToDouble(meters) * 3.6, 0);
        public static double ConvertKilometeresPerHourToMetersPerSecond(double kilometers) => Math.Round(kilometers / 3.6, 0);
        public static double ConvertKilometeresPerHourToMetersPerSecond(int kilometers) => Math.Round(Convert.ToDouble(kilometers) / 3.6, 0);

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