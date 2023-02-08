using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<GroupActivity> activities;
    }
    public void ViewSchedule(User user)
    {
        if (user.perm == 0)
        {
            Console.WriteLine("You are not allowed to view the schedule");
        }
        else
        {
            Console.WriteLine("Schedule:");
            foreach (GroupActivity activity in activities)
            {
                Console.WriteLine(activity);
            }
        }
    }
    public void AddActivity(User user, GroupActivity activity)
    {
        if (user.perm == 0)
        {
            Console.WriteLine("You are not allowed to add activities");
        }
        else
        {
            activities.Add(activity);
        }
    }
    public void RemoveActivity(User user, GroupActivity activityID)
    {
        if (user.perm == 0)
        {
            Console.WriteLine("You are not allowed to remove activities");
        }
        else
        {
            activities.Remove(activityID);
        }
    }
    public void UpdateActivity(User user, GroupActivity activityID, GroupActivity activity)
    {
        if (user.perm == 0)
        {
            Console.WriteLine("You are not allowed to update activities");
        }
        else
        {
            //ToDO
        }
    }
}

