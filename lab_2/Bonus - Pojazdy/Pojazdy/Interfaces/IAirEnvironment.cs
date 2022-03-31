namespace Pojazdy
{
    /// <summary>
    /// Interfejst dla pojazdów w środowisku powietrznych
    /// </summary>
    public interface IAirEnvironment : IVehicle
    {
        int Wheels { get; init; }

        void Fly();
        void Land();
        string ToString();
    }
}