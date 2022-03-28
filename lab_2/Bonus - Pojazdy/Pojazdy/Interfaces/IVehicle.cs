namespace Pojazdy
{
    /// <summary>
    /// Intefejst podstawowy dla pojazdów
    /// </summary>
    public interface IVehicle
    {
        BaseVehicle.Environment environment { get; }
        BaseVehicle.State VehicleState { get; }
        string SpeedUnit { get; }
        double MaxSpeed { get; }
        double MinSpeed { get; }
        double MainSpeed { get; }
        string Mark { get; init; }
        bool IsEngine { get; init; }
        BaseVehicle.TypeOfFuel Fuel { get; init; }
        int? EnginePower { get; init; }

        void Start();
        void Stop();
        void SetSpeed(double speed);
        double GetSpeed();
        void CheckTheSpeedLimitsOfTheEnvironment();
    }
}