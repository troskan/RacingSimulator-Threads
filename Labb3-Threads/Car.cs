using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_Threads
{
    internal class Car
    {
        private static readonly object ConsoleLock = new object();

        public int ID { get; set; }
        public string Name { get; set; }
        public int CurrentSpeed { get; set; }
        public double DistanceTraveled { get; set; }
        public static Car[] FinishedCars = new Car[1];
        public double TimeDriven { get; set; }

        public static void StartCar(Car car, int ID, string carName)
        {

            car.ID = ID;
            car.Name = carName;
            car.CurrentSpeed = 120;
            car.DistanceTraveled = 0;
            car.TimeDriven = 0;


            Stopwatch sw = Stopwatch.StartNew();
            Stopwatch raceTime = Stopwatch.StartNew();
            sw.Start();
            raceTime.Start();


            int counter = 0;
            Console.WriteLine($"{car.Name} is getting ready to race...");
            Thread.Sleep(3000);
            for (double i = car.DistanceTraveled; i <= 1000; car.DistanceTraveled++)
            {
                Thread.Sleep(50);

                double time = raceTime.Elapsed.TotalHours; // 50 milliseconds = 0.05 hours
                double distance = car.CurrentSpeed * time;
                car.DistanceTraveled += distance;
                if (sw.Elapsed.TotalSeconds > 9 && sw.Elapsed.TotalSeconds < 11)

                {

                    NoFuel(car);
                    TirePuncture(car);
                    BirdCollision(car);
                    EngineFailure(car);

                }

                if (car.DistanceTraveled >= 100) { break; }
               

                lock (ConsoleLock)
                {
                    // Set the cursor position to the car's row and update the car's information
                    Console.SetCursorPosition(0, car.ID - 1);
                    Console.WriteLine($"#{car.ID} {car.Name}, " +
                        $"Distance: {Math.Round(car.DistanceTraveled)} km," +
                        $" {car.CurrentSpeed} km/h, {Math.Round(raceTime.Elapsed.TotalSeconds) + "s"}");
                }
                if (sw.Elapsed.TotalSeconds > 32)
                {
                    sw.Restart();
                }

            }
            if (FinishedCars[0] == null)
            {
                FinishedCars[0] = car;
                Console.SetCursorPosition(0, car.ID + 6);
                Console.WriteLine($"{car.Name} has finished the race first and is declared winner!");
                Thread.Sleep(5000);
            }
            else
            {
                Console.SetCursorPosition(0, car.ID + 6);
                Console.WriteLine($"{car.Name} has finished the race and is declared as looser!");
            }


            sw.Stop();
            raceTime.Stop();
            car.TimeDriven = raceTime.Elapsed.Seconds;

        }

        public static void RaceEndCondtions(Car car1, Car car2)
        {
            Console.Clear();
            ConsoleHelper.WriteGreen("\tFetching results..");
            Console.WriteLine("");
            Thread.Sleep(3000);
            Console.Clear();

            ConsoleHelper.WriteGreen("\t*********** Race Complete ************");
            Console.WriteLine();
            ConsoleHelper.WriteRed("ID\tCar\t\tDistance\tTime");
            Console.WriteLine();
            Console.WriteLine($"{car1.ID}\t{car1.Name}\t{Math.Round(car1.DistanceTraveled)} km\t\t{car1.TimeDriven} secounds");
            Console.WriteLine($"{car2.ID}\t{car2.Name}\t{Math.Round(car1.DistanceTraveled)} km\t\t{car2.TimeDriven} secounds");
            Console.ReadKey();
        }
        public static void NoFuel(Car car)
        {
            Random random = new Random();
            int noFuel = random.Next(0, 50);

            if (noFuel == 1)
            {
                Console.SetCursorPosition(0, car.ID + 3);
                ConsoleHelper.WriteRed("WARNING: ");
                Console.WriteLine($"{car.ID} {car.Name} has run out of fuel! Refueling will take 10 secounds!");
                Thread.Sleep(10000);
                Console.Clear();

            }
        }
        public static void TirePuncture(Car car)
        {
            Random random = new Random();
            int punctureOdds = random.Next(0, 25);


            if (punctureOdds == 1)
            {
                Console.SetCursorPosition(0, car.ID + 3);

                ConsoleHelper.WriteRed("WARNING: ");
                Console.WriteLine($"{car.ID} {car.Name} has gotten puncture, replacing tire will take 15 secounds");
                Thread.Sleep(15000);
                Console.Clear();
            }
        }
        public static void BirdCollision(Car car)
        {
            Random random = new Random();
            int collisionOdds = random.Next(0, 10);


            if (collisionOdds == 1)
            {
                Console.SetCursorPosition(0, car.ID + 3);

                ConsoleHelper.WriteRed("WARNING: ");
                Console.WriteLine($"{car.ID} {car.Name} has crashed with a seagull, starting windshield wipers! {car.Name} will stop for 10 secounds!");
                Thread.Sleep(15000);
                Console.Clear();

            }
        }

        public static void EngineFailure(Car car)
        {
            Random random = new Random();
            int engineFailureOdds = random.Next(0, 25);


            if (engineFailureOdds == 1)
            {
                Console.SetCursorPosition(0, car.ID + 3);

                car.CurrentSpeed -= 1;
                ConsoleHelper.WriteRed("WARNING: ");
                Console.WriteLine($"{car.ID} {car.Name} has gotten puncture, replacing tire will take 15 secounds");
                Thread.Sleep(15000);
                Console.Clear();

            }
        }

    }


}
