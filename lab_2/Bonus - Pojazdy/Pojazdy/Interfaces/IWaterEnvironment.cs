namespace Pojazdy
{
    /// <summary>
    /// Interfejs dla pojazdów w środowisku wodnych
    /// </summary>
    public interface IWaterEnvironment : IVehicle
    {
        int Displacement { get; init; }

        string ToString();
    }
}