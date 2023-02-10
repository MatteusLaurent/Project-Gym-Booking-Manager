using System;
using System.Collections.Generic;
using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Users;
using Gym_Booking_Manager.Calendars;

namespace Gym_Booking_Manager.Activities
{
    public class Activity
    {
        public static List<Activity> activities = new List<Activity>();

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool open { get; set; }
        public User instructor { get; set; }
        public int participantsLimit { get; set; }
        public List<Customer> participants { get; set; }
        public Reservation reservation { get; set; }
        public Activity(string name, string description, bool open, User instructor, int participantsLimit, Reservation reservation)
        {
            this.name = name;
            this.description = description;
            this.open = open;
            this.instructor = instructor;
            this.participantsLimit = participantsLimit;
            participants = new List<Customer>();
            this.reservation = reservation;
        }
        public static void Load() // TODO: Add Calender arg to constructor!
        {
            string[] lines = File.ReadAllLines("Activities/Activities.txt");
             foreach (string line in lines)
             {
                string[] strings = line.Split(";");
                activities.Add(new Activity(strings[0], strings[1], bool.Parse(strings[2]), User.users[int.Parse(strings[3])], int.Parse(strings[4]), Reservation.reservations[int.Parse(strings[5])]));
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
