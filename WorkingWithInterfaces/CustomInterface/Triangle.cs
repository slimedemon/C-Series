using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomInterface
{
    internal class Triangle : Shape, IPointy
    {
        public Triangle() { }

        public Triangle(string name) : base(name) { }
        public override byte GetNumberOfPoints()
        {
            throw new NotImplementedException();
        }

        public byte Points
        {
            get { return 3; }
        }
    }
}
