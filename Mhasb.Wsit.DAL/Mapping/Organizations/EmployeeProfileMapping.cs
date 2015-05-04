using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Organizations
{
    public class EmployeeProfileMapping : EntityTypeConfiguration<EmployeeProfile>
    {
        public EmployeeProfileMapping()
        {
            this.HasKey(ep => ep.Id);
            this.Ignore(ep => ep.State);

            // 
            this.Property(ep => ep.Bio).HasMaxLength(1000).HasColumnName("bio");
            this.Property(ep => ep.ImageLocation).HasColumnName("image_location").HasMaxLength(100);
            this.Property(ep => ep.Location).HasColumnName("location");
            this.ToTable("org.employee_profiles");

            this.HasRequired(ep => ep.Users)
                .WithOptional()
                .Map(ep => ep.MapKey("userid"))
                .WillCascadeOnDelete(false);
        }
    }
}
