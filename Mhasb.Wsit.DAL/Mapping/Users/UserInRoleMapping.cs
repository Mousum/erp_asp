using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Users
{
    public class UserInRoleMapping : EntityTypeConfiguration<UserInRole>
    {
        public UserInRoleMapping()
        {
            this.HasKey(uir => uir.Id);
            this.Ignore(uir => uir.State);
            this.Property(uir => uir.IsActive).HasColumnName("isactive");
            this.Property(uir => uir.RoleId).HasColumnName("roleid");
            this.Property(uir => uir.UserId).HasColumnName("userid");
            this.Property(uir => uir.EmployeeId).HasColumnName("employeeid");

            this.HasRequired(uir => uir.Users).
                WithMany(uir => uir.UserInRoles).
                HasForeignKey(uir => uir.UserId);


            this.HasRequired(uir => uir.Roles).
                WithMany(uir => uir.UserInRoles).
                HasForeignKey(uir=>uir.RoleId);

            this.HasRequired(uir => uir.Companies).
                WithMany(uir => uir.UserInRoles).
                HasForeignKey(uir=>uir.EmployeeId);


            this.ToTable("sec.userinrole");


        }
    }
}
