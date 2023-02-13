using Gym_Booking_Manager.Activities;
using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Calendars;
using System.Globalization;
using System;
using Gym_Booking_Manager.Logger;

namespace Gym_Booking_Manager
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            GBMLogger logger = new GBMLogger("GBMLogger.txt");
            int activeUser=-1;
            User.Load();
            logger.LogActivity("User is loaded");
            //Reservable.Load();
            logger.LogActivity("Reservable is loaded");
            Reservation.Load();
            logger.LogActivity("Reservation is loaded");
            Activity.Load();            
            logger.LogActivity("Activity is loaded");
            activeUser = User.LogIn();
            if(activeUser!=-1)User.users[activeUser].Menu();
        }
    }
}