using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class GymDatabaseContext
    {
        private IDatabase dbImplementation = new LocalStorage();

        public bool Create<T>(T entity)
        {
            return dbImplementation.Create<T>(entity);
        }

        // Field and Value arguments can be provided for a simple "selector". Otherwise, we read the entire table of entities in whichever respective file for the type.
        public List<T> Read<T>(String? field = null, String? value = null)
        {
            if ((field is null && value is not null) ||
                (field is not null && value is null))
            {
                throw new ArgumentException("Database.Read(): Only both or neither of Field and Value arguments must be provided.");
            }
            return dbImplementation.Read<T>(field, value);
        }
        public bool Update<T>(T newEntity, T oldEntity)
        {
            return dbImplementation.Update<T>(newEntity, oldEntity);
        }
        public bool Delete<T>(T entity)
        {
            return dbImplementation.Delete<T>(entity);
        }
    }
}
