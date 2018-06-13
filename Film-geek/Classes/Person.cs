﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{
    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Person()
        {

        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
