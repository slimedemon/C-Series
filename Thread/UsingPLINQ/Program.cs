using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingPLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start any key to start processing");
            Console.ReadLine();

            Console.WriteLine("Processing");
            Task.Factory.StartNew(() => ProcessIntData());
            Console.ReadLine();
        }

        static void ProcessIntData()
        {
            // Get a very large array of intergers.
            int[] source = Enumerable.Range(1, 90_000_000).ToArray();

            //// Find the numbers where num % 3 == 0 is true, returned
            //// in descending order.
            //// Note: nonparallel version.
            //int[] modThreeIsZero = (from num in source where num % 3 == 0 orderby num descending select num).ToArray();

            // Note: parallel version.
            int[] modThreeIsZero = (from num in source.AsParallel() where num % 3 == 0 orderby num descending select num).ToArray();
            Console.WriteLine($"Found {modThreeIsZero.Count()} numbers that match query!");
        }
    }
}
