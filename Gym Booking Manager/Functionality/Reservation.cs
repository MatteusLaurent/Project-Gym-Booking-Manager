using Gym_Booking_Manager.Scheduling;
using Gym_Booking_Manager.Users;

namespace Gym_Booking_Manager.Reservations
{
    public class Reservation
    {
        public static List<Reservation> reservations = new List<Reservation>();

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public User owner { get; set; }
        public Reservable type { get; set; }
        public Calender date { get; set; }

        public Reservation(string name, string description, User owner, Reservable type, Calender date)
        {
            id = 0; // IdCounter():
            this.name = name;
            this.description = description;
            this.owner = owner;
            this.type = type;
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
        bool reserved;
        public Reservable(string name, string description, bool reserved)
        {
            id = 0; // IdCounter():

            this.name = name;
            this.description = description;
            this.reserved = reserved;

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
            : base(name, description, reserved) { }
    }
    public class Space : Reservable
    {
        public Space(string name, string description, bool reserved)
            : base(name, description, reserved) { }
    }
    public class PTrainer : Reservable
    {
        private Staff instructor;
        public PTrainer(string name, string description, bool reserved, Staff instructor)
            : base(name, description, reserved)
        {
            this.instructor = instructor;
        }
    }
}
