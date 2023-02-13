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
            int activeUser=-1;
            User.Load();
            Reservable.Load();
            Reservation.Load();
            Activity.Load();            
            activeUser = User.LogIn();
            if(activeUser!=-1)User.users[activeUser].Menu();
        }
    }
}