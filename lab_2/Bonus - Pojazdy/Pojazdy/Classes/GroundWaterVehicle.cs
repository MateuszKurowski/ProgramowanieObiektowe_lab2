using System;

namespace Pojazdy
{
    /// <summary>
    /// Pojazd wielorodzajowy ziemia-woda
    /// </summary>
    /// <seealso cref="Pojazdy.BaseVehicle" />
    /// <seealso cref="Pojazdy.IGroundEnvironment" />
    /// <seealso cref="Pojazdy.IWaterEnvironment" />
    public class GroundWaterVehicle : BaseVehicle, IGroundEnvironment, IWaterEnvironment
    {
        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="mark">Marka</param>
        /// <param name="wheels">Ilość kół</param>
        /// <param name="displacement">Wyporność</param>
        /// <param name="isEngine">Czy pojazd silnikowy?</param>
        /// <param name="environmentOfVehicle">Środowisko gdzie znajduje się obecnie pojazd</param>
        /// <param name="enginePower">Moc silnika wyrażona w koniach mechanicznych</param>
        /// <param name="typeOfFuel">Typ paliwa</param>
        /// <exception cref="System.ArgumentException">Podano niewłaściwe paliwo. Pojazdy wodne z silnikiem jeżdżą tylko na olej.</exception>
        public GroundWaterVehicle(string mark, int wheels, int displacement, bool isEngine, IVehicle.Environment environmentOfVehicle = IVehicle.Environment.Ziemia, int? enginePower = null, IVehicle.TypeOfFuel typeOfFuel = IVehicle.TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            if (isEngine && Fuel is not IVehicle.TypeOfFuel.Olej)
                throw new ArgumentException("Podano niewłaściwe paliwo. Pojazdy wodne z silnikiem jeżdżą tylko na olej.");
            Displacement = displacement;
            Wheels = wheels;
            if (environmentOfVehicle is IVehicle.Environment.Woda)
                SetEnvironment(IVehicle.Environment.Woda);
            else SetEnvironment(IVehicle.Environment.Ziemia);
        }

        /// <summary>
        /// Rodzaj pojazdu
        /// </summary>
        protected override string _Type { get; } = "Wielorodzajowy";

        /// <summary>
        /// Wyporność
        /// </summary>
        public int Displacement { get; init; }

        /// <summary>
        /// Ilość kół
        /// </summary>
        public int Wheels { get; init; }

        #region Stan
        /// <summary>
        /// Wodowanie pojazdu
        /// </summary>
        public void LaunchingToWater()
        {
            if (VehicleState is IVehicle.State.Płynie or IVehicle.State.Stoi)
                return;
            SetEnvironment(IVehicle.Environment.Woda, GetSpeed());
        }

        /// <summary>
        /// Uruchamia procedure uruchamiania pojazdu
        /// </summary>
        public override void Start()
        {
            if (VehicleState is IVehicle.State.Płynie or IVehicle.State.Jedzie)
                return;
            if (environment is IVehicle.Environment.Woda)
                StartTheVehicle(IVehicle.State.Płynie);
            else 
                StartTheVehicle(IVehicle.State.Jedzie);
        }

        /// <summary>
        /// Lądowanie pojazdu z wody
        /// </summary>
        public void Land()
        {
            if (VehicleState is IVehicle.State.Stoi or IVehicle.State.Jedzie)
                return;
            SetEnvironment(IVehicle.Environment.Ziemia, GetSpeed());
        }
        #endregion

        /// <summary>
        /// Wyświetla dane o pojeździe
        /// </summary>
        /// <returns>Strin opisujący atrybuty danego pojazdu</returns>
        public override string ToString()
        {
            Console.WriteLine();
            var description = $"Opis pojazdu: \r\n Typ obiektu: {GetType()}, \r\n Rodzaj pojazdu: {_Type}, \r\n Środowisko: {environment}, \r\n Stan: {VehicleState}, \r\n Minimalna prędkość: {MinSpeed}, \r\n Maksymalna prędkość: {MaxSpeed}, \r\n Aktualna prędkość: {GetSpeed()} {SpeedUnit}, \r\n Marka: {Mark}, \r\n Ilość kół: {Wheels}, \r\n Wyporność: {Displacement}";
            if (IsEngine)
                description += $", \r\n Czy silnikowy: Tak, \r\n Moc: {EnginePower} KM, \r\n Rodzaj paliwa: {Fuel}";
            else
                description += ", \r\n Czy silnikowy: Nie";
            Console.WriteLine(description);
            Console.WriteLine();
            return description;
        }
    }
}