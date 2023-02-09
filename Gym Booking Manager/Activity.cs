using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class Activity
    {
        private int id;
        private string name;
        private int participantsLimit;
        private List<User> participants;
        private Reservable reservable;
        private DateTime dateTime;
        public Activity() { }

        public void createActivity()
        {

        }
        public void deleteActivity()
        {

        }
    }
    class GroupActivity : Activity
    {
        private int capacity;
        private Staff instructor;

        public GroupActivity() { }

        public void maxCapacity()
        {

        }
    }

    class Equipment : Activity
    {
        private bool isReserved;

        public Equipment() { }
    }

    class Trainer : Activity
    {
        private string type;
        private Customer customer;
        private Staff trainer;

        public Trainer() { }
    }
}
