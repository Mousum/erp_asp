using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.DAL.Data
{
    public static class DbContextExtension
    {
        public static void ApplyStateChanges(this DbContext dbContext)
        {
            foreach (var dbEntityEntry in dbContext.ChangeTracker.Entries())
            {
                var entityState = dbEntityEntry.Entity as IObjectState;
                if (entityState == null)
                    throw new InvalidCastException(
                        "All entites must implement " +
                        "the IObjectState interface, this interface " +
                        "must be implemented so each entites state" +
                        "can explicitely determined when updating graphs.");

                dbEntityEntry.State = ConvertState(entityState.State);
            }
        }

        private static System.Data.Entity.EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return System.Data.Entity.EntityState.Added;
                case ObjectState.Modified:
                    return System.Data.Entity.EntityState.Modified;
                case ObjectState.Deleted:
                    return System.Data.Entity.EntityState.Deleted;
                default:
                    return System.Data.Entity.EntityState.Unchanged;
            }
        }
    }
}
