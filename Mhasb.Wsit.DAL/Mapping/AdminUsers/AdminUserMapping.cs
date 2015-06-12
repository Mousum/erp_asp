using Mhasb.Domain.AdminUsers;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.AdminUsers
{
    public class AdminUserMapping : EntityTypeConfiguration<AdminUser>
    {
        public AdminUserMapping()
        {
            //Key
            this.HasKey(au => au.Id);
            
            //Ignore
            this.Ignore(au => au.State);

            //properties

            this.Property(au => au.FirstName).HasColumnName("fname");
            this.Property(au => au.LastName).HasColumnName("lname");
            this.Property(au => au.Email).HasColumnName("email");
            this.Property(au => au.Password).HasColumnName("password");
            this.Property(au => au.type).HasColumnName("type");
            this.Property(au => au.PhoneNo).HasColumnName("phoneno");
            this.ToTable("sys.adminuser");

        }

    }
}
