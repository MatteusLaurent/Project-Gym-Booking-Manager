using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{

    internal class GroupSchedule
    {
    }
    internal class GroupActivity
    {
        private string acticvtyID { get; set; }
        private int participantLimit { get; set; }
        private DateTime timeSlot { get; set; }
        private Instructor instructor { get; set; }
        private Space space { get; set; }
        private Equipment equpiment { get; set; }
        private List<string> participants { get; set; }

        public GroupActivity(string acticvtyID, int participantLimit, DateTime timeSlot, Instructor instructor, Space space, Equipment equpiment, List<string> participants)
        {
            this.acticvtyID = acticvtyID;
            this.participantLimit = participantLimit;
            this.timeSlot = timeSlot;
            this.instructor = instructor;
            this.space = space;
            this.equpiment = equpiment;
            this.participants = participants;
        }

        public void SignUp(string participant)
        {
            participants.Add(participant);
        }
        public void Modify()
        {
            string command;
            Console.WriteLine("What do you want to modify? : ");
            command = Console.ReadLine();
            while (command != "quit")
            {
                if (command == "activity")
                {
                    Console.Write("Change activity ID to: ");
                    acticvtyID = Console.ReadLine();
                }
                //TODO continue this loop to modify the rest of class
            }
        }


    }
    internal class Equipment
    {
        private string equipmentName { get; set; }
        private int quantity { get; set; }
        //private string euipmentSize { get; set; } Do we need??

        public Equipment(string equipmentName, int quantity)
        {
            this.equipmentName = equipmentName;
            this.quantity = quantity;
            //this.euipmentSize = euipmentSize;
        }
    }
    internal class Instructor
    {
        private string instructorName { get; set; }
        public Instructor(string instructorName)
        {
            this.instructorName = instructorName;
        }   
    }
}

