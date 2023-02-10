using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Calenda;

namespace Gym_Booking_Manager.Reservations
{
    public class Reservation
    {
        public static List<Reservation> reservations = new List<Reservation>();
        public Calendar date { get; set; }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public User owner { get; set; }
        public static List<Reservable> items = new List<Reservable>();

        public Reservation(string name, string description, User owner, Calendar date, List<Reservable> itemss)
        {
            id = 0; // IdCounter():
            this.name = name;
            this.description = description;
            this.owner = owner;
            items = itemss;
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
        public static List<Reservable> reservables = new List<Reservable>();

        int id;
        string name;
        string description;
        List<Reservation> reserved;
        public Reservable(string name, string description, List<Reservation> reserved)
        {
            id = 0; // IdCounter():

            this.name = name;
            this.description = description;
            this.reserved = reserved;
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
        public Equipment(string name, string description, List<Reservation> reserved, string type)
            : base(name, description, reserved)
        {
            this.type = type;
        }
    }
    public class Space : Reservable
    {
        private string type;
        public Space(string name, string description, List<Reservation> reserved, string type)
            : base(name, description, reserved)
        {
            this.type = type;
        }
    }
    public class PTrainer : Reservable
    {
        private Staff instructor;
        public PTrainer(string name, string description, List<Reservation> reserved, Staff instructor)
            : base(name, description, reserved)
        {
            this.instructor = instructor;
        }
    }
}
