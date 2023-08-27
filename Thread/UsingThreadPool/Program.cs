using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingThreadPool
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
                Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.ManagedThreadId);

                // Print out number
                Console.Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                {
                    Random rnd = new Random();
                    //Thread.Sleep(1000 * rnd.Next(5));
                    Thread.Sleep(1000);
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
            Console.WriteLine("***** Fun with the CLR Thread Pool *****\n");

            Console.WriteLine("Main thread started. ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);

            Printer p = new Printer();

            WaitCallback workItem = new WaitCallback(PrintTheNumbers);

            // Queue the method ten times.
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItem, p);
            }

            Console.WriteLine("All tasks queued");
            Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state;
            task.PrintNumbers();
        }
    }
}
