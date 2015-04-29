using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Users
{
    public class RoleVsActionMapping : EntityTypeConfiguration<RoleVsAction>
    {
        public RoleVsActionMapping() {
            this.HasKey(ra=>ra.Id);
            this.Ignore(ra=>ra.State);

            this.Property(ra => ra.ActionId).HasColumnName("actionid");
            this.Property(ra => ra.RoleId).HasColumnName("roleid");
            this.Property(ra => ra.IsActive).HasColumnName("isactive");

            this.ToTable("sec.rolevsaction");

            this.HasRequired(ra => ra.Roles)
                .WithMany(ra => ra.RoleVsActions)
                .HasForeignKey(ra => ra.RoleId);

            this.HasRequired(ra => ra.ActionLists)
                .WithMany(ra => ra.RoleVsActions)
                .HasForeignKey(ra => ra.ActionId);

        }
    }
}
