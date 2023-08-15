using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** First look at interfaces *****");
            string str = "Hello";
            OperatingSystem os = new OperatingSystem(PlatformID.Unix, new Version());
            SqlConnection sqlConnection = new SqlConnection();

            cloneMe(str);
            cloneMe(os);
            cloneMe(sqlConnection);

            Console.ReadLine();
        }

        private static void cloneMe(ICloneable obj)
        {
            Object theClone = obj.Clone();
            Console.WriteLine("Your clone is a: {0}", theClone.GetType().Name);
        }
    }
}
