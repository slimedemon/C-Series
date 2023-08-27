using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDomain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with the default AppDomain *****\n");
            AppDomain defaultAD = AppDomain.CurrentDomain;
            defaultAD.ProcessExit += (o, s) =>
            {
                Console.WriteLine("Default AD unloaded!");
            };

            InitDAD(defaultAD);
            DisplayDADStats(defaultAD);
            ListAllAssembliesInAppDomain(defaultAD);
            // Make new AppDomain
            MakeNewAppDomain();
            Console.ReadLine();
        }

        private static void DisplayDADStats(AppDomain ad)
        {
            // Print out various stats about this domain.
            Console.WriteLine("Name of this domain: {0}", ad.FriendlyName);
            Console.WriteLine("ID of domain in this process: {0}", ad.Id);
            Console.WriteLine("Is this the defautl domain!: {0}", ad.IsDefaultAppDomain());
            Console.WriteLine("Base directory of this domain: {0}", ad.BaseDirectory); ;
            Console.WriteLine();
        }

        private static void ListAllAssembliesInAppDomain(AppDomain ad)
        {
            //// Now get all loaded assemblies in the default AppDomain.
            //Assembly[] assemblies = ad.GetAssemblies();

            var assemblies = ad.GetAssemblies().OrderBy(p => p.GetName().Name).Select(p => p);
            // List all assemblies
            Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n", ad.FriendlyName);

            foreach (var assembly in assemblies)
            {
                Console.WriteLine("-> Name: {0}", assembly.GetName().Name);
                Console.WriteLine("-> Version: {0}\n", assembly.GetName().Version);
            }
            Console.WriteLine();
        }

        private static void InitDAD(AppDomain ad)
        {
            ad.AssemblyLoad += (o, s) =>
            {
                Console.WriteLine("{0} has been loaded!", s.LoadedAssembly.GetName().Name);
            };
            Console.WriteLine();
        }

        private static void MakeNewAppDomain()
        {
            // Make a new AppDomain in the current process.
            AppDomain newAD = AppDomain.CreateDomain("SecondAppDomain");
            newAD.DomainUnload += (o, s) =>
            {
                Console.WriteLine("The second AppDomain has been unloaded!");
            };
            try
            {
                // Now load CarLibrary.dll into this new domain.
                newAD.Load("CarLibrary");
            }
            catch (FileNotFoundException ex)
            {
                    Console.WriteLine(ex.Message);
            }
            // List all assemblies.
            ListAllAssembliesInAppDomain(newAD);
            // Now tear down this AppDomain.
            AppDomain.Unload(newAD);
        }

    }
}
