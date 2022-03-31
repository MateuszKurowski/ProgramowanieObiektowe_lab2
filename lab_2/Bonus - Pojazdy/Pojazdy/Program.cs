using System;
using System.Collections.Generic;
using System.Linq;

namespace Pojazdy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Test();

            // Zadania z projektu
            Test4();
            Console.Clear();
            var vehilces = Test_6();
            Console.Clear();
            Test_6_1(vehilces);
            Console.Clear();
            Test_6_2(vehilces);
            Console.Clear();
            Test_6_3(vehilces);
            Console.Clear();
            Test_6_4(vehilces);

        }

        static void Test()
        {
            var samochod = new GroundVehicle(4, "BMW", true, 300, IVehicle.TypeOfFuel.Diesiel);
            samochod.Start();
            samochod.CheckTheSpeedLimitsOfTheEnvironment();
            samochod.SetSpeed(20);
            samochod.SetSpeed(300);
            samochod.GetSpeed();
            samochod.ToString();
            samochod.Stop();

            //var lodz = new WaterWehicle(20, "Samsung", true);
            //var lodz = new WaterWehicle(20, "Samsung", true, 30);
            var lodz = new WaterWehicle(20, "Samsung", true, 30, IVehicle.TypeOfFuel.Olej);
            lodz.Start();
            lodz.SetSpeed(30);
            lodz.ToString();

            var samolot = new AirVehicle(18, "Odlot", true, 400, IVehicle.TypeOfFuel.Benzyna);
            samolot.Start();
            samolot.Fly();
            samolot.SetSpeed(40);
            samolot.Stop();
            samolot.ToString();
        }

        static void Test4()
        {
            var samochod = new GroundVehicle(4, "Audi", true, 180, IVehicle.TypeOfFuel.LPG);
            samochod.ToString();
            var motorowka = new WaterWehicle(8, "Motorówka", true, 30, IVehicle.TypeOfFuel.Olej);
            motorowka.ToString();
            var amfibia = new GroundWaterVehicle("Amfibia", 4, 5, true, IVehicle.Environment.Ziemia, 90, IVehicle.TypeOfFuel.Olej);
            amfibia.ToString();
            var samolot = new AirVehicle(20, "Prezydencki", true, 500, IVehicle.TypeOfFuel.Benzyna);
            samolot.ToString();
            var rower = new GroundVehicle(2, "Górski rower", false);
            rower.ToString();
        }

        static List<BaseVehicle> Test_6()
        {
            var vehilces = new List<BaseVehicle>();
            vehilces.Add(new GroundVehicle(4, "Audi", true, 180, IVehicle.TypeOfFuel.LPG));
            vehilces[0].SetSpeed(20);
            vehilces.Add(new WaterWehicle(8, "Motorówka", true, 30, IVehicle.TypeOfFuel.Olej));
            vehilces[1].SetSpeed(38);
            vehilces.Add(new GroundWaterVehicle("Amfibia", 4, 5, true, IVehicle.Environment.Ziemia, 90, IVehicle.TypeOfFuel.Olej));
            vehilces[2].SetSpeed(30);
            vehilces.Add(new AirVehicle(20, "Prezydencki", true, 500, IVehicle.TypeOfFuel.Benzyna));
            vehilces[3].SetSpeed(60);
            vehilces.Add(new GroundVehicle(2, "Górski rower", false));
            vehilces[4].SetSpeed(70);
            return vehilces;
        }

        static void Test_6_1(List<BaseVehicle> vehilces)
        {
            foreach (var vehicle in vehilces)
            {
                vehicle.ToString();
            }
        }

        static void Test_6_2(List<BaseVehicle> vehilces)
        {
            foreach (var vehicle in vehilces)
            {
                // Środowisko lądowe
                //if (vehicle.environment is IVehicle.Environment.Ziemia)
                //    Console.WriteLine(vehicle);

                // Typ lądowy
                if (vehicle.GetType().Name == nameof(GroundVehicle))
                    vehicle.ToString();
            }
        }

        static void Test_6_3(List<BaseVehicle> vehilces)
        {
            vehilces = vehilces.OrderBy(x => x.MainSpeed).ToList();
        }

        static void Test_6_4(List<BaseVehicle> vehilces)
        {
            var listOfLandVehicle = new List<BaseVehicle>();
            foreach (var vehicle in vehilces)
            {
                if (vehicle.environment is IVehicle.Environment.Ziemia)
                   listOfLandVehicle.Add(vehicle);
            }
            listOfLandVehicle.OrderByDescending(x => x.MainSpeed).ToList().ForEach(vehicle => vehicle.ToString());
        }
    }
}