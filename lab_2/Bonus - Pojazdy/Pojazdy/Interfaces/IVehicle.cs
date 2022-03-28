namespace Pojazdy
{
    public interface IVehicle
    {
        int? EnginePower { get; init; }
        BaseVehicle.Environment environment { get; }
        BaseVehicle.TypeOfFuel Fuel { get; init; }
        bool IsEngine { get; init; }

        void CheckTheSpeedLimitsOfTheEnvironment();
        void Start();
        void SetSpeed(double speed);
        void Stop();
    }
}