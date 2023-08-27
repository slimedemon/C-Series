using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousNature
{
    public delegate int BinaryOp(int x, int y);
    internal class Program
    {
        private static bool isDone = false;
        static void Main(string[] args)
        {
            // Print out the ID of the executing thread.
            Console.WriteLine("Main() invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);

            // Create BinaryOp delegate
            BinaryOp b = new BinaryOp(Add);

            // Call Add()
            IAsyncResult ar = b.BeginInvoke(10, 10, new AsyncCallback(AddComplete), null);

            // Check Add is complete
            while (!isDone)
            {
                Console.WriteLine("Working...");
                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }

        private static int Add(int x, int y)
        {
            // Print out the ID of the executing thread
            Console.WriteLine("Add() invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);

            // Pause to simulate a lengthy operation.
            Thread.Sleep(5000);
            return x + y;
        }

        private static void AddComplete(IAsyncResult iar)
        {
            // Print out the ID of the executing thread
            Console.WriteLine("AddComplete() invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("Your addition is complete");
            isDone = true;
        }
    }
}
