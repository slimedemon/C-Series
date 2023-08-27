using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForegroundAndBackgroundThread
{
    internal class Printer
    {
        public void PrintNumbers()
        {
            // Display Thread info.
            Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.ManagedThreadId);

            // Print out numbers.
            Console.WriteLine("Your numbers: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0}", i);
                Thread.Sleep(2000);
            }
            Console.WriteLine();
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Background Threads *****\n");
            Printer p = new Printer();
            Thread bgroundThread = new Thread(new ThreadStart(p.PrintNumbers));
            bgroundThread.IsBackground = true;
            bgroundThread.Start();
            Console.WriteLine("Do something in main function!");
            Console.ReadLine();
        }
    }
}
