using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectContextApp
{
    internal class Program
    {
        private static void Main()
        { 
            // Object will display contextual info upon creation.
            SportsCar sport1 = new SportsCar();
            Console.WriteLine();

            SportsCar sport2 = new SportsCar();
            Console.WriteLine();

            SportsCarTS synchroSport = new SportsCarTS();
            Console.ReadLine();
        }
    }
}
