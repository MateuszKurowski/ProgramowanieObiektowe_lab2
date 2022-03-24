using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pojazdy
{
    public class WaterWehicle : BaseVahicle, IWaterEnvironment
    {
        public WaterWehicle(int displacement, string mark, bool isEngine, int? enginePower = null) : base(mark, isEngine, enginePower, TypeOfFuel.Brak)
        {
            Displacement = displacement;
            if (isEngine)
                Fuel = TypeOfFuel.Olej;
            else Fuel = TypeOfFuel.Brak;
        }

        public int Displacement { get; init; }

        public override Environment environment => Environment.Powietrze;

        protected override string SpeedUnit => SpeedUnits.MileMorskie;

        protected override int MinSpeed => 1;

        protected override int MaxSpeed => 40;

        public static double ConvertMilesPerHourToKilometeresPerHour(double miles) => Math.Round(miles * 1.6093123, 2);
        public static double ConvertMilesPerHourToKilometeresPerHour(int miles) => Math.Round(Convert.ToDouble(miles) * 1.6093123, 2);
        public static double ConvertKilometeresPerHourToMilesPerHour(double kilometers) => Math.Round(kilometers / 1.6093123, 2);
        public static double ConvertKilometeresPerHourToMilesPerHour(int kilometers) => Math.Round(Convert.ToDouble(kilometers) / 1.6093123, 2);
    }
}