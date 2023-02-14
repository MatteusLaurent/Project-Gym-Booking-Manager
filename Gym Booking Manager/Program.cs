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
        public static int activeUser = -1;
        static void Main(string[] args)
        {       
            User.Load();
            Reservable.Load();
            Reservation.Load();
            Activity.Load();
            Console.WriteLine("antal i listan reserverade " + Reservation.reservations[0].reservableList.Count()+" date time ");
            activeUser = User.LogIn();
            if(activeUser!=-1)User.users[activeUser].Menu();
        }
    }
}