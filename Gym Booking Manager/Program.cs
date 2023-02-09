using Gym_Booking_Manager.Functionality;
using Gym_Booking_Manager.Users;

namespace Gym_Booking_Manager
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            int activeUser;
            User.Load();
            //Activity.Load();
            activeUser = User.LogIn();
            User.users[activeUser].Menu();
        }
    }
}