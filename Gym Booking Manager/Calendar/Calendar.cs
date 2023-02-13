using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Users;
using System.Collections.Generic;
using System.Globalization;

namespace Gym_Booking_Manager.Calendars
{
    public class Calendar
    {
        public DateTime timeFrom { get; set; }
        public DateTime timeTo { get; set; }

        public Calendar(DateTime timeFrom, DateTime timeTo)
        {
            this.timeFrom = start;
            this.timeTo = end;
        }
        public static void ViewCalender()
        {
            ConsoleKeyInfo keyPressed;
            Console.WriteLine("<< ACTIVITY CALENDER >>\n(Press key to pick an option)\n\n1. View weekly calendar.\n2. View monthly calendar.");
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1)
            {
                ViewCalenderWeek();
            }
            else if (keyPressed.Key == ConsoleKey.D2 || keyPressed.Key == ConsoleKey.NumPad2)
            {
                ViewCalenderMonth();
            }
            else if (keyPressed.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("View calendar cancelled!");
            }
            else
            {
                Console.WriteLine($"Invalid option ({keyPressed.Key}), view calender cancelled.");
            }
        }
        public static void ViewCalenderWeek()
        {
            Console.WriteLine("\n<< ACTIVITY WEEK CALENDER >>\n");
            Console.WriteLine($"|{"TIME:",-5}|{"MONDAY",-10}|{"TUESDAY",-10}|{"WEDNESDAY",-10}|{"THURSDAY",-10}|{"FRIDAY",-10}|{"SATURDAY",-10}|{"SUNDAY",-10}|");
            Console.WriteLine($"|-----|----------|----------|----------|----------|----------|----------|----------|");

            for (int i = 0; i < 12; i++)
            {
                int time = 8 + i;

            }
            Console.WriteLine($"|{"08-09",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"09-10",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"10-11",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"11-12",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"12-13",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"13-14",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"14-15",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"15-16",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"16-17",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"17-18",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"18-19",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");
            Console.WriteLine($"|{"19-20",-5}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|{"",-10}|");

        }
        public static void ViewCalenderMonth()
        {

        }
        public static void SortReservationWeek(int weekNr)
        {
            // Filtering Reservation.reservations list based on weekNr of date.timeFrom
            List<Reservation> weekReservations = Reservation.reservations.Where(w => ISOWeek.GetWeekOfYear(w.date.timeFrom) == weekNr).ToList();
            // Sorting Reservation.reservations list based on date.timeFrom.
            weekReservations.Sort((x, y) => x.date.timeFrom.CompareTo(y.date.timeFrom));

            // Creating new sorted 2-Dimensional list of reservations. 
            List<List<Reservation>> sortedReservations = new();
            List<Reservation> timeSlotReservation = new();

            // Loops through days of the week (Sunday = 0,... Saturday = 6).
            for (int d = 0; d <= 7; d++)
            {
                // Loops through the time slots of the day (08:00 ... 19:00).
                for (int t = 8; t < 20; t++)
                {
                    foreach (Reservation r in weekReservations)
                    {
                        if ((int)r.date.timeFrom.DayOfWeek == d && r.date.timeFrom.Hour == t)
                        {
                            timeSlotReservation.Add(r);
                        }
                    }
                    sortedReservations.Add(timeSlotReservation);
                    timeSlotReservation = new List<Reservation>();
                }
            }
            
            for (int i = 0; i < sortedReservations.Count; i++) 
            {
                if (sortedReservations[i].Count > 1)
                {

                }
                else
                {
                    Console.WriteLine(sortedReservations[i][0].date.timeFrom);
                }
                
            }
        }
    }
}

