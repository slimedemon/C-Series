using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithCSharpAsync
{
    internal class Program
    {
        // Note: nonparallel version
        //static async Task Main(string[] args)
        //{
        //    Console.WriteLine("Fun With Async ===>");
        //    // This is to prompt Visual Studio to upgrade project to C# 7.1
        //    List<int> l = default;
        //    Console.WriteLine(DoWord());
        //    Console.WriteLine("Completed");
        //    Console.ReadLine();
        //}

        //static string DoWord()
        //{
        //    Thread.Sleep(5_000);
        //    return "Done with work!";
        //}

        //static async Task Main(string[] args)
        //{
        //    Console.WriteLine("Fun With Async ===>");
        //    Task<string> task = DoWorkAsync();
        //    Console.WriteLine("Completed");
        //    string message = await task;
        //    Console.WriteLine(message);
        //    Console.ReadLine();
        //}
        //static string DoWork()
        //{
        //    Thread.Sleep(5_000);
        //    return "Done with work!";
        //}
        //static async Task<string> DoWorkAsync()
        //{
        //    return await Task.Run(() =>
        //    {
        //        Thread.Sleep(5_000);
        //        return "Done with work!";
        //    });
        //}

        static async Task Main(string[] args)
        {
            //ommitted for brevity
            string message = await DoWorkAsync();
            Console.WriteLine(message);
            //ommitted for brevity
        }
        static string DoWork()
        {
            Thread.Sleep(5_000);
            return "Done with work!";
        }
        static async Task<string> DoWorkAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(5_000);
                return "Done with work!";
            });
        }
    }
}
