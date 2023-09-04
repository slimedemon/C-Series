using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ObjectSerialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserPrefs userData = new UserPrefs();
            userData.WindowColor = "Yellow";
            userData.FontSize = 50;

            // The BinaryFormatter persists state data in a binary format.
            // You would need to import System.Runtime.Serialization.Formatters.Binary
            // to gan access to BinaryFormatter.
            BinaryFormatter binFormat = new BinaryFormatter();

            // Store object in a local file
            using (Stream fStream = new FileStream("user.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, userData);
            }
            Console.ReadLine();
        }
    }
}
