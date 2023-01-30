using System;
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
        //uniqueID?
        public string name { get; set; } // Here the "field" is private, but properties (access of the field) public here - this constellation being purely declarative without change in functionality
        public string phone { get; set; }
        public string email { get; set; }

        protected User(string name)
        {
            this.name = name;
            this.phone = "0";
            this.email = "test@test";
        }
    }
}