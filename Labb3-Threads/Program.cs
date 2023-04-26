using System;
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
            Thread firstCar = new Thread(() => Car.StartCar(car1,1, "Volvo 142"));
            Thread secondCar = new Thread(() => Car.StartCar(car2,2, "Porsche 911"));

            firstCar.Start();
            secondCar.Start();

            firstCar.Join();
            secondCar.Join();

            Car.RaceEndCondtions(car1, car2);

        }
        

    }
}