using System;
using System.Diagnostics;

namespace Labb3_Threads
{

    internal class Program
    {
        private static readonly object ConsoleLock = new object();
        static void Main(string[] args)
        {
            StartRace(new Car(), new Car());
        }
        public static void StartRace(Car car1, Car car2)
        {
            Thread firstCar = new Thread(() => StartCar(car1,1, "Volvo 142"));
            Thread secondCar = new Thread(() => StartCar(car2,2, "Porsche 911"));

            firstCar.Start();
            secondCar.Start();

            firstCar.Join();
            secondCar.Join();

            RaceEndCondtions(car1, car2);

        }
        public static void StartCar( Car car, int ID, string carName)
        {
            car = new Car()
            {
                ID = ID,
                Name = carName,
                CurrentSpeed = 120,
                DistanceTraveled = 0,
                TimeDriven = 0
           };

            Stopwatch sw = Stopwatch.StartNew();
            Stopwatch raceTime = Stopwatch.StartNew();
            sw.Start();
            raceTime.Start();


            int counter = 0;

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

                if (car.DistanceTraveled >= 150)
                {
                    Console.WriteLine("Race ended");

                    break;
                }

                lock (ConsoleLock)
                {
                    // Set the cursor position to the car's row and update the car's information
                    Console.SetCursorPosition(0, car.ID - 1);
                    Console.WriteLine($"#{car.ID} {car.Name}, " +
                        $"Distance: {Math.Round(car.DistanceTraveled)} km," +
                        $" {car.CurrentSpeed} km/h, {Math.Round(raceTime.Elapsed.TotalSeconds)+ "s"}");
                }
                if (sw.Elapsed.TotalSeconds > 32)
                {
                    sw.Restart();
                }
                
            }
            sw.Stop();
            raceTime.Stop();
        }

        public static void RaceEndCondtions(Car car1,Car car2)
        {
            Console.Clear();
            Console.WriteLine("Fetching results..");
            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine("*********** Race Complete ************");
            Console.WriteLine("ID\tCar\tDistance\tTime");
            Console.WriteLine();
            Console.WriteLine($"{car1.ID}\t{car1.Name}\t{car1.DistanceTraveled}\t{car1.TimeDriven}");
            Console.WriteLine($"{car2.ID}\t{car2.Name}\t{car2.DistanceTraveled}\t{car2.TimeDriven}");
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

                ConsoleHelper.WriteRed("WARNING: ");
                Console.WriteLine($"{car.ID} {car.Name} has gotten puncture, replacing tire will take 15 secounds");
                Thread.Sleep(15000);
                Console.Clear();

            }
        }

    }
}