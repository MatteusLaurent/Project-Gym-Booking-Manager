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
            this.timeFrom = timeFrom;
            this.timeTo = timeTo;
        }
        public static void ViewCalender()
        {
            ConsoleKeyInfo keyPressed;
            Console.WriteLine("<< ACTIVITY CALENDAR >>\n(Press an appropriate key)\n[1] - View weekly calendar.\n[2] - View monthly calendar.\n[ESC] - Cancel.");
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1)
            {
                SelectCalendarWeek();
            }
            else if (keyPressed.Key == ConsoleKey.D2 || keyPressed.Key == ConsoleKey.NumPad2)
            {
                ViewCalendarMonth();
            }
            else if (keyPressed.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("View calendar cancelled!");
                Task.Delay(500).Wait();
            }
            else
            {
                Console.WriteLine($"Invalid key option [{keyPressed.Key}], view calendar cancelled.");
                Task.Delay(500).Wait();
            }
        }
        private static void SelectCalendarWeek()
        {
            int currentWeek = ISOWeek.GetWeekOfYear(DateTime.Now);
            bool escape = false;
            int posX, posY;
            (posX, posY) = Console.GetCursorPosition();

            ViewCalendarWeek(currentWeek);

            while (!escape)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(posX, posY);
                        currentWeek -= 1;
                        ViewCalendarWeek(currentWeek);
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(posX, posY);
                        currentWeek += 1;
                        ViewCalendarWeek(currentWeek);
                        break;
                    case ConsoleKey.V:

                        break;
                    case ConsoleKey.Escape:
                        escape = true;
                        break;
                    default: break;
                }
            }
        }
        private static void ViewCalendarMonth()
        {

        }
        private static void ViewCalendarWeek(int weekNumber)
        {
            int currentYear = ISOWeek.GetYear(DateTime.Now);
            int posX, posY;

            // Filtering Reservation.reservations list based on weekNr of date.timeFrom
            List<Reservation> weekReservations = Reservation.reservations.Where(w => ISOWeek.GetWeekOfYear(w.date.timeFrom) == weekNumber).ToList();

            // Sorting Reservation.reservations list based on date.timeFrom.
            weekReservations.Sort((x, y) => x.date.timeFrom.CompareTo(y.date.timeFrom));

            DateTime monday = ISOWeek.ToDateTime(currentYear, weekNumber, DayOfWeek.Monday);
            DateTime tuesday = ISOWeek.ToDateTime(currentYear, weekNumber, DayOfWeek.Tuesday);
            DateTime wednesday = ISOWeek.ToDateTime(currentYear, weekNumber, DayOfWeek.Wednesday);
            DateTime thursday = ISOWeek.ToDateTime(currentYear, weekNumber, DayOfWeek.Thursday);
            DateTime friday = ISOWeek.ToDateTime(currentYear, weekNumber, DayOfWeek.Friday);
            DateTime saturday = ISOWeek.ToDateTime(currentYear, weekNumber, DayOfWeek.Saturday);
            DateTime sunday = ISOWeek.ToDateTime(currentYear, weekNumber, DayOfWeek.Sunday);

            Console.WriteLine($"\n<< ACTIVITY WEEK {weekNumber} CALENDAR >>\n");
            Console.WriteLine($"|{"",-5}|{monday.ToShortDateString(),-10}|{tuesday.ToShortDateString(),-10}|{wednesday.ToShortDateString(),-10}|{thursday.ToShortDateString(),-10}|{friday.ToShortDateString(),-10}|{saturday.ToShortDateString(),-10}|{sunday.ToShortDateString(),-10}|");
            Console.WriteLine($"|{"TIME",-5}|{"MONDAY",-10}|{"TUESDAY",-10}|{"WEDNESDAY",-10}|{"THURSDAY",-10}|{"FRIDAY",-10}|{"SATURDAY",-10}|{"SUNDAY",-10}|");
            Console.WriteLine($"|-----|----------|----------|----------|----------|----------|----------|----------|");

            // Loops through days of the week (Monday = 1, Tuesday = 2,... Sunday = 0).
            for (int t = 8; t < 20; t++)
            {
                Console.Write($"|{t.ToString("00")}-{(t + 1).ToString("00")}|");
                (posX, posY) = Console.GetCursorPosition();

                // Loops through the time slots of the day (08:00 ... 19:00).
                for (int d = 0; d < 7; d++)
                {
                    int day = d + 1;
                    if (day == 7) day = 0;

                    int amount = 0;
                    foreach (Reservation r in weekReservations)
                    {
                        if ((int)r.date.timeFrom.DayOfWeek == day && r.date.timeFrom.Hour == t)
                        {
                            Console.SetCursorPosition(posX + (11 * d), posY);
                            Console.Write($"{$"{r.id}. {r.name}", -10}|");
                            amount++;
                        }
                    }

                    if (amount == 0)
                    {
                        Console.Write($"{"",-10}|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\n{"<< [LEFT.ARROW](Prev. Week)",-29}{"[ESC](Cancel)",-15}{"[V](View Act.)",-16}{"[RIGHT ARROW](Next Week) >>",-25}");
        }
    }
}

