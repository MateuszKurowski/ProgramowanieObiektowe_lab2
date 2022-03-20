using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pojazdy
{
    public class AirVehicle : BaseVahicle, IAirEnvironment
    {
        public AirVehicle(int wheels, string mark, bool isEngine, int? enginePower = null, TypeOfFuel typeOfFuel = TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            Wheels = wheels;
        }

        new public enum State
        {
            Standing,
            Driving,
            Flying
        }

        public int Wheels { get; init; }

        protected override string SpeedUnit => SpeedUnits.MetryNaSekunde;

        protected override int MinSpeed => 20;

        protected override int MaxSpeed => 200;

        public override Environment environment => Environment.Powietrze;
    }
}