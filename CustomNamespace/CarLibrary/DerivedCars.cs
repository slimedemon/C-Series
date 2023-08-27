using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarLibrary
{
    public class SportsCar : Car
    {
        public SportsCar() { }
        public SportsCar(string petName, int currentSpeed, int maxSpeed) : base(petName, currentSpeed, maxSpeed) { }
        public override void turboBoost()
        {
            MessageBox.Show("Ramming speed!", "Faster is better ...");
        }
    }

    public class MiniVan : Car
    { 
        public MiniVan() { }
        public MiniVan(string petName, int currentSpeed, int maxSpeed) : base(petName, currentSpeed, maxSpeed) { }

        public override void turboBoost()
        {
            // MiniVan have poor turbo capabilities!
            egnState = EngineState.engineDead;
            MessageBox.Show("Eek!", "Your engine block exploded!");
        }
    }
}
