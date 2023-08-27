using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingTheLockKeyword
{
    public class Printer
    {
        // Lock token.
        private object threadLock = new object();

        public void PrintNumbers()
        {
            // Use the lock token.
            lock (threadLock)
            {
                // Display thread info.
                Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name);

                // Print out number
                Console.Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                { 
                    Random rnd = new Random();
                    // Thread.Sleep(1000 * rnd.Next(5));
                    Console.Write("{0}", i);
                }
                Console.WriteLine();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Synchronizing Threads *****\n");

            Printer p = new Printer();

            // Make 10 threads that are all pointing to the same 
            // method on the same object.

            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers))
                {
                    Name = $"Worker thread #{i}"
                };
            }

            // Now start print out number;
            foreach (Thread t in threads)
            {
                t.Start();
            }

            Console.ReadLine();
        }
    }
}
