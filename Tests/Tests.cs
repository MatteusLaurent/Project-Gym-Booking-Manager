using Gym_Booking_Manager;
using static Gym_Booking_Manager.Space;
using System;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GroupActivityAdded()
        {
            GymDatabaseContext db = new GymDatabaseContext();
            GroupSchedule schedule = new GroupSchedule(); // Do we need constructor arguments? Maybe GroupSchedule is static - meaning we don't need to call 'new'.

            // // Some examples - or maybe even overloading of AddActivity()?

            //GroupActivity activity = new GroupActivity(); // Args
            //schedule.AddActivity(activity);

            // // struct ActivityDetails maybe? Letting the method generate the object to insert in its internal list
            //ActivityDetails details = new ActivityDetails() // Args
            //schedule.AddActivity(details);

            // // All the arguments?
            // 
            //schedule.AddActivity(participantLimit = ??, timeSlot = ??, instructor = ??, space = ??, ...)

            // Persistance of the activity in the database should happen at some point as part class methods,
            // and not as a db call in this scope. We call db.Read() as part of the test.

            List<GroupActivity> persistedActivities = db.activities.Read<GroupActivity>(); // Make changes if necessary

            Assert.IsTrue(persistedActivities.Exists(x => ??));
            // Replace ?? with a reasonable comparison.
            // Example: if ?? is replaced with 'x == activity':
            //     Assert.IsTrue(persistedActivities.Exists(x => x == activity));
            // or 'x == new GroupActivity(args)':
            //     Assert.IsTrue(persistedActivities.Exists(x => x == new GroupActivity(args)));
            // then the GroupActivity may need to implement IEquatable<GroupActivity> for that comparison.
            //
            // If you prefer, you can also change the Assert statement to something else.
        }
    }
}
