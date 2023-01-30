// Placeholder name for file until we get a more complete grasp of classes in the system
// and the organisation thereof.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class Calendar
    {
        private readonly List<Reservation> reservations;

        public Calendar()
        {
            this.reservations = new List<Reservation>();
        }

        // Leaving this method for now. Idea being it may be useful to get entries within a "start" and "end" time/date range.
        // Need parameters if so.
        // Or maybe we'll come up with a better solution elsewhere.
        public List<Reservation> GetSlice()
        {
            return this.reservations; // Promise to implement or delete this later, please just compile.
        }
    }
}