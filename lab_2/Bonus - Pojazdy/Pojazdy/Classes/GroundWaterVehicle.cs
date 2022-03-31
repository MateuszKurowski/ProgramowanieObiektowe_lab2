using System;

namespace Pojazdy
{
    public class GroundWaterVehicle : BaseVehicle, IGroundEnvironment, IWaterEnvironment
    {
        public GroundWaterVehicle(string mark, int wheels, int displacement, bool isEngine, IVehicle.Environment environmentOfVehicle = IVehicle.Environment.Ziemia, int? enginePower = null, IVehicle.TypeOfFuel typeOfFuel = IVehicle.TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            if (isEngine && Fuel is not IVehicle.TypeOfFuel.Olej)
                throw new ArgumentException("Podano niewłaściwe paliwo. Pojazdy wodne z silnikiem jeżdżą tylko na olej.");
            Displacement = displacement;
            Wheels = wheels;
            if (environmentOfVehicle is IVehicle.Environment.Woda)
                SetEnvironment(IVehicle.Environment.Woda);
            else SetEnvironment(IVehicle.Environment.Ziemia);
        }

        /// <summary>
        /// Rodzaj pojazdu
        /// </summary>
        protected override string _Type { get; } = "Wielorodzajowy";

        public int Displacement { get; init; }
        public int Wheels { get; init; }

        public void LaunchingToWater()
        {
            if (VehicleState is IVehicle.State.Płynie or IVehicle.State.Stoi)
                return;
            SetEnvironment(IVehicle.Environment.Woda, GetSpeed());
        }

        public override void Start()
        {
            if (VehicleState is IVehicle.State.Płynie or IVehicle.State.Jedzie)
                return;
            if (environment is IVehicle.Environment.Woda)
                StartTheVehicle(IVehicle.State.Płynie);
            else 
                StartTheVehicle(IVehicle.State.Jedzie);
        }

        public void Land()
        {
            if (VehicleState is IVehicle.State.Stoi or IVehicle.State.Jedzie)
                return;
            SetEnvironment(IVehicle.Environment.Ziemia, GetSpeed());
        }
    }
}