namespace Pojazdy
{
    /// <summary>
    /// Interfejst dla pojazdów w środowisku powietrznych
    /// </summary>
    public interface IAirEnvironment
    {
        AirVehicle.State VehicleState { get; }
        int Wheels { get; init; }

        void Start();
        void Fly();
        void Land();
        void Stop();
        string ToString();
    }
}