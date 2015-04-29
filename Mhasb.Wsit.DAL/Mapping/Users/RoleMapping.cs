using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Users
{
    public class RoleMapping : EntityTypeConfiguration<Role>
    {
        public RoleMapping()
        {
            //key
            this.HasKey(r => r.Id);
            this.Ignore(r => r.State);

            this.Property(r => r.RoleName).HasMaxLength(100).HasColumnName("role_name");
            this.Property(r => r.Remarks).HasMaxLength(100).HasColumnName("remarks");

            this.ToTable("sec.roles");
        }
    }
}
