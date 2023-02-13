using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Calendars;
using System.Xml.Linq;

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

        public Reservation(int id, string name, string description, User owner, Calendar date, List<Reservable> reservableList)
        {
            this.id = id; // IdCounter():
            this.name = name;
            this.description = description;
            this.owner = owner;
            this.reservableList = new List<Reservable>();
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
                reservations.Add(new Reservation(int.Parse(strings[0]),strings[1], strings[2], User.users[int.Parse(strings[3])], new Calendar(DateTime.Parse(strings[4]), DateTime.Parse(strings[5])), reservables));
            }
        }
        public void Save()
        {

            string[] lines = File.ReadAllLines("Reservations/Reservations.txt");
            string itemsReserved="";
            for(int i=0; i< Reservation.reservations[Reservation.reservations.Count() - 1].reservableList.Count(); i++)
            {
                if(Reservation.reservations[Reservation.reservations.Count() - 1].reservableList.Count()-1==i) itemsReserved=itemsReserved+ Reservation.reservations[Reservation.reservations.Count() - 1].reservableList[i].id;
                else itemsReserved = itemsReserved+ Reservation.reservations[Reservation.reservations.Count() - 1].reservableList[i].id + ";";
            }
            using (StreamWriter writer = new StreamWriter("Reservations/Reservations.txt", true))        
            writer.WriteLine($"{Reservation.reservations[Reservation.reservations.Count() - 1].name};{Reservation.reservations[Reservation.reservations.Count() - 1].description};{User.users[Reservation.reservations.Count() - 1].id};{Reservation.reservations[Reservation.reservations.Count() - 1].date.timeFrom};{Reservation.reservations[Reservation.reservations.Count() - 1].date.timeTo}+{itemsReserved}");
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

        public int id { get; set; }
        string name { get; set; }
        string description { get; set; }
        List<Reservation> reservations { get; set; }
        public Reservable(int id,string name, string description)
        {
            this.id = id; // IdCounter():
            this.name = name;
            this.description = description;
            reservations = new List<Reservation>();
        }
        public Reservable(int id)
        {
            this.id = id;
            this.name = "";
            this.description = "";
        }
        public static void Load()
        {
            string[] lines = File.ReadAllLines("Reservations/Reservables.txt");
            foreach (string line in lines)
            {
                string[] strings = line.Split(";");
                if (strings[0]=="Equipment")reservables.Add(new Equipment(int.Parse(strings[1]), strings[2], strings[3]));
                if (strings[0] == "Space") reservables.Add(new Space(int.Parse(strings[1]), strings[2], strings[3], int.Parse(strings[4])));
                if (strings[0] == "PTrainer") reservables.Add(new PTrainer(int.Parse(strings[1]), int.Parse(strings[2])));
            }
        }
        public static void Save()
        {
            string[] lines = File.ReadAllLines("Reservations/Reservables.txt");
            using (StreamWriter writer = new StreamWriter("Reservations/Reservables.txt", true))
            {
                if (Reservable.reservables[Reservable.reservables.Count() - 1] is PTrainer)
                {
                    PTrainer pTrainer = (PTrainer)Reservable.reservables[Reservable.reservables.Count() - 1];
                    writer.WriteLine($"PTrainer;{pTrainer.id};{pTrainer.name};{pTrainer.description};{pTrainer.Instructor.id}");
                }
                if (Reservable.reservables[Reservable.reservables.Count() - 1] is Equipment) writer.WriteLine($"Equipment;{Reservable.reservables[Reservable.reservables.Count() - 1].id};{Reservable.reservables[Reservable.reservables.Count() - 1].name};{Reservable.reservables[Reservable.reservables.Count() - 1].description}");
                if (Reservable.reservables[Reservable.reservables.Count() - 1] is Space)
                {
                    Space space = (Space)Reservable.reservables[Reservable.reservables.Count() - 1];
                    writer.WriteLine($"Space;{space.id};{space.name};{space.description};{space.Capacity}");
                }
            }
        }
        public static void NewReservable()
        {
            bool go = true;
            while (go == true)
            {
                Console.WriteLine("Skriv 1 för att registrera utrustning, 2 för att registrera utrymme, 3 för att registrera PT, 4 Avsluta");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        NewEquipment();
                        break;
                    case "2":
                        NewSpace();
                        break;
                    case "3":
                        NewPT();
                        break;
                    case "4":
                        go = false;
                        break;
                }
            }
        }
        public static int GetID()
        {
            return reservables.Count();
        }
        public static void NewEquipment()
        {
            string[] input=new string[3];
            Console.Write("Skriv in utrustningens namn: ");
            input[0]=Console.ReadLine();
            Console.Write("Skriv in utrustningens beskrivning: ");
            input[1] = Console.ReadLine();
            int ID = GetID();             
            Console.WriteLine(); 
            Console.WriteLine("Vill du spara namn:" + input[0]+" "+ input[1]+" skriv ja om du vill det");
            string spara=Console.ReadLine();
            if (spara == "ja" || spara == "Ja" || spara == "JA")
            {
                reservables.Add(new Equipment(ID, input[0], input[1]));
                Save();
            }
        }
        public static void NewSpace()
        {
            string[] input = new string[3];
            Console.Write("Skriv in lokalens namn: ");
            input[0] = Console.ReadLine();
            Console.Write("Skriv in lokalens beskrivning: ");
            input[1] = Console.ReadLine();
            Console.Write("Skriv in lokalens kapacitet: ");
            input[2] = Console.ReadLine();
            int ID = GetID();
            Console.WriteLine();
            Console.WriteLine("Vill du spara namn:" + input[0] + " " + input[1] + " " + input[2] + "? skriv ja om du vill det");
            string spara = Console.ReadLine();
            if (spara == "ja" || spara == "Ja" || spara == "JA")
            {
                reservables.Add(new Space(ID, input[0], input[1], int.Parse(input[2])));
                Save();
            }
        }

        public static void NewPT()
        {
            Console.Write("Skriv in ID av PT'ens ID: ");
            int trainerID = int.Parse(Console.ReadLine());
            int ID = GetID();
            Console.WriteLine();
            Console.WriteLine("Vill du spara en PT session med trainer ID " + trainerID + "? skriv ja om du vill det");
            string spara = Console.ReadLine();
            if (spara == "ja" || spara == "Ja" || spara == "JA")
            {
                reservables.Add(new PTrainer(ID, trainerID));
                Save();
            }
        }
        public static void UpdateReservable()
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
        public Equipment(int id,string name, string description)
            : base(id,name, description) { }
    }
    public class Space : Reservable
    {
        int capacity { get; set; }
        public int Capacity { get { return capacity; } }
        public Space(int id,string name, string description, int capacity)
            : base(id, name, description) { this.capacity = capacity; }
    }
    public class PTrainer : Reservable
    {
        private User instructor;
        public User Instructor { get { return instructor; } }
        public PTrainer(int id, int who)
            : base(id)
        {
            this.instructor = User.users[who];
        }
    }
}
