﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal abstract class User
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int perm { get; set; }

        protected User(string name)
        {
            this.name = name;
            this.phone = "0";
            this.email = "test@test";
            this.perm = 0;
        }
    }
    internal class Customer : User
    {
        public Customer(string name) : base(name)
        {
            this.perm = 0;
        }
    }

    internal class Staff : User
    {
        public Staff(string name) : base(name)
        {
            this.perm = 1;
        }
    }

    internal class Admin : User
    {
        public Admin(string name) : base(name)
        {
            this.perm = 3;
        }
    }
}