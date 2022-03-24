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
            MinSpeed = BaseVahicle.EnvironmentLimitSpeedKmPerH.PowietrzeMin;
        }

        new public enum State
        {
            Standing,
            Driving,
            Flying
        }

        public int Wheels { get; init; }


        public override Environment environment => Environment.Ziemia;

        protected override string SpeedUnit { get; set; } = SpeedUnits.KilometryNaGodzine;
        protected override double MinSpeed { get; set; } = EnvironmentLimitSpeedKmPerH.ZiemiaMin;
        protected override double MaxSpeed { get; set; } = EnvironmentLimitSpeedKmPerH.ZiemiaMax;

        public static double ConvertMetersPerSecondToKilometeresPerHour(double meters) => Math.Round(meters * 3.6, 0);
        public static double ConvertMetersPerSecondToKilometeresPerHour(int meters) => Math.Round(Convert.ToDouble(meters) * 3.6, 0);
        public static double ConvertKilometeresPerHourToMetersPerSecond(double kilometers) => Math.Round(kilometers / 3.6, 0);
        public static double ConvertKilometeresPerHourToMetersPerSecond(int kilometers) => Math.Round(Convert.ToDouble(kilometers) / 3.6, 0);
    }
}