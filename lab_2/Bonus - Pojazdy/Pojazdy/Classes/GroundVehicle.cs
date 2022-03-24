namespace Pojazdy
{
    public class GroundVehicle : BaseVahicle, IGroundEnvironment
    {
        public int Wheels { get; init; }

        protected override int MinSpeed => 1;

        protected override int MaxSpeed => 350;

        public override Environment environment => Environment.Ziemia;

        protected override string SpeedUnit => SpeedUnits.KilometryNaGodzine;

        public GroundVehicle(int wheels, string mark, bool isEngine, int? enginePower = null, TypeOfFuel typeOfFuel = TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            Wheels = wheels;
        }


    }
}