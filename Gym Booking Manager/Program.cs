using Gym_Booking_Manager.Users;
using System.Runtime.CompilerServices;

namespace Gym_Booking_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List <User> users = new List <User> ();
            User.LoadUsers(users);
            Console.WriteLine(users[0].name);
        }        
        // Static methods for the program
    }
}