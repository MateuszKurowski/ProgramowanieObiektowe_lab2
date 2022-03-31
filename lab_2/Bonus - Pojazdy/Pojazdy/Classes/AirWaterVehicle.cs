using System;

namespace Pojazdy
{
    /// <summary>
    /// /Pojazd wielorodzajowy woda-powietrze
    /// </summary>
    /// <seealso cref="Pojazdy.BaseVehicle" />
    /// <seealso cref="Pojazdy.IWaterEnvironment" />
    /// <seealso cref="Pojazdy.IAirEnvironment" />
    public class AirWaterVehicle : BaseVehicle, IWaterEnvironment, IAirEnvironment
    {
        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="mark">Marka</param>
        /// <param name="wheels">Ilość kół</param>
        /// <param name="displacement">Wyporność</param>
        /// <param name="isEngine">Czy pojazd silnikowy?</param>
        /// <param name="enginePower">Moc silnika wyrażona w koniach mechanicznych</param>
        /// <param name="typeOfFuel">Typ paliwa</param>
        /// <exception cref="System.ArgumentException">Podano niewłaściwe paliwo. Pojazdy wodne z silnikiem jeżdżą tylko na olej.</exception>
        public AirWaterVehicle(string mark, int wheels, int displacement, bool isEngine, int? enginePower = null, IVehicle.TypeOfFuel typeOfFuel = IVehicle.TypeOfFuel.Brak) : base(mark, isEngine, enginePower, typeOfFuel)
        {
            if (isEngine && Fuel is not IVehicle.TypeOfFuel.Olej)
                throw new ArgumentException("Podano niewłaściwe paliwo. Pojazdy wodne z silnikiem jeżdżą tylko na olej.");
            Displacement = displacement;
            Wheels = wheels;
            SetEnvironment(IVehicle.Environment.Woda);
        }

        /// <summary>
        /// Wyporność
        /// </summary>
        public int Displacement { get; init; }

        /// <summary>
        /// Ilość kół
        /// </summary>
        public int Wheels { get; init; }

        /// <summary>
        /// Rodzaj pojazdu
        /// </summary>
        protected override string _Type { get; } = "Wielorodzajowy";

        #region Stan
        /// <summary>
        /// Uruchomienie pojazdu
        /// </summary>
        public override void Start()
        {
            if (VehicleState is IVehicle.State.Leci or IVehicle.State.Płynie)
                return;
            StartTheVehicle(IVehicle.State.Jedzie);
        }

        /// <summary>
        /// Odlot pojazdu z środowiska lądowego do środowiska powietrznego
        /// </summary>
        public void Fly()
        {
            if (VehicleState is IVehicle.State.Leci)
                return;
            if (VehicleState is IVehicle.State.Stoi)
            {
                Console.WriteLine("Aby pojazd mógł latać należy go najpierw uruchomić. Rozpoczynam sekwencje uruchomienia..");
                StartTheVehicle(IVehicle.State.Płynie);
                SetSpeed(EnvironmentLimitSpeedKmPerH.PowietrzeMin);
            }
            else
            {
                if (GetSpeed() < EnvironmentLimitSpeedKmPerH.PowietrzeMin)
                    SetSpeed(EnvironmentLimitSpeedKmPerH.PowietrzeMin);
            }

            Console.WriteLine("Pojazd wzniósł się do góry..");
            SetEnvironment(IVehicle.Environment.Powietrze, GetSpeed());
            VehicleState = IVehicle.State.Leci;
        }
        
        /// <summary>
        /// Lądowanie pojazdu z środowiska powietrznego na środowisko wodne
        /// </summary>
        public void Land()
        {
            if (VehicleState is IVehicle.State.Płynie or IVehicle.State.Stoi)
                return;
            Console.WriteLine("Lądowanie..");
            SetSpeed(MinSpeed);
            SetEnvironment(IVehicle.Environment.Woda, GetSpeed());
            VehicleState = IVehicle.State.Jedzie;
        }

        /// <summary>
        /// Zatrzymanie pojazdu
        /// </summary>
        public override void Stop()
        {
            if (VehicleState is IVehicle.State.Stoi)
                return;
            if (VehicleState is IVehicle.State.Leci)
            {
                Console.WriteLine("Nie można zatrzymać pojazdu w powietrzu, należy najpierw wylądować. Rozpoczynam sekwencje lądowania..");
                Land();
            }
            SetStop();
            VehicleState = IVehicle.State.Stoi;
            Console.WriteLine("Pojazd się zatrzymał..");
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