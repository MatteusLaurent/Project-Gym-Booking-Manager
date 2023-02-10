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

        public Reservation(string name, string description, User owner, Reservable type, Calendar date)
        {
            id = 0; // IdCounter():
            this.name = name;
            this.description = description;
            this.owner = owner;
            this.reservableList = reservableList;
            this.date = date;

            reservations.Add(this);
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
