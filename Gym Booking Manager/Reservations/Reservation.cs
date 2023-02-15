using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Calendars;

namespace Gym_Booking_Manager.Reservations
{
    public class Reservation
    {
        public static int getReservationID;
        public static List<Reservation> reservations = new List<Reservation>();

        public int id { get; set; }
        public User owner { get; set; }
        public Calendar date { get; set; }
        public List<Reservable>? reservables { get; set; }

        public Reservation(int id, User owner, Calendar date, List<Reservable>? reservables = default)
        {
            this.id = id;
            this.owner = owner;
            this.reservables = new List<Reservable>();
            this.date = date;
            this.reservables = reservables;
        }
        public static void LoadReservations()
        {
            string[] lines = File.ReadAllLines("Reservations/Reservations.txt");
            getReservationID = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] stringsA = lines[i].Split(";");
                string[] stringsB = stringsA[4].Split(",");

                var reservables = new List<Reservable>();

                // Adding objects(Reservable) from public list(Reservable.reservables) to list(reservables) based on integers(Reservable.id).
                foreach (string strB in stringsB)
                {
                    var reservable = Reservable.reservables.Find(r => r.id == int.Parse(strB));
                    reservables.Add(reservable);
                }

                var owner = User.users.Find(u => u.id == int.Parse(stringsA[1]));
                var date = new Calendar(DateTime.Parse(stringsA[2]), DateTime.Parse(stringsA[3]));
                var reservation = new Reservation(int.Parse(stringsA[0]), owner, date, reservables);
                reservations.Add(reservation);
            }
        }
        public static void SaveReservations()
        {
            using (StreamWriter writer = new StreamWriter("Reservations/Reservations.txt", false))
            {
                writer.WriteLine(getReservationID);
                foreach (Reservation rsv in reservations)
                {
                    string reservables = string.Empty;
                    foreach (Reservable rvb in rsv.reservables)
                    {
                        reservables += $"{rvb.id},";
                    }
                    reservables = reservables[0..^1];
                    writer.WriteLine($"{rsv.id};{rsv.owner.id};{rsv.date.timeFrom};{rsv.date.timeTo};{reservables}");
                }
            }
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
                Console.WriteLine($"{i + 1} {Reservable.reservables[ReservableToList[i]].name}");
            }

            while (true)
            {
                Console.WriteLine("Type in which equiment/PT you wish to reserve(number): ");
                string input = Console.ReadLine();
                bool isNumber;
                isNumber = int.TryParse(input, out int number);
                if (isNumber && number > 0 && number < ReservableToList.Count() + 1)
                {
                    list.Add(Reservable.reservables[ReservableToList[number - 1]]);
                    Console.WriteLine("You have booked " + Reservable.reservables[ReservableToList[number - 1]].name);
                    reservations.Add(new Reservation(GetID(), User.users[userID], new Calendar(date[0], date[1]), list));
                    SaveReservations();
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
            List<int> reservableToList = new List<int>();
            DateTime[] date = new DateTime[2];
            EnterDate(date);
            for (int i = 0; i < Reservable.reservables.Count(); i++)
            {
                overlap = false;
                for (int j = 0; j < reservations.Count(); j++)
                {
                    for (int k = 0; k < reservations[j].reservables.Count(); k++)
                    {
                        if (i == reservations[j].reservables[k].id) overlap = date[0] < reservations[j].date.timeTo && reservations[j].date.timeFrom < date[1];
                        if (overlap) break;
                    }
                    if (overlap) break;
                }
                if (!overlap) reservableToList.Add(Reservable.reservables[i].id);
            }
            ChooseReservation(reservableToList, userID, date);
        }
        public static void NewReservationUserMember(int userID)
        {
            bool overlap = false;
            List<int> ReservableToList = new List<int>();
            DateTime[] date = new DateTime[2];
            EnterDate(date);
            for (int i = 0; i < Reservable.reservables.Count(); i++)
            {
                overlap = false;
                for (int j = 0; j < reservations.Count(); j++)
                {
                    for (int k = 0; k < reservations[j].reservables.Count(); k++)
                    {
                        if (i == reservations[j].reservables[k].id) overlap = date[0] < reservations[j].date.timeTo && reservations[j].date.timeFrom < date[1];
                        if (overlap) break;
                    }
                    if (overlap) break;
                }
                if (!overlap && Reservable.reservables[i] is not Space) ReservableToList.Add(Reservable.reservables[i].id);
            }
            ChooseReservation(ReservableToList, userID, date);
        }
        public static void NewReservationUserNonMember(int userID)
        {
            bool overlap = false;
            List<int> ReservableToList = new List<int>();
            DateTime[] date = new DateTime[2];
            EnterDate(date);
            for (int i = 0; i < Reservable.reservables.Count(); i++)
            {
                overlap = false;
                for (int j = 0; j < reservations.Count(); j++)
                {
                    for (int k = 0; k < reservations[j].reservables.Count(); k++)
                    {
                        if (i == reservations[j].reservables[k].id) overlap = date[0] < reservations[j].date.timeTo && reservations[j].date.timeFrom < date[1];
                        if (overlap) break;
                    }
                    if (overlap) break;
                }
                if (!overlap && Reservable.reservables[i] is PTrainer) ReservableToList.Add(Reservable.reservables[i].id);
                if (!overlap && Reservable.reservables[i] is Equipment)
                {
                    Equipment equipment = (Equipment)Reservable.reservables[i];
                    if(equipment.bookable==true)ReservableToList.Add(Reservable.reservables[i].id);
                }
            }
            ChooseReservation(ReservableToList, userID, date);
        }
        public static int GetID()
        {
            int id = getReservationID;
            getReservationID++;
            return id;
        }
        public void NewReservation()
        {

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
        public static int getReservablesID;
        public static List<Reservable> reservables = new List<Reservable>();

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public static List<Reservation> reservations { get; set; }
        public Reservable(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        public static void LoadReservables()
        {
            string[] lines = File.ReadAllLines("Reservations/Reservables.txt");
            getReservablesID = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] strings = lines[i].Split(";");
                if (strings[0] == "Equipment")
                {
                    var equipment = new Equipment(int.Parse(strings[1]), strings[2], strings[3], bool.Parse(strings[4]));
                    reservables.Add(equipment);
                }
                if (strings[0] == "Space")
                {
                    var space = new Space(int.Parse(strings[1]), strings[2], strings[3], int.Parse(strings[4]));
                    reservables.Add(space);
                }
                if (strings[0] == "PTrainer")
                {
                    Staff staff = (Staff)User.users.Find(u => u.id == int.Parse(strings[2]));
                    var ptrainer = new PTrainer(int.Parse(strings[1]), staff);
                    reservables.Add(ptrainer);
                }
            }
        }
        public static void SaveReservables()
        {
            using (StreamWriter writer = new StreamWriter("Reservations/Reservables.txt", false))
            {
                writer.WriteLine(getReservablesID);
                for (int i = 0; i < reservables.Count; i++)
                {
                    if (reservables[i] is Equipment)
                    {
                        Equipment equipment = (Equipment)reservables[i];
                        writer.WriteLine($"Equipment;{equipment.id};{equipment.name};{equipment.description};{equipment.bookable}");
                    }
                    if (reservables[i] is Space)
                    {
                        Space space = (Space)reservables[i];
                        writer.WriteLine($"Space;{space.id};{space.name};{space.description};{space.capacity}");
                    }
                    if (reservables[i] is PTrainer)
                    {
                        PTrainer ptrainer = (PTrainer)reservables[i];
                        writer.WriteLine($"PTrainer;{ptrainer.id};{ptrainer.name};{ptrainer.description};{ptrainer.instructor.id}");
                    }
                }
            }
        }
        public static void NewReservable()
        {
            bool go = true;
            while (go == true)
            {
                Console.WriteLine("Write 1 to registrer Equipment, 2 to register space, 3 to register PT PT, 4 to Quit");
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
                    default:
                        Console.WriteLine("Incorrect input!");
                        break;
                }
            }
        }
        public static int GetID()
        {
            int id = getReservablesID;
            getReservablesID++;
            return id;
        }
        public static void NewEquipment()
        {
            string[] input = new string[3];
            Console.Write("Write the space name: ");
            input[0] = Console.ReadLine();
            Console.Write("Write the space description: ");
            input[1] = Console.ReadLine();
            Console.Write("Should non-members be able to book this? (yes or no)");
            input[2] = Console.ReadLine();
            if (input[2] == "Yes" || input[2] == "yes" || input[2] == "YES") input[2] = "true";
            else input[2] = "false";
            Console.WriteLine();
            Console.WriteLine("Do you want to save:" + input[0] + " " + input[1] + " write yes if so");
            string spara = Console.ReadLine();
            if (spara == "ja" || spara == "Ja" || spara == "JA")
            {
                int ID = GetID();
                reservables.Add(new Equipment(ID, input[0], input[1], bool.Parse(input[2])));
                SaveReservables();
            }
        }
        public static void NewSpace()
        {
            string[] input = new string[3];
            Console.Write("Write the space name: ");
            input[0] = Console.ReadLine();
            Console.Write("Write the space description: ");
            input[1] = Console.ReadLine();
            while (true)
            {
                Console.Write("Write the space capacity: ");
                input[2] = Console.ReadLine();
                int.TryParse(input[2], out int no);
                if (no > 0) break;
                else Console.WriteLine("Incorrect input!");
            }
            Console.WriteLine();
            Console.WriteLine("Do you want to save:" + input[0] + " " + input[1] + " " + input[2] + " write yes if so");
            string spara = Console.ReadLine();
            if (spara == "yes" || spara == "Yes" || spara == "YES")
            {
                int ID = GetID();
                reservables.Add(new Space(ID, input[0], input[1], int.Parse(input[2])));
                SaveReservables();
            }
        }
        public static void NewPT(Staff staff = null)
        {
            if (staff != null)
            {
                int id = GetID();
                var ptrainer = new PTrainer(id, staff);

                reservables.Add(ptrainer);
                SaveReservables();
            }
            else
            {
                Console.Write("Write ID of PT: ");
                int trainerID = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Do you want to save " + trainerID + "? write yes if so");
                string spara = Console.ReadLine();
                if (spara == "yes" || spara == "Yes" || spara == "YES")
                {
                    Staff ptrainer = (Staff)User.users[trainerID];
                    int ID = GetID();
                    reservables.Add(new PTrainer(ID, ptrainer));
                    SaveReservables();
                }
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
        public Equipment(int id, string name, string description, bool bookable)
            : base(id, name, description)
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
            : base(id, $"{PTrainer.firstName} {PTrainer.lastName}", $"{PTrainer.phone}, {PTrainer.email}")
        {
            this.instructor = PTrainer;
        }
    }
}
