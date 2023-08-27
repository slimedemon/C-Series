using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingInterlockedType
{
    public class InterLockedTypeTest
    {
        // interact with this varible
        public int val = 0;

        //// Using lock keyword to synchronize access to shared resource.
        //private object lockThread = new object();
        //public void AddOne()
        //{
        //    lock (lockThread)
        //    {
        //        val++;
        //    }
        //}

        // This is a way instead of using lock keyword.
        public void AddOne()
        {
            int newVal = Interlocked.Increment(ref val);
        }

        public void SafeAssigment()
        {
            Interlocked.Exchange(ref val, 83);
        }

        public void CompareAndExchange()
        {
            // If the value of i is currently 83, change i to 99.
            Interlocked.CompareExchange(ref val, 99, 83);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Using InterLocked type for synchronize access *****\n");

            // Print out thread ID
            Console.WriteLine("Thread ID {0} is working on Main", Thread.CurrentThread.ManagedThreadId);

            InterLockedTypeTest t = new InterLockedTypeTest();

            Thread[] threads = new Thread[3];
            threads[0] = new Thread(new ParameterizedThreadStart(UsingAddOne));
            threads[1] = new Thread(new ParameterizedThreadStart(UsingSafeAssignment));
            threads[2] = new Thread(new ParameterizedThreadStart(UsingCompareAndExchange));

            foreach (Thread thread in threads)
            {
                thread.Start(t);
            }

            Console.ReadLine();
        }

        private static void UsingAddOne(object data)
        {
            if (data is InterLockedTypeTest t)
            {
                for (int i = 0; i < 10; i++)
                {
                    t.AddOne();
                    Console.WriteLine("-> AddOne: {0}", t.val);
                }
            }
        }

        private static void UsingSafeAssignment(object data)
        {
            if (data is InterLockedTypeTest t)
            {
                t.SafeAssigment();
                Console.WriteLine("-> UsingSafeAssignment: {0}", t.val);
            }
        }

        private static void UsingCompareAndExchange(object data)
        {
            if (data is InterLockedTypeTest t)
            {
                t.CompareAndExchange();
                Console.WriteLine("-> UsingCompareAndExchange: {0}", t.val);
            }
        }
    }
}
