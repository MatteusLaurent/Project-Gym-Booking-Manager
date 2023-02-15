using System;
using System.Collections.Generic;
using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Calendars;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Gym_Booking_Manager.Activities
{
    public class Activity
    {
        public static int activityID;
        public static List<Activity> activities = new List<Activity>();

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool open { get; set; }
        public int limit { get; set; }
        public Staff instructor { get; set; }
        public Calendar date { get; set; }
        public Reservation reservation { get; set; }
        public List<Customer>? participants { get; set; }

        public Activity(int id, string name, string description, bool open, int limit, Staff instructor, Calendar date, Reservation reservation, List<Customer>? participants = default(List<Customer>))
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.open = open;
            this.limit = limit;
            this.instructor = instructor;
            this.date = date;
            this.reservation = reservation;
            this.participants = participants;
        }
        public static void LoadActivities()
        {
            string[] lines = File.ReadAllLines("Activities/Activities.txt");
            activityID = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++) 
            {
                string[] stringsA = lines[i].Split(";");
                string[] stringsB = stringsA[9].Split(",");

                var participants = new List<Customer>();

                foreach (string strB in stringsB)
                {
                    if(strB.Length>0)participants.Add((Customer)User.users.Find(user => user.id == int.Parse(strB)));
                }

                // Adding new objects (Activity) to public list (Activity.activities) based on values read from file (Activities.txt).
                activities.Add(new Activity(int.Parse(stringsA[0]), stringsA[1], stringsA[2], bool.Parse(stringsA[3]), int.Parse(stringsA[4]),
                    (Staff)User.users.Find(u => u.id == int.Parse(stringsA[5])), new Calendar(DateTime.Parse(stringsA[6]), DateTime.Parse(stringsA[7])), Reservation.reservations.Find(r => r.id == int.Parse(stringsA[8])), participants));
            }
        }
        public static void SaveActivities()
        {
            using (StreamWriter writer = new StreamWriter("Activities/Activities.txt", false))
            {
                writer.WriteLine(activityID);
                foreach (Activity activity in activities)
                {
                    string participants = string.Empty;
                    foreach (Customer customer in activity.participants)
                    {
                        participants += $"{customer.id},";
                    }
                    if(activity.participants.Count()>0)participants = participants[0..^1];
                    writer.WriteLine($"{activity.id};{activity.name};{activity.description};{activity.open};{activity.limit};{activity.instructor.id};{activity.date.timeFrom};" +
                        $"{activity.date.timeTo};{activity.reservation.id};{participants}");
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
                Console.WriteLine("Enter a date and time of start of start of Activity (in the format yyyy-MM-dd HH):");
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
                Console.WriteLine("Enter a date and time of end of Activity (in the format yyyy-MM-dd HH):");
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
        public static void ChooseReservables(int idStaff, DateTime[] date, List<int> reservableToList)
        {         
            while (true)
            {
                Console.WriteLine("Available Equipment/PT: ");
                for (int i = 0; i < reservableToList.Count(); i++)
                {
                    Console.WriteLine($"{i + 1} {Reservable.reservables[reservableToList[i]].name}");
                }
                Console.WriteLine("Type in which Equipment/PT you wish to reserve (number) type 0 to quit: ");
                string input = Console.ReadLine();
                bool isNumber;
                isNumber = int.TryParse(input, out int number);
                if (isNumber && number > 0 && number < reservableToList.Count() + 1)
                {
                    Reservation.reservations[Reservation.reservations.Count() - 1].reservables.Add(Reservable.reservables[reservableToList[number - 1]]);
                    Console.WriteLine("You have booked " + Reservable.reservables[reservableToList[number - 1]].name);
                    Reservation.SaveReservations();
                    reservableToList.RemoveAt(number - 1);
                }
                else if (number == 0) break;
                else
                {
                    Console.WriteLine("Incorrect input!");
                }
            }
            Reservation.SaveReservations();
        }
        public static void NewActivity(int idStaff)
        {
            int id = GetID(); string Name; string Description; bool Open = true; int participantsNo = 0; Staff Instructor = User.users[idStaff] as Staff; List<Customer>? Participants=new List<Customer>();
            bool overlap = false;
            List<int> reservableToList = new List<int>();
            DateTime[] date = new DateTime[2];
            EnterDate(date);
            for (int i = 0; i < Reservable.reservables.Count(); i++)
            {
                overlap = false;
                for (int j = 0; j < Reservation.reservations.Count(); j++)
                {
                    for (int k = 0; k < Reservation.reservations[j].reservables.Count(); k++)
                    {
                        if (i == Reservation.reservations[j].reservables[k].id) overlap = date[0] < Reservation.reservations[j].date.timeTo && Reservation.reservations[j].date.timeFrom < date[1];
                        if (overlap) break;
                    }
                    if (overlap) break;
                }
                if (!overlap && Reservable.reservables[i] is Space) reservableToList.Add(Reservable.reservables[i].id);
            }
            ChooseSpace(idStaff, date, reservableToList);
            reservableToList.Clear();
            for (int i = 0; i < Reservable.reservables.Count(); i++)
            {
                overlap = false;
                for (int j = 0; j < Reservation.reservations.Count(); j++)
                {
                    for (int k = 0; k < Reservation.reservations[j].reservables.Count(); k++)
                    {
                        if (i == Reservation.reservations[j].reservables[k].id) overlap = date[0] < Reservation.reservations[j].date.timeTo && Reservation.reservations[j].date.timeFrom < date[1];
                        if (overlap) break;
                    }
                    if (overlap) break;
                }
                if (!overlap && Reservable.reservables[i] is not Space) reservableToList.Add(Reservable.reservables[i].id);
            }
            ChooseReservables(idStaff, date, reservableToList);
            Console.Write("Enter name of activity: ");
            Name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter description of activity: ");
            Description = Console.ReadLine();
            Console.WriteLine();
            int Limit=0;
            for(int i=0; i<Reservation.reservations[Reservation.reservations.Count() - 1].reservables.Count(); i++)
            {
                if (Reservation.reservations[Reservation.reservations.Count() - 1].reservables[i] is Space)
                {
                    Space room = (Space)Reservation.reservations[Reservation.reservations.Count() - 1].reservables[i];
                    Limit = room.capacity;
                }
            }
            while (true)
            {            
            Console.Write($"Enter participant limit(max={Limit}): ");
            Console.WriteLine();
            string input = Console.ReadLine();
            int.TryParse( input, out participantsNo);
            if(participantsNo > 0 && participantsNo<= Limit)
                {
                    break;
                }
            else Console.WriteLine("Felaktigt input!");
            }
            Reservation Reservationn = Reservation.reservations[Reservation.reservations.Count() - 1];
            Calendar datee = new Calendar(date[0], date[1]); 
            Activity.activities.Add(new Activity(id, Name, Description, Open, participantsNo, Instructor, datee, Reservationn, Participants));
            SaveActivities();
        }
        public static void ChooseSpace(int idStaff, DateTime[] date, List<int> reservableToList)
        {
            List<Reservable> list = new List<Reservable>();
            Console.WriteLine(reservableToList.Count());
            Console.WriteLine("Available Spaces: ");
            for (int i = 0; i < reservableToList.Count(); i++)
            {
                Console.WriteLine($"{i + 1} {Reservable.reservables[reservableToList[i]].name}");
            }

            while (true)
            {
                Console.WriteLine("Type in which space you wish to reserve (number): ");
                string input = Console.ReadLine();
                bool isNumber;
                isNumber = int.TryParse(input, out int number);
                if (isNumber && number > 0 && number < reservableToList.Count() + 1)
                {
                    list.Add(Reservable.reservables[reservableToList[number - 1]]);
                    Console.WriteLine("You have booked " + Reservable.reservables[reservableToList[number - 1]].name);
                    Reservation.reservations.Add(new Reservation(Reservation.GetID(), User.users[idStaff], new Calendar(date[0], date[1]), list));
                    Reservation.SaveReservations();
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect input!");
                }
            }
        }
        public static int GetID()
        {
            int id = activityID;
            activityID++;
            return id;
        }
        public void EditActivity()
        {
            // Staff edits activites.
        }
        public void DeleteActivity()
        {
            // Staff deletes activites.
        }
        public void ActivityRegister()
        {
            // Customers register for activities.
        }
        public void ActivityCancel()
        {
            // Customers cancels registered activities.
        }
        public void ActivityView()
        {
            // Customers views registered activities.
        }
    }
}
