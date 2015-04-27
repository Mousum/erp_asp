﻿using Mhasb.DAL.Mapping.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.DAL.Data
{
    class WsDbContext: DbContext, IDbContext
    {
       
        static WsDbContext()
        {
            //Database.SetInitializer<WsDbContext>(null);

        }

        public WsDbContext()
            : base("Name=DbConString")
        {
            Configuration.LazyLoadingEnabled = false;
        }


        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            try
            {
                this.ApplyStateChanges();
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                           validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Commons Entities
            modelBuilder.Configurations.Add(new UserMaping());            
            
        }

        public System.Data.Entity.DbSet<Mhasb.Domain.Users.User> Users { get; set; }

        //public System.Data.Entity.DbSet<Mhasb.Wsit.Web.Areas.UserManagement.Models.Login> Logins { get; set; }
    }
}
