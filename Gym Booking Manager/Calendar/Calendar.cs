﻿using Gym_Booking_Manager.Reservations;

namespace Gym_Booking_Manager.Calendars
{
    public class Calendar
    {
        private DateTime timeFrom;
        private DateTime timeTo;
        public Calendar()
        {

        }
        public static void ViewCalender()
        {
            ConsoleKeyInfo keyPressed;
            Console.WriteLine(".....View Calender.....\n(Press key to pick an option)\n\n1. View weekly calendar.\n2. View monthly calendar.");
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

        }
        public static void ViewCalenderMonth()
        {

        }
    }
}