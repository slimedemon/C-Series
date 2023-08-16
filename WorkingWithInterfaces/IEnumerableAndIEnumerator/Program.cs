using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerableAndIEnumerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Garage g = new Garage();
            foreach (var car in g)
            {
                if (car is Car c)
                {
                    Console.WriteLine("Name of car is {0}, and price is {1}", c.Name, c.Price);
                }
            }

            Console.ReadLine();
        }
    }

    public class Car
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int price;
        public int Price
        { 
            get { return price; }
            set { price = value; }
        }
        public Car(string name = "", int price = 0) 
        {
            this.Name = name;
            this.Price = price;
        }
    }
    public class Garage : IEnumerable
    {
        private Car[] myCars = new Car[4];

        public Garage()
        {
            myCars[0] = new Car("Subisi", 10);
            myCars[1] = new Car("Tayako", 20);
            myCars[2] = new Car("HOBO", 30);
            myCars[3] = new Car("Cici", 40);
        }

        public IEnumerator GetEnumerator() 
        {
            //return myCar.GetEnumerator();

            foreach (var car in myCars)
            {
                yield return car;
            }
        }
    }
}
