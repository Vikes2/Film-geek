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

        public Director(string fn, string ln)
        {
            this.FirstName = fn;
            this.LastName = ln;
        }

    }
}
