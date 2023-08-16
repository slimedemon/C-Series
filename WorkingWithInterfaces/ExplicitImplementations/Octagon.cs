using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplicitImplementations
{
    //class Octagon : IDrawToForm, IDrawToMemory, IDrawToPrinter
    //{
    //    public void Draw()
    //    {
    //        // Shared drawing logic.
    //        Console.WriteLine("Drawing the Octagon...");
    //    }
    //}

    class Octagon : IDrawToForm, IDrawToMemory, IDrawToPrinter
    {
        void IDrawToForm.Draw()
        {
            Console.WriteLine("Draw method of IDrawToForm");
        }

        void IDrawToMemory.Draw()
        {
            Console.WriteLine("Draw method of IDrawToMemory");
        }

        void IDrawToPrinter.Draw()
        {
            Console.WriteLine("Draw method of IDrawToPrinter");
        }
    }

    class TestMethod
    {
        private void Print()
        {
            Console.WriteLine("print method of TestMethod");
        }
    }
}
