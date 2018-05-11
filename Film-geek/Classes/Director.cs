using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{
    public class Director : Person
    {
        public Director()
        {

        }

        public Director(string name, string surname)
        {
            this.FirstName = name;
            this.LastName = surname;
        }
    }
}
