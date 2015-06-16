using Mhasb.Domain.AdminUsers;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.AdminUsers
{
    public class AdminUserLogMapping : EntityTypeConfiguration<AdminUserLog>
    {
        public AdminUserLogMapping() {
            //key
            this.HasKey(l => l.Id);
            //Ignore
            this.Ignore(l=>l.State);

            //Properties

            this.Property(l => l.AdminId).HasColumnName("adminuser");
            this.Property(l => l.ActionId).HasColumnName("actionid");
            this.Property(l=>l.Ip).HasColumnName("ip");
            this.Property(l => l.ActionTime).HasColumnName("actionname");
            this.ToTable("app.adminuserlog");
          //Relationships
            this.HasRequired(l => l.AdminUser)
                .WithMany()
                .HasForeignKey(l => l.AdminId);
            this.HasRequired(l => l.ActionList)
                .WithMany()
                .HasForeignKey(l => l.ActionId);
        }
    }
}
