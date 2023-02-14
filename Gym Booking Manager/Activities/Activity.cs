using System;
using System.Collections.Generic;
using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Calendars;

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

        public Activity(int id, string name, string description, bool open, int limit,
            Staff instructor, Calendar date, Reservation reservation, List<Customer>? participants = default(List<Customer>))
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

                // Adding objects (Customer) from public list (User.users) to new list (participants) based on integers (User.id).
                foreach (string strB in stringsB)
                {
                    participants.Add((Customer)User.users.Find(user => user.id == int.Parse(strB)));
                }

                // Adding new objects (Activity) to public list (Activity.activities) based on values read from file (Activities.txt).
                activities.Add(new Activity(int.Parse(stringsA[0]), stringsA[1], stringsA[2], bool.Parse(stringsA[3]), int.Parse(stringsA[4]),
                    (Staff)User.users.Find(u => u.id == int.Parse(stringsA[5])), new Calendar(DateTime.Parse(stringsA[6]), DateTime.Parse(stringsA[7])), Reservation.reservations.Find(r => r.id == int.Parse(stringsA[8])), participants));
            }
        }

        public void NewActivity()
        {
            // Staff registers new activites.
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
