using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSynchronization
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
            int answer = b(10, 10);

            // Print out result of Add()
            Console.WriteLine("10 + 10 = {0}", answer);
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
    }
}
