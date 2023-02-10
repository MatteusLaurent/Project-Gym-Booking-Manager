using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Calendars;

namespace Gym_Booking_Manager.Reservations
{
    public class Reservation
    {
        public static List<Reservation> reservations = new List<Reservation>();

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public User owner { get; set; }
        public List<Reservable> reservableList { get; set; }
        public Calendar date { get; set; }

        public Reservation(string name, string description, User owner, Calendar date, List<Reservable> itemss)
        {
            id = 0; // IdCounter():
            this.name = name;
            this.description = description;
            this.owner = owner;
            this.reservableList = reservableList;
            this.date = date;
        }
        public static void Load()
        {
            string[] lines = File.ReadAllLines("Reservations/Reservations.txt");
            foreach (string line in lines)
            {
                List<Reservable> reservables = new List<Reservable>();
                string[] strings = line.Split(";");
                for (int i=4; i<lines.Length; i++)
                {
                    reservables.Add(Reservable.reservables[int.Parse(strings[i])]);
                }
                reservations.Add(new Reservation(strings[0], strings[1], User.users[int.Parse(strings[2])], new Calendar(DateTime.Parse(strings[3]), DateTime.Parse(strings[3])), reservables));
            }
        }
        public void NewReservation()
        {
            // Register new reservations to list.
        }
        public void UpdateReservation()
        {
            // Update reservations from list.
        }
        public void DeleteReservation()
        {
            // Delete reservations from list.
        }
    }
    public class Reservable
    {
        public static List<Reservable> reservables = new List<Reservable>();

        int id;
        string name;
        string description;
        List<Reservation> reservations;
        public Reservable(string name, string description)
        {
            id = 0; // IdCounter():

            this.name = name;
            this.description = description;
            reservations = new List<Reservation>();

            reservables.Add(this);
        }
        public void NewReservable()
        {
            // Staff creates new reservables.
        }
        public void UpdateReservable()
        {
            // Staff updates existing reservables.
        }
        public void DeleteReservable()
        {
            // Staff deletes existing reservables.
        }
    }
    public class Equipment : Reservable
    {
        public Equipment(string name, string description, bool reserved)
            : base(name, description) { }
    }
    public class Space : Reservable
    {
        public Space(string name, string description, bool reserved)
            : base(name, description) { }
    }
    public class PTrainer : Reservable
    {
        private Staff instructor;
        public PTrainer(string name, string description, bool reserved, Staff instructor)
            : base(name, description)
        {
            this.instructor = instructor;
        }
    }
}
