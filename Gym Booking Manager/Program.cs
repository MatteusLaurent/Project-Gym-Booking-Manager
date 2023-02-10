using Gym_Booking_Manager.Activities;
using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Calendars;

namespace Gym_Booking_Manager
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            int activeUser;
            User.Load();
            Reservation.Load();
            activeUser = User.LogIn();
            User.users[activeUser].Menu();
        }
    }
}