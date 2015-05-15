using Mhasb.DAL.Mapping.Commons;
using Mhasb.DAL.Mapping.Notifications;
using Mhasb.DAL.Mapping.Organizations;
using Mhasb.DAL.Mapping.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.DAL.Mapping.OrgSettings;
using Mhasb.Domain.OrgSettings;

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
            modelBuilder.Configurations.Add(new CountryMaping());
            modelBuilder.Configurations.Add(new LanguageMaping());
            modelBuilder.Configurations.Add(new LegalEntityMaping());
            modelBuilder.Configurations.Add(new IndustryMaping());
            modelBuilder.Configurations.Add(new AreaTimeMaping());

            // Notification
            modelBuilder.Configurations.Add(new InvitationMapping());

            // User Management
            modelBuilder.Configurations.Add(new UserMaping());
            modelBuilder.Configurations.Add(new RoleMapping());
            modelBuilder.Configurations.Add(new RoleVsActionMapping());
            modelBuilder.Configurations.Add(new ActionListMapping());
            modelBuilder.Configurations.Add(new UserInRoleMapping());
            modelBuilder.Configurations.Add(new SettingsMapping());

            // Company
            modelBuilder.Configurations.Add(new CompanyMapping());
            modelBuilder.Configurations.Add(new EmployeeMapping());
            modelBuilder.Configurations.Add(new CompanyProfileMapping());
            modelBuilder.Configurations.Add(new EmployeeProfileMapping());
            modelBuilder.Configurations.Add(new ContactDetailMapping());
            modelBuilder.Configurations.Add(new CompanyDocumentMapping());
            modelBuilder.Configurations.Add(new FounderMapping());
            modelBuilder.Configurations.Add(new DesignationMapping());
            // Project Task
            modelBuilder.Configurations.Add(new TaskManageMapping());
            modelBuilder.Configurations.Add(new ProjectMapping());

            // Org Settings
            modelBuilder.Configurations.Add(new CurrencyMapping());
            modelBuilder.Configurations.Add(new FinalcialSettingMapping());
            modelBuilder.Configurations.Add(new AuditorMapping());
            modelBuilder.Configurations.Add(new TaxSettingMapping());
            //modelBuilder.Configurations.Add(new FinalcialSettingMapping());

        }

        public System.Data.Entity.DbSet<Mhasb.Domain.OrgSettings.TaxSetting> TaxSettings { get; set; }

        public System.Data.Entity.DbSet<Mhasb.Domain.Organizations.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<Mhasb.Domain.Organizations.Designation> Designations { get; set; }

        public System.Data.Entity.DbSet<Mhasb.Domain.OrgSettings.Auditor> Auditors { get; set; }

        public System.Data.Entity.DbSet<Mhasb.Domain.Organizations.Employee> Employees { get; set; }

    }
}
