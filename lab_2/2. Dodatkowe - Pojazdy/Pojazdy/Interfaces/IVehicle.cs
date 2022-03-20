namespace Pojazdy
{
    public interface IVehicle
    {
        int? EnginePower { get; init; }
        BaseVahicle.Environment environment { get; }
        BaseVahicle.TypeOfFuel Fuel { get; init; }
        bool IsEngine { get; init; }

        void CheckTheSpeedLimitsOfTheEnvironment();
        void Move();
        void SetSpeed(int speed);
        void Stop();
    }
}