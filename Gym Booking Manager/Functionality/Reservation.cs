using Gym_Booking_Manager.Users;

namespace Gym_Booking_Manager.Reservations
{
    public class Reservation
    {
        public List<Reservation> reservations = new List<Reservation>();

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public User owner { get; set; }
        public Reservable type { get; set; }
        public Calendar date { get; set; }

        public Reservation(string name, string description, User owner, Reservable type, Calendar date)
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

        }
        public void UpdateReservation()
        {

        }
        public void DeleteReservation()
        {

        }
    }
    public class Reservable
    {
        public List<Reservable> reservables = new List<Reservable>();

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

        }
        public void UpdateReservable()
        {

        }
        public void DeleteReservable()
        {

        }
    }
    public class Equipment : Reservable
    {
        private string type;
        public Equipment(string name, string description, bool reserved, string type)
            : base(name, description, reserved)
        {
            this.type = type;
        }
    }
    public class Space : Reservable
    {
        private string type;
        public Space(string name, string description, bool reserved, string type)
            : base(name, description, reserved)
        {
            this.type = type;
        }
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
