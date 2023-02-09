using System;
using System.Collections.Generic;
using Gym_Booking_Manager.Reservations;
using Gym_Booking_Manager.Users;

namespace Gym_Booking_Manager.Functionality
{
    public class Activity
    {
        public List<Activity> activities = new List<Activity>();

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool open { get; set; }
        public PTrainer instructor { get; set; }
        public int participantsLimit { get; set; }
        public List<Customer> participants { get; set; }
        public Reservation reservation { get; set; }
        public Activity(string name, string description, bool open, PTrainer instructor, int participantsLimit, Reservation reservation) 
        { 
            this.name = name;
            this.description = description;
            this.open = open;
            this.instructor = instructor;
            this.participantsLimit = participantsLimit;
            participants = new List<Customer>();
            this.reservation = reservation;

            activities.Add(this);
        }
        public void Load()
        {
            string[] lines = File.ReadAllLines("Activities/Activities.txt");
           // foreach (string line in lines)
            //{
              //  string[] strings = line.Split(";");
               // if (strings[0] == "Staff") activities.Add(new Staff(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7]));
                //if (strings[0] == "Admin") activities.Add(new Admin(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7]));
                //if (strings[0] == "Customer") activities.Add(new Customer(int.Parse(strings[1]), strings[2], int.Parse(strings[3]), strings[4], strings[5], strings[6], strings[7], DateTime.Parse(strings[8]), DateTime.Parse(strings[9]), bool.Parse(strings[10])));
            //}
        }

        public void NewActivity()
        {

        }
        public void EditActivity()
        {

        }
        public void DeleteActivity()
        {

        }
        public void ActivityRegister()
        {

        }
        public void ActivityCancel()
        {

        }
        public void ActivityView()
        {

        }
    }
}
