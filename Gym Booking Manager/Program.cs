using Gym_Booking_Manager;
using System.Runtime.CompilerServices;

namespace Gym_Booking_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List <User> users = new List <User> ();
            string[] lines = File.ReadAllLines("Users.txt");
            foreach (string line in lines)
            {
                string[] strings = line.Split(";");
                if (strings[0]=="Staff")users.Add (new Staff(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7]));
                if (strings[0] == "Admin") users.Add(new Admin(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7]));
                if (strings[0] == "Customer") users.Add(new Customer(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7], DateTime.Parse(strings[8]), DateTime.Parse(strings[9]), bool.Parse(strings[10])));
            }
        }        
        // Static methods for the program
    }
}