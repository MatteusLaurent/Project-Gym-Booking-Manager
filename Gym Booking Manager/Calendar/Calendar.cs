using Gym_Booking_Manager.Activities;
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
        public static void ViewCalendarMenu()
        {
            ConsoleKeyInfo keyPressed;

            Console.Clear();
            Console.WriteLine($"<< ACTIVITY CALENDAR >>\n\n>> Select an option!\n{"- [1]",-8}View weekly calendar.\n{"- [2]",-8}View monthly calendar.\n{"- [ESC]",-8}Cancel.");

            keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.D1 || keyPressed.Key == ConsoleKey.NumPad1)
            {
                Task.Delay(250).Wait();
                SelectCalendarWeek();
            }
            else if (keyPressed.Key == ConsoleKey.D2 || keyPressed.Key == ConsoleKey.NumPad2)
            {
                Task.Delay(250).Wait();
                ViewCalendarMonth();
            }
            else if (keyPressed.Key == ConsoleKey.Escape)
            {
                Console.WriteLine(">> View calendar cancelled!");
                Task.Delay(500).Wait();
            }
            else
            {
                Console.WriteLine($">> Invalid key option: [{keyPressed.Key}], view calendar canceller!");
                Task.Delay(500).Wait();
            }

        }
        private static void SelectCalendarWeek()
        {
            int currentYear = ISOWeek.GetYear(DateTime.Now);
            int currentWeek = ISOWeek.GetWeekOfYear(DateTime.Now);
            bool escape = false;

            ViewCalendarWeek(currentYear, currentWeek);

            while (!escape)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.LeftArrow:
                        try
                        {
                            currentWeek -= 1;
                            ViewCalendarWeek(currentYear, currentWeek);
                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                            Console.WriteLine(">> System.ArgumentOutOfRangeException: The week parameter must be in the range 1 through 53. (Parameter 'week')");
                            escape = true;
                            break;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        try
                        {
                            currentWeek += 1;
                            ViewCalendarWeek(currentYear, currentWeek);
                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                            Console.WriteLine(">> System.ArgumentOutOfRangeException: The week parameter must be in the range 1 through 53. (Parameter 'week')");
                            escape = true;
                            break;
                        }
                        break;
                    case ConsoleKey.V:

                        break;
                    case ConsoleKey.D:
                        Console.WriteLine("\n>> Select a day to view:");
                        Console.WriteLine($"{"- [1]",-8}Monday");
                        Console.WriteLine($"{"- [2]",-8}Tuesday");
                        Console.WriteLine($"{"- [3]",-8}Wednesday");
                        Console.WriteLine($"{"- [4]",-8}Thursday");
                        Console.WriteLine($"{"- [5]",-8}Friday");
                        Console.WriteLine($"{"- [6]",-8}Saturday");
                        Console.WriteLine($"{"- [7]",-8}Sunday");
                        Console.WriteLine();

                        keyPressed = Console.ReadKey(true);
                        ViewWeekDay(currentYear, currentWeek, keyPressed);
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
        private static void ViewCalendarWeek(int year, int week)
        {
            // Filtering Reservation.reservations list based on weekNr of date.timeFrom
            List<Activity> weekActivities = Activity.activities.Where(a => ISOWeek.GetWeekOfYear(a.date.timeFrom) == week).ToList();

            // Sorting Reservation.reservations list based on date.timeFrom.
            weekActivities.Sort((x, y) => x.date.timeFrom.CompareTo(y.date.timeFrom));

            DateTime monday = ISOWeek.ToDateTime(year, week, DayOfWeek.Monday);
            DateTime tuesday = ISOWeek.ToDateTime(year, week, DayOfWeek.Tuesday);
            DateTime wednesday = ISOWeek.ToDateTime(year, week, DayOfWeek.Wednesday);
            DateTime thursday = ISOWeek.ToDateTime(year, week, DayOfWeek.Thursday);
            DateTime friday = ISOWeek.ToDateTime(year, week, DayOfWeek.Friday);
            DateTime saturday = ISOWeek.ToDateTime(year, week, DayOfWeek.Saturday);
            DateTime sunday = ISOWeek.ToDateTime(year, week, DayOfWeek.Sunday);

            Console.Clear();
            Console.WriteLine($"{"",-42}<< ACTIVITY CALENDAR, WEEK {week} >>\n");
            Console.WriteLine($"|{"",-6}|{monday.ToShortDateString(),-14}|{tuesday.ToShortDateString(),-14}|{wednesday.ToShortDateString(),-14}|{thursday.ToShortDateString(),-14}|{friday.ToShortDateString(),-14}|{saturday.ToShortDateString(),-14}|{sunday.ToShortDateString(),-14}|");
            Console.WriteLine($"|{"TIME",-6}|{"MONDAY",-14}|{"TUESDAY",-14}|{"WEDNESDAY",-14}|{"THURSDAY",-14}|{"FRIDAY",-14}|{"SATURDAY",-14}|{"SUNDAY",-14}|");
            Console.WriteLine($"|------|--------------|--------------|--------------|--------------|--------------|--------------|--------------|");

            int posX, posY;
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
                    foreach (Activity activity in weekActivities)
                    {

                        if ((int)activity.date.timeFrom.DayOfWeek == day && activity.date.timeFrom.Hour == t)
                        {
                            if (slotContent) addRows++;
                            string slotInfo = $"{activity.id}. {activity.name}";

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

        private static void ViewWeekDay(int year, int week, ConsoleKeyInfo key)
        {
            List<Activity> dayReservations = Activity.activities.Where(activity => ISOWeek.GetWeekOfYear(activity.date.timeFrom) == week).ToList();
            DayOfWeek dayOfWeek = DayOfWeek.Monday;

            if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1) dayOfWeek = DayOfWeek.Monday;
            if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2) dayOfWeek = DayOfWeek.Tuesday;
            if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3) dayOfWeek = DayOfWeek.Wednesday;
            if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4) dayOfWeek = DayOfWeek.Thursday;
            if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5) dayOfWeek = DayOfWeek.Friday;
            if (key.Key == ConsoleKey.D6 || key.Key == ConsoleKey.NumPad6) dayOfWeek = DayOfWeek.Saturday;
            if (key.Key == ConsoleKey.D7 || key.Key == ConsoleKey.NumPad7) dayOfWeek = DayOfWeek.Sunday;

            DateTime dateTime = ISOWeek.ToDateTime(year, week, dayOfWeek);

            Console.WriteLine($"<< ACTIVITY CALENDAR, WEEK {week} >>");
            Console.WriteLine($"    << {dayOfWeek} {dateTime.ToShortDateString()} >>");
            foreach (Activity activity in dayReservations)
            {
                if (activity.date.timeFrom.DayOfWeek == dayOfWeek)
                {
                    Console.WriteLine($"\n-  ACTIVITY ID: {activity.id}");
                    Console.WriteLine($"- ACTIVITY NAME: {activity.name}");
                    Console.WriteLine($"- ACTIVITY DESCRIPTION: {activity.description}");
                    // ADD MORE!!
                }
            }
        }
    }
}

