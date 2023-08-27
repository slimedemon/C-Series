using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car(0, 100, "BBW");
            car.listOfHandler += OnCarEngineCar;
            car.listOfHandler += (WarnForHighSpeed);

            // speed up 
            for (int i = 20; i <= 120; i = i + 20)
            {
                car.Accelerate(20);
                if (car.CurrentSpeed >= 100) car.listOfHandler -= WarnForHighSpeed;
            }

            Console.ReadLine();
        }

        static void OnCarEngineCar(object sender, CarEventArgs args)
        {
            Console.WriteLine("\n***** Message From Car Object *****");
            Console.WriteLine("=> {0}", args.msg);
            Console.WriteLine("**********************************");
        }

        static void WarnForHighSpeed(object sender, CarEventArgs args)
        {
            Console.WriteLine("\n ***** Warn For High Speed *****");
            Console.WriteLine("Slow down! Your speed is over the allowance value!");
            Console.WriteLine("**********************************");
        }
    }

    public class CarEventArgs : EventArgs
    {
        public readonly string msg;

        public CarEventArgs(string message)
        {
            this.msg = message;
        }
    }

    public class Car
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        //Is the car alive or dead?
        private bool carIsDead;

        // Class constructors
        public Car()
        {
            CurrentSpeed = MaxSpeed = 0;
            PetName = "No name";
            carIsDead = false;

        }

        public Car(int currSpeed, int mxSpeed, string name)
        {
            this.CurrentSpeed = currSpeed;
            this.MaxSpeed = mxSpeed;
            this.PetName = name;
            carIsDead = false;
        }

        // 1) Define a delegate type.
        public delegate void CarEngineHanler(object sender, CarEventArgs args);

        // 2) Define member variable of this delegate.
        public event CarEngineHanler listOfHandler;

        public void Accelerate(int delta)
        {
            // If this car is "Dead", send dead message.
            if (carIsDead)
            {
                if (listOfHandler != null)
                {
                    listOfHandler(this, new CarEventArgs("Sorry, this car is dead..."));
                }
            }
            else
            {
                this.CurrentSpeed += delta;
                if ((this.MaxSpeed - this.CurrentSpeed) <= 10)
                {
                    listOfHandler(this, new CarEventArgs("Carefull buddy! Gonna blow!"));
                }

                if (this.CurrentSpeed >= this.MaxSpeed)
                {
                    carIsDead = true;
                }
                else
                {
                    Console.WriteLine("CurrentSpeed = {0}", this.CurrentSpeed);
                }
            }
        }
    }
}

