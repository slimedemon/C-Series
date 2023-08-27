using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParameterizedThreadStartDelegate
{
    public class AddParams
    {
        public int a, b;
        public AddParams(int num1, int num2)
        {
            this.a = num1; this.b = num2;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Adding with Thread objects *****");
            Console.WriteLine("ID of thread in Main(): {0}", Thread.CurrentThread.ManagedThreadId);

            AddParams ap = new AddParams(10, 10);
            Thread t = new Thread(new ParameterizedThreadStart(Add));
            t.Start(ap);

            // Force a wait to let other thread finish.
            //Thread.Sleep(5);

            Console.ReadLine();
        }

        private static void Add(object data)
        {
            if (data is AddParams ap)
            {
                Console.WriteLine("ID of thread in Add(): {0}", Thread.CurrentThread.ManagedThreadId);

                Console.WriteLine("{0} + {1} = {2}", ap.a, ap.b, ap.a + ap.b);
            }
        }
    }
}
