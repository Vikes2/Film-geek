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

        public Actor(string fn, string ln)
        {
            this.FirstName = fn;
            this.LastName = ln;
        }
    }
}
