using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    //Represents the state of the engine.
    public enum EngineState
    {
        engineAlive, engineDead
    }

    // The abstract class base in the hierarchy.
    public abstract class Car
    {
        public string PetName { get; set; }
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }

        protected EngineState egnState = EngineState.engineAlive;
        public EngineState EngineState => egnState;
        public abstract void turboBoost();

        public Car() { }
        public Car(string petName, int currentSpeed, int maxSpeed)
        {
            PetName = petName;
            CurrentSpeed = currentSpeed;
            MaxSpeed = maxSpeed;
        }
    }

}
