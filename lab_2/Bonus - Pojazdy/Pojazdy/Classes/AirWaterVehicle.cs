using System;

namespace Pojazdy
{
    public class AirWaterVehicle : BaseVehicle, IWaterEnvironment, IAirEnvironment
    {
        public AirWaterVehicle(string mark, int wheels, int displacement, bool isEngine, int? enginePower = null, IVehicle.TypeOfFuel typeOfFuel = IVehicle.TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            if (isEngine && Fuel is not IVehicle.TypeOfFuel.Olej)
                throw new ArgumentException("Podano niewłaściwe paliwo. Pojazdy wodne z silnikiem jeżdżą tylko na olej.");
            Displacement = displacement;
            Wheels = wheels;
            SetEnvironment(IVehicle.Environment.Woda);
        }

        public int Displacement { get; init; }
        public int Wheels { get; init; }

        /// <summary>
        /// Rodzaj pojazdu
        /// </summary>
        protected override string _Type { get; } = "Wielorodzajowy";

        /// <summary>
        /// Uruchomienie pojazdu
        /// </summary>
        public override void Start()
        {
            if (VehicleState is IVehicle.State.Leci or IVehicle.State.Płynie)
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
                StartTheVehicle(IVehicle.State.Płynie);
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
        /// Lądowanie pojazdu z środowiska powietrznego na środowisko wodne
        /// </summary>
        public void Land()
        {
            if (VehicleState is IVehicle.State.Płynie or IVehicle.State.Stoi)
                return;
            Console.WriteLine("Lądowanie..");
            SetSpeed(MinSpeed);
            SetEnvironment(IVehicle.Environment.Woda, GetSpeed());
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
    }
}