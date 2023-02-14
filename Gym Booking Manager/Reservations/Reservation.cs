using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Calendars;

namespace Gym_Booking_Manager.Reservations
{
    public class Reservation
    {
        public static int reservationID;
        public static List<Reservation> reservations = new List<Reservation>();

        public int id { get; set; }
        public User owner { get; set; }
        public Calendar date { get; set; }
        public List<Reservable>? reservableList { get; set; }

        public Reservation(int id, User owner, Calendar date, List<Reservable>? reservableList = default)
        {
            this.id = id;
            this.owner = owner;
            this.reservableList = new List<Reservable>();
            this.date = date;
            this.reservableList = reservableList;
        }
        public static void LoadReservations()
        {
            string[] lines = File.ReadAllLines("Reservations/Reservations.txt");
            reservationID = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++) 
            {
                string[] stringsA = lines[i].Split(";");
                string[] stringsB = stringsA[4].Split(",");

                var reservables = new List<Reservable>();

                // Adding objects (Reservable) from public list (Reservable.reservables) to list (reservables) based on integers (Reservable.id).
                foreach (string strB in stringsB)
                {
                    reservables.Add(Reservable.reservables.Find(r => r.id == int.Parse(strB)));
                }

                // Adding new objects (Reservation) to public list (Reservation.reservation) based on values read from file (Reservations.txt).
                reservations.Add(new Reservation(int.Parse(stringsA[0]), User.users.Find(u => u.id == int.Parse(stringsA[1])), new Calendar(DateTime.Parse(stringsA[2]), DateTime.Parse(stringsA[3])), reservables));
            }
        }
        public static void Save()
        {
            string[] lines = File.ReadAllLines("Reservations/Reservations.txt");
            string itemsReserved = "";
            Console.WriteLine(Reservation.reservations[Reservation.reservations.Count() - 1].reservableList.Count());
            for (int i = 0; i < Reservation.reservations[Reservation.reservations.Count() - 1].reservableList.Count(); i++)
            {
                if (Reservation.reservations[Reservation.reservations.Count() - 1].reservableList.Count() - 1 == i) itemsReserved = itemsReserved + Reservation.reservations[Reservation.reservations.Count() - 1].reservableList[i].id;
                else itemsReserved = itemsReserved + Reservation.reservations[Reservation.reservations.Count() - 1].reservableList[i].id + ";";
            }
            using (StreamWriter writer = new StreamWriter("Reservations/Reservations.txt", true))
                writer.WriteLine($"{Reservation.reservations[Reservation.reservations.Count() - 1].id};{User.users[Reservation.reservations.Count() - 1].id};{Reservation.reservations[Reservation.reservations.Count() - 1].date.timeFrom};{Reservation.reservations[Reservation.reservations.Count() - 1].date.timeTo};{itemsReserved}");
        }
        public static void EnterDate(DateTime[] date)
        {
            bool go = true;
            string input;
            rerun:
            while (go == true)
            {
                Console.WriteLine("Enter a date and time of start of lending (in the format yyyy-MM-dd HH):");
                input = Console.ReadLine() + ":00:00";
                if (DateTime.TryParse(input, out date[0]))
                {
                    go = false;
                }
                else
                {
                    Console.WriteLine("Invalid date and time format.");
                }
            }
            go = true;
            while (go == true)
            {
                Console.WriteLine("Enter a date and time of end of lending (in the format yyyy-MM-dd HH):");
                input = Console.ReadLine() + ":00:00";
                if (DateTime.TryParse(input, out date[1]))
                {
                    go = false;
                }
                else
                {
                    Console.WriteLine("Invalid date and time format.");
                }
            }
            if (date[0] >= date[1])
            {
                Console.WriteLine("End date/time is the same or earlier then start date/time, try again.");
                go = true;
                goto rerun;
            }
        }
        public static void ChooseReservation(List<int> ReservableToList, int userID, DateTime[] date)
        {
            List<Reservable> list = new List<Reservable>();
            Console.WriteLine("Available Equipment: ");
            for (int i = 0; i < ReservableToList.Count(); i++)
            {
                Console.WriteLine($"{i + 1} {Reservable.reservables[ReservableToList[i]].Name}");
            }

            while (true)
            {
                Console.WriteLine("Type in which equiment you wish to reserve: ");
                string input = Console.ReadLine();
                bool isNumber;
                isNumber = int.TryParse(input, out int number);
                if (isNumber && number > 0 && number < ReservableToList.Count()+1)
                {
                    list.Add(Reservable.reservables[ReservableToList[number - 1]]);
                    Console.WriteLine("You have booked " + Reservable.reservables[ReservableToList[number - 1]].Name);
                    reservations.Add(new Reservation(reservations.Count(), User.users[userID], new Calendar(date[0], date[1]), list));
                    Save();
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect input!");
                }
            }
        }
        public static void NewReservationStaff(int userID)
        {
            bool overlap = false;
            List<int> ReservableToList= new List<int>();
            DateTime[] date = new DateTime[2];
            EnterDate(date);
            for (int i = 0; i < Reservable.reservables.Count(); i++)
            {
                overlap = false;  
                for (int j = 0; j < reservations.Count(); j++)
                {
                    for (int k = 0; k < reservations[j].reservableList.Count(); k++)
                    {
                        if (i == reservations[j].reservableList[k].id)overlap = date[0] < reservations[j].date.timeTo && reservations[j].date.timeFrom < date[1];
                        if (overlap) break;
                    }
                    if (overlap) break;  
                }
                if (!overlap) ReservableToList.Add(Reservable.reservables[i].id);
            }
            ChooseReservation(ReservableToList, userID, date);
        }
        //public void SaveReservations()
        //{
        //    string[] lines = File.ReadAllLines("Reservations/Reservations.txt");
        //    string itemsReserved = "";
        //    for (int i = 0; i < Reservation.reservations[Reservation.reservations.Count() - 1].reservableList.Count(); i++)
        //    {
        //        if (Reservation.reservations[Reservation.reservations.Count() - 1].reservableList.Count() - 1 == i) itemsReserved = itemsReserved + Reservation.reservations[Reservation.reservations.Count() - 1].reservableList[i].id;
        //        else itemsReserved = itemsReserved + Reservation.reservations[Reservation.reservations.Count() - 1].reservableList[i].id + ";";
        //    }
        //    using (StreamWriter writer = new StreamWriter("Reservations/Reservations.txt", true))
        //        writer.WriteLine($"{Reservation.reservations[Reservation.reservations.Count() - 1].name};{Reservation.reservations[Reservation.reservations.Count() - 1].description};{User.users[Reservation.reservations.Count() - 1].id};{Reservation.reservations[Reservation.reservations.Count() - 1].date.timeFrom};{Reservation.reservations[Reservation.reservations.Count() - 1].date.timeTo}+{itemsReserved}");
        //}
        public void NewReservation()
        {

        }
        public static void UpdateReservation()
        {
            // Update reservations from list.
        }
        public static void DeleteReservation()
        {
            // Delete reservations from list.
        }
    }
    public class Reservable
    {
        public static int reservableID;
        public static List<Reservable> reservables = new List<Reservable>();

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<Reservation> reservations { get; set; }
        public Reservable(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        //public Reservable(int id)
        //{
        //    this.id = id;
        //    this.name = "";
        //    this.description = "";
        //}
        public static void LoadReservables()
        {
            string[] lines = File.ReadAllLines("Reservations/Reservables.txt");
            reservableID = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] strings = lines[i].Split(";");
                if (strings[0] == "Equipment") reservables.Add(new Equipment(int.Parse(strings[1]), strings[2], strings[3]));
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
                if (Reservable.reservables[Reservable.reservables.Count() - 1] is Space)
                {
                    Space space = (Space)Reservable.reservables[Reservable.reservables.Count() - 1];
                    writer.WriteLine($"Space;{space.id};{space.name};{space.description};{space.Capacity}");
                }
                if (Reservable.reservables[Reservable.reservables.Count() - 1] is Equipment)
                {
                    Equipment space = (Equipment)Reservable.reservables[Reservable.reservables.Count() - 1];
                    writer.WriteLine($"Space;{space.id};{space.name};{space.description};{space.Bookable}");
                }
            }
        }
        //public static void SaveReservables()
        //{
        //    string[] lines = File.ReadAllLines("Reservations/Reservables.txt");
        //    using (StreamWriter writer = new StreamWriter("Reservations/Reservables.txt", true))
        //    {
        //        if (Reservable.reservables[Reservable.reservables.Count() - 1] is PTrainer)
        //        {
        //            PTrainer pTrainer = (PTrainer)Reservable.reservables[Reservable.reservables.Count() - 1];
        //            writer.WriteLine($"PTrainer;{pTrainer.id};{pTrainer.name};{pTrainer.description};{pTrainer.Instructor.id}");
        //        }
        //        if (Reservable.reservables[Reservable.reservables.Count() - 1] is Equipment) writer.WriteLine($"Equipment;{Reservable.reservables[Reservable.reservables.Count() - 1].id};{Reservable.reservables[Reservable.reservables.Count() - 1].name};{Reservable.reservables[Reservable.reservables.Count() - 1].description}");
        //        if (Reservable.reservables[Reservable.reservables.Count() - 1] is Space)
        //        {
        //            Space space = (Space)Reservable.reservables[Reservable.reservables.Count() - 1];
        //            writer.WriteLine($"Space;{space.id};{space.name};{space.description};{space.Capacity}");
        //        }
        //    }
        //}
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
            string[] input = new string[3];
            Console.Write("Skriv in utrustningens namn: ");
            input[0] = Console.ReadLine();
            Console.Write("Skriv in utrustningens beskrivning: ");
            input[1] = Console.ReadLine();
            Console.Write("Ska kund kunna boka denna utrustning? ");
            input[1] = Console.ReadLine();
            int ID = GetID();             
            Console.WriteLine(); 
            Console.WriteLine("Vill du spara namn:" + input[0]+" "+ input[1]+" skriv ja om du vill det");
            string spara=Console.ReadLine();
            if (spara == "ja" || spara == "Ja" || spara == "JA")
            {
                reservables.Add(new Equipment(ID, input[0], input[1], bool.Parse(input[2])));
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
                //SaveReservables();
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
                //reservables.Add(new PTrainer(ID, trainerID));
                //SaveReservables();
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
        public bool bookable { get; set; }
        public Equipment(int id,string name, string description, bool bookable)
            : base(id,name, description) 
        { 
            this.bookable = bookable; 
        }
    }
    public class Space : Reservable
    {
        public int capacity { get; set; }
        public Space(int id, string name, string description, int capacity)
            : base(id, name, description) 
        { 
            this.capacity = capacity; 
        }
    }
    public class PTrainer : Reservable
    {
        public Staff instructor { get; set; }
        public PTrainer(int id, Staff PTrainer)
            : base(id, PTrainer.name, $"{PTrainer.phone}, {PTrainer.email}")
        {
            this.instructor = PTrainer;
        }
    }
}
