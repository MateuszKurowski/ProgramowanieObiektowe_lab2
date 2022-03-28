namespace Pojazdy
{
    /// <summary>
    /// Interfejs dla pojazdów w środowisku wodnych
    /// </summary>
    public interface IWaterEnvironment
    {
        int Displacement { get; init; }

        string ToString();
    }
}