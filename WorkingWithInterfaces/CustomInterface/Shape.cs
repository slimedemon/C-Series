using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomInterface
{
    internal abstract class Shape
    {
        private string name;
        public string Name 
        { 
            get { return name; }
            set { name = value; } 
        }
        public Shape() 
        {
            this.Name = "No name";
        }

        public Shape(string name)
        {
            this.Name = name;
        }
        public abstract byte GetNumberOfPoints();
    }


}
