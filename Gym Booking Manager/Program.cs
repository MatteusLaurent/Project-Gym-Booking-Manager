using Gym_Booking_Manager.Users;
using System.Runtime.CompilerServices;

namespace Gym_Booking_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int activeUser;
            List <User> users = new List <User> ();
            User.LoadUsers(users);
            Console.WriteLine(users[0].name);
            activeUser=User.LogIn(users);
        }        
        // Static methods for the program
    }
}