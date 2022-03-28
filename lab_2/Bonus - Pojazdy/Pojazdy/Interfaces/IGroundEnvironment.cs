namespace Pojazdy
{
    /// <summary>
    /// Interfejs dla pojazdów w środowisku lądowym
    /// </summary>
    public interface IGroundEnvironment
    {
        int Wheels { get; init; }

        string ToString();
    }
}