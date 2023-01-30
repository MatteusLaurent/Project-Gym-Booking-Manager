using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Gym_Booking_Manager
{
    internal interface IReservable
    {
        void MakeReservation(IReservingEntity owner);
        void CancelReservation();
        void ViewTimeTable(); // start and end as arguments?
    }
}
