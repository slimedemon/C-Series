using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarLibrary;

using The3DHexagon = CustomNamespace.My3DShapes.Hexagon;
using bfHome = System.Runtime.Serialization.Formatters.Binary;
namespace CustomNamespace

{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyShapes.Hexagon hexagon = new MyShapes.Hexagon();
            MyShapes.Circle circle = new MyShapes.Circle();
            MyShapes.Square square = new MyShapes.Square();
            My3DShapes.Circle circle3D = new My3DShapes.Circle();
            The3DHexagon hexagon3D = new The3DHexagon();
            bfHome.BinaryFormatter b = new bfHome.BinaryFormatter();
            SportsCar viper = new SportsCar();
            viper.turboBoost();
        }
    }
}
