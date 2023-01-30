using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal partial class LocalStorage : IDatabase
    {
        internal class DbSet<T>
        {
            private readonly string dataFile;
            SortedSet<T> entities;

            public DbSet(string dataFile)
            {
                this.dataFile = dataFile;
                this.entities = new SortedSet<T>();
            }

            /* NOTE: There are many things to consider in regards to reinventing database basics with local file i/o management.
             * This section of code is not meant to show best practices.
             * 
             * To consider, what's the scope/lifetime of the "database context"
             * object? For the flat file implementation, should we open and close
             * files at the lowest level? For now, yes.
             * 
             * Naive solution for object management: Create the entity list in its entirity
             * every time we want to perform an operation.
             * 
             * CRUD Methods: Create, Read, Update, Delete */

            public bool create(T newEntity)
            {
                this.pullEntitiesFromFile(this.dataFile);

                if (!this.entities.Add(newEntity))
                {
                    return false; // Object with same values exists, operation failed.
                }

                this.pushEntitiesToFile(this.dataFile);
                return true; // Hopefully it went fine.
            }


            // TODO: Read all or specific?
            public List<T> read(String? field, String? value)
            {
                this.pullEntitiesFromFile(this.dataFile, selector: field, value: value);
                return new List<T>(this.entities);
            }

            // This may seem crude, as it assumes no key attribute.
            // If we had a key attribute (value of key attribute is unique and can trivially identify a row in a table of data)
            // our implementation could look different.
            // At that point though, we're closer to using a proper database implementation.
            public bool update(T newEntity, T oldEntity)
            {
                this.pullEntitiesFromFile(this.dataFile);

                if (this.entities.Contains(oldEntity)) // If we can find oldEntity, "update" to newEntity.
                {
                    this.entities.Remove(oldEntity);
                    this.entities.Add(newEntity);
                }
                else
                {
                    return false; // No match for the old entity. Operation failed.
                }

                this.pushEntitiesToFile(this.dataFile);
                return true; // Hopefully it went fine.
            }


            public bool delete(T entity)
            {
                this.pullEntitiesFromFile(this.dataFile);

                // Try to delete the entity.
                if (!this.entities.Remove(entity))
                {
                    return false; // No match for the entity. Operation failed.
                }

                this.pushEntitiesToFile(this.dataFile);
                return true; // Hopefully it went fine.
            }

            // Reads ALL, then creates a list with objects. If a selector and value is provided, we skip creating objects according to that rule.
            private void pullEntitiesFromFile(string dataFile, String? selector = null, String? value = null)
            {
                this.entities.Clear();

                if (!File.Exists(dataFile))
                {
                    File.Create(dataFile);
                }

                List<KeyValuePair<String, String>> keyValuePairs;
                Dictionary<String, String> dictArgs;
                foreach (String line in File.ReadLines(dataFile, Encoding.UTF8))
                {
                    if (line == String.Empty) { continue; }

                    // Create a dictionary of comma-separated field:value combinations. Construct an entity of type T using this dictionary.
                    keyValuePairs = line.Split(',').
                        Select(field => new KeyValuePair<String, String>(
                            field.Substring(0, field.IndexOf(':')),
                            field.Substring(field.IndexOf(':') + 1))).
                            ToList();
                    dictArgs = new Dictionary<String, String>(keyValuePairs);
                    if (selector is not null) 
                    {
                        if (value is null) // TODO: We kind of do this sanity check at the GymDatabaseContext interface level, don't we? Maybe only keep one.
                        {
                            throw new ArgumentException("Argument mismatch: 'selector' is non-null and 'value' is null.", nameof(value));
                        }
                        if (dictArgs[selector] != value)
                        {
                            continue;
                        }
                    }
                    T entity = (T)Activator.CreateInstance(typeof(T), dictArgs) ?? throw new SystemException("CreateInstance returned null."); // Consider custom or better specific exception.
                    this.entities.Add(entity);
                }
            }

            // Writes ALL
            private void pushEntitiesToFile(string dataFile)
            {
                using (FileStream fs = File.Open(dataFile, FileMode.Truncate, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        foreach (ICSVable entity in this.entities)
                        {
                            writer.WriteLine(entity.CSVify());
                        }
                    }
                }
            }
        }
    }
}
