using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadStartDelegate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** The Amazing Thread App *****\n");
            Console.WriteLine("Do you want [1] or [2] threads? ");
            string threadCount = Console.ReadLine();

            // Name the current thread.
            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary";

            // Make worker class.
            Printer p = new Printer();
            
            switch(threadCount)
            {
                case "1":
                    p.PrintNumbers();
                    break;
                case "2":
                    // Create new thread.
                    Thread backgroundThread = new Thread(new ThreadStart(p.PrintNumbers));
                    backgroundThread.Name = "Secondary";
                    backgroundThread.Start();
                    break;
                default:
                    Console.WriteLine("I do not know what you want... you get 1 thread");
                    p.PrintNumbers();
                    break;
            }

            // Do some additional work.
            MessageBox.Show("I'm busy!", "Work on main thread...");
            Console.ReadLine();
        }
    }
}
