using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfiguringObjectsForSerialization
{
    [Serializable]
    public class Person
    {
        // A public filed.
        public bool isAlive = true;

        // A private filed.
        private int personAge = 21;

        // Publi propertyp/private data.
        private string fName = string.Empty;
        public string FirstName
        {
            get { return fName; }
            set { fName = value; }
        }
    }
}
