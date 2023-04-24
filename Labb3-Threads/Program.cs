using System.Diagnostics;

namespace Labb3_Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartRace(new Car(), new Car());
        }
        public static void StartRace(Car car1, Car car2)
        {
            Thread firstCar = new Thread(() => StartCar(car1,1, "Volvo 142"));
            Thread secondCar = new Thread(() => StartCar(car2,2, "Porsche 911"));

            firstCar.Start();

        }
        public static void StartCar( Car car, int ID, string carName)
        {
            car = new Car()
            {
                ID = ID,
                Name = carName,
                CurrentSpeed = 120,
                DistanceTraveled = 800,
                TimeDriven = 0
           };
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            int counter = 0;
            for (double i = car.DistanceTraveled; i <= 1000; car.DistanceTraveled++) 
            {
                Thread.Sleep(50);
                Console.Clear();

                double time = 0.05; // 50 milliseconds = 0.05 hours
                double distance = car.CurrentSpeed * time;
                car.DistanceTraveled += distance;

                if (car.DistanceTraveled >= 1000)
                {
                    Console.WriteLine("Race ended");

                    break;
                }


                Console.WriteLine($"#{car.ID} {car.Name}, Distance: {car.DistanceTraveled} km, {car.CurrentSpeed} km/h, {sw.Elapsed}");
                Console.WriteLine($"Counter: {counter++}");
                
            }
            sw.Stop();

        }
    }
}