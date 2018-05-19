using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{
    public class Actor : Person
    {
        public Actor()
        {

        }

        public Actor(string name, string surname)
        {
            this.FirstName = name;
            this.LastName = surname;
        }
    }
}
