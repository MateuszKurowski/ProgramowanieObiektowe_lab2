namespace Pojazdy
{
    /// <summary>
    /// Intefejst podstawowy dla pojazdów
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// Stan pojazdu
        /// </summary>
        enum State { Stoi, Jedzie, Leci, Płynie }
        /// <summary>
        /// Środowisko pojazdu
        /// </summary>
        enum Environment { Ziemia, Powietrze, Woda }
        /// <summary>
        /// Typ paliwa
        /// </summary>
        enum TypeOfFuel { Benzyna, Olej, LPG, Prad, Diesiel, Brak }

        Environment environment { get; }
        State VehicleState { get; }
        string SpeedUnit { get; }
        double MaxSpeed { get; }
        double MinSpeed { get;  }
        double MainSpeed { get; }
        string Mark { get; init; }
        bool IsEngine { get; init; }
        TypeOfFuel Fuel { get; init; }
        int? EnginePower { get; init; }

        void Start();
        void Stop();
        void SetSpeed(double speed);
        double GetSpeed();
        void CheckTheSpeedLimitsOfTheEnvironment();
    }
}