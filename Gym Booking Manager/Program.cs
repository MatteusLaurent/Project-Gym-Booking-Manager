using Gym_Booking_Manager.Activities;
using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Calendars;
using System.Globalization;
using System;

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
            var userA = new Staff(0, "TOM", "145687-4354", "012-5342-543", "tom@something.com", "", "");
            var userB = new Staff(0, "BEN", "145687-4354", "012-5342-543", "ben@something.com", "", "");
            var userC = new Staff(0, "LARRY", "145687-4354", "012-5342-543", "larry@something.com", "", "");

            // TEST CALENDER:
            var dateA = new DateTime(2023, 02, 17, 8, 0, 0);
            var dateAa = dateA.AddHours(2);
            var dateB = new DateTime(2023, 02, 16, 8, 0, 0);
            var dateBb = dateA.AddHours(1);
            var dateC = new DateTime(2023, 02, 14, 8, 0, 0);
            var dateCc = dateA.AddHours(3);
            var dateD = new DateTime(2023, 02, 13, 8, 0, 0);
            var dateDd = dateA.AddHours(4);
            var dateE = new DateTime(2023, 02, 16, 16, 0, 0);
            var dateEe = dateE.AddHours(1);
            var calA = new Calendars.Calendar(dateA, dateAa);
            var calB = new Calendars.Calendar(dateB, dateBb);
            var calC = new Calendars.Calendar(dateC, dateCc);
            var calD = new Calendars.Calendar(dateD, dateDd);
            var calE = new Calendars.Calendar(dateE, dateEe);

            // TEST RESVATIONS:
            var resA = new Reservation("FIGHT", "", userA, calA);
            var resB = new Reservation("WORKOUT", "", userB, calB);
            var resC = new Reservation("JUMP", "", userB, calC);
            var resD = new Reservation("RUN", "", userC, calD);
            var resE = new Reservation("DANCE", "", userC, calE);
            Reservation.reservations.Add(resA);
            Reservation.reservations.Add(resB);
            Reservation.reservations.Add(resC);
            Reservation.reservations.Add(resD);
            Reservation.reservations.Add(resE);

            Calendars.Calendar.SortReservationWeek(7);

            //Console.WriteLine($"{resA.date.timeFrom.DayOfWeek} Week: {ISOWeek.GetWeekOfYear(resA.date.timeFrom)}");
            //Console.WriteLine($"{resB.date.timeFrom.DayOfWeek} Week: {ISOWeek.GetWeekOfYear(resB.date.timeFrom)}");
            //Console.WriteLine($"{resC.date.timeFrom.DayOfWeek} Week: {ISOWeek.GetWeekOfYear(resC.date.timeFrom)}");
            //Console.WriteLine($"{resD.date.timeFrom.DayOfWeek} Week: {ISOWeek.GetWeekOfYear(resD.date.timeFrom)}");
            //Console.WriteLine($"{resE.date.timeFrom.DayOfWeek} Week: {ISOWeek.GetWeekOfYear(resE.date.timeFrom)}");
        }
    }
}