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
            Console.WriteLine("<< ACTIVITY CALENDAR >>\n  (Select an option!)\n\n[1] - View weekly calendar.\n[2] - View monthly calendar.\n[ESC] - Cancel.");
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
                        try
                        {
                            currentWeek -= 1;
                            ViewCalendarWeek(currentWeek);
                        }
                        catch (ArgumentOutOfRangeException) 
                        { 
                            Console.WriteLine("System.ArgumentOutOfRangeException: Week parameter must be between 1 to 53.");
                            escape = true;
                            break;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        try
                        {
                            currentWeek += 1;
                            ViewCalendarWeek(currentWeek);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("System.ArgumentOutOfRangeException: Week parameter must be between 1 to 53.");
                            escape = true;
                            break;
                        }
                        break;
                    case ConsoleKey.V:

                        break;
                    case ConsoleKey.D:
                        Console.WriteLine("Select a day to view:");
                        Console.WriteLine("[1] - Monday");
                        Console.WriteLine("[2] - Tuesday");
                        Console.WriteLine("[3] - Wednesday");
                        Console.WriteLine("[4] - Thursday");
                        Console.WriteLine("[5] - Friday");
                        Console.WriteLine("[6] - Saturday");
                        Console.WriteLine("[7] - Sunday");

                        keyPressed = Console.ReadKey(true);
                        //ViewWeekDay(currentWeek, keyPressed.Key);
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

            Console.Clear();
            Console.WriteLine($"{"", -42}<< ACTIVITY CALENDAR, WEEK {weekNumber} >>\n");
            Console.WriteLine($"|{"",-6}|{monday.ToShortDateString(),-14}|{tuesday.ToShortDateString(),-14}|{wednesday.ToShortDateString(),-14}|{thursday.ToShortDateString(),-14}|{friday.ToShortDateString(),-14}|{saturday.ToShortDateString(),-14}|{sunday.ToShortDateString(),-14}|");
            Console.WriteLine($"|{"TIME",-6}|{"MONDAY",-14}|{"TUESDAY",-14}|{"WEDNESDAY",-14}|{"THURSDAY",-14}|{"FRIDAY",-14}|{"SATURDAY",-14}|{"SUNDAY",-14}|");
            Console.WriteLine($"|------|--------------|--------------|--------------|--------------|--------------|--------------|--------------|");
            (posX, posY) = Console.GetCursorPosition();

            for (int i = 0; i < 12; i++)
            {
                Console.WriteLine($"|{"",-6}|{"",-14}|{"",-14}|{"",-14}|{"",-14}|{"",-14}|{"",-14}|{"",-14}|");
            }

            // Loops through the timeslots of the day (08:00, 09.00,... 19:00).
            int addRows = 0;
            bool slotContent = false;
            for (int t = 8; t < 20; t++)
            {
                Console.SetCursorPosition(posX, posY + addRows);
                Console.Write($"|{t.ToString("00")}-{(t + 1).ToString("00")} |");

                // Loops through dayslots of the week (Monday = 1, Tuesday = 2,... Sunday = 0).
                for (int d = 0; d < 7; d++)
                {
                    int day = d + 1;
                    if (day == 7) day = 0;

                    slotContent = false;
                    foreach (Reservation r in weekReservations)
                    {

                        if ((int)r.date.timeFrom.DayOfWeek == day && r.date.timeFrom.Hour == t)
                        {
                            if (slotContent) addRows++;
                            string slotInfo = $"{r.id}. {r.name}";

                            if (slotInfo.Length > 14) slotInfo = slotInfo.Substring(0, 11) + "...";

                            Console.SetCursorPosition(posX + 8 + (15 * d), posY + addRows);
                            Console.Write($"{slotInfo,-14}|");

                            slotContent = true;
                        }
                    }
                    if (!slotContent)
                    {
                        Console.SetCursorPosition(posX + 8 + (15 * d), posY + addRows);
                        Console.Write($"{"",-14}|");
                    }
                }
                addRows++;
            }
            Console.WriteLine($"\n|------|--------------|--------------|--------------|--------------|--------------|--------------|--------------|");
            Console.WriteLine($"\n{"<< [LEFT.ARROW](Prev. Week)",-32}{"[V](View Act.)",-20}{"[D](View Day)",-18}{"[ESC](Cancel)",-18}{"[RIGHT ARROW](Next Week) >>",-25}");
        }
    }
}

