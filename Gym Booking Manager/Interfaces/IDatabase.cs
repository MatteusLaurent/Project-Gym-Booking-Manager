using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal interface IDatabase
    {
        bool Create<T>(T entity);
        List<T> Read<T>(String? field, String? value);
        bool Update<T>(T newEntity, T oldEntity);
        bool Delete<T>(T oldEntity);
    }
}
