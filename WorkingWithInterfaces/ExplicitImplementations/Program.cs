using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplicitImplementations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Octagon o = new Octagon();
            ((IDrawToPrinter)o).Draw();
        }
    }
}
