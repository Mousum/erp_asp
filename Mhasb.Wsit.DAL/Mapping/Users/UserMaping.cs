using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Users
{
    public class UserMaping : EntityTypeConfiguration<User>
    {
        public UserMaping()
        {
            // Key
            this.HasKey(u => u.Id);
            // ignor
            this.Ignore(u => u.State);
            this.Ignore(u => u.ConfirmPassword);
            
            this.Property(u => u.FirstName).HasMaxLength(100).HasColumnName("first_name");
            this.Property(u => u.LastName).HasMaxLength(100).HasColumnName("last_name");
            this.Property(u => u.Email).HasMaxLength(50).HasColumnName("email");
            this.Property(u => u.Password).HasMaxLength(50).HasColumnName("password");
            this.Property(u => u.Status).HasMaxLength(50).HasColumnName("status");

            this.ToTable("sec.users");

        }
    }
}
