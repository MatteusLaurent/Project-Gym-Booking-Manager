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
            //int activeUser;
            //User.Load();
            //Reservation.Load();
            //activeUser = User.LogIn();
            //User.users[activeUser].Menu();
            // Calendar.ViewCalender();

            // DAVIDS TEST:
            // TEST USERS:
            var userA = new Staff(0, "TOM", 1456874354, "012-5342-543", "tom@something.com", "", "");
            var userB = new Staff(0, "BEN", 1456874354, "012-5342-543", "ben@something.com", "", "");
            var userC = new Staff(0, "LARRY", 1456874354, "012-5342-543", "larry@something.com", "", "");

            // TEST CALENDER:
            var dateA = new DateTime(2023, 02, 17, 8, 0, 0);
            var dateAa = dateA.AddHours(2);
            var dateB = new DateTime(2023, 02, 16, 12, 0, 0);
            var dateBb = dateA.AddHours(1);
            var dateC = new DateTime(2023, 02, 17, 8, 0, 0);
            var dateCc = dateA.AddHours(3);
            var dateD = new DateTime(2023, 02, 13, 10, 0, 0);
            var dateDd = dateA.AddHours(4);
            var dateE = new DateTime(2023, 02, 16, 16, 0, 0);
            var dateEe = dateE.AddHours(1);
            var calA = new Calendars.Calendar(dateA, dateAa);
            var calB = new Calendars.Calendar(dateB, dateBb);
            var calC = new Calendars.Calendar(dateC, dateCc);
            var calD = new Calendars.Calendar(dateD, dateDd);
            var calE = new Calendars.Calendar(dateE, dateEe);

            // TEST RESVATIONS:
            var resA = new Reservation(0, "FIGHT", "", userA, calA, null);
            var resB = new Reservation(1, "WORKOUT", "", userB, calB, null);
            var resC = new Reservation(2, "JUMP", "", userB, calC, null);
            var resD = new Reservation(3, "RUN", "", userC, calD, null);
            var resE = new Reservation(4, "DANCE", "", userC, calE, null);
            Reservation.reservations.Add(resA);
            Reservation.reservations.Add(resB);
            Reservation.reservations.Add(resC);
            Reservation.reservations.Add(resD);
            Reservation.reservations.Add(resE);

            Calendars.Calendar.ViewCalender();

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