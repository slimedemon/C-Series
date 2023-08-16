using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomInterface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shape[] myShapes = { new Triangle("dat"), new Triangle("tin")};
            foreach (var shape in myShapes)
            {
                /*
                Triangle t = shape as Triangle;
                if (t != null)
                {
                    Console.WriteLine("Class Triangle, name: {0}", t.Name);
                    Console.WriteLine("Number of points: {0}", t.Points);
                }
                */

                // try{} catch(InvalidCastException e){ }

                if (shape is Triangle t)
                {
                    Console.WriteLine("Class Triangle, name: {0}", t.Name);
                    Console.WriteLine("Number of points: {0}", t.Points);
                }
            }
            
            Console.ReadLine();
        }
    }
}
