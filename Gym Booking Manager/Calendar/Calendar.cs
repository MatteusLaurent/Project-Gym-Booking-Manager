using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Users;

namespace Gym_Booking_Manager.Calenda
{
    public class Calendar
    {
        public static List<Calendar> calendar = new List<Calendar>();
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int id { get; set; }
        public Calendar(int id, DateTime start, DateTime end)
        {
            this.id = id;
            this.start = start;
            this.end = end;
        }
        public void Load()
        {
            string[] lines = File.ReadAllLines("Calendar/Calendar.txt");
            foreach (string line in lines)
            {
                string[] strings = line.Split(";");
                calendar.Add(new Calendar(int.Parse(strings[0]), DateTime.Parse(strings[1]), DateTime.Parse(strings[2])));
            }
        }
    }
}
