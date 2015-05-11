using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Organizations
{
    public class CompanyProfileMapping : EntityTypeConfiguration<CompanyProfile>
    {
        public CompanyProfileMapping()
        {
            this.HasKey(cp=>cp.Id);
            this.Ignore(cp => cp.State);

            this.ToTable("org.company_profiles");
            // property
            this.Property(cp => cp.ImageLocation).HasColumnName("imagelocation").HasMaxLength(200);
            this.Property(cp => cp.BusinessName).HasColumnName("business_name").HasMaxLength(100);
            this.Property(cp => cp.Vision).HasColumnName("vision").HasMaxLength(1000).IsOptional();
            
            this.Property(cp => cp.TurnOver).HasColumnName("turn_over").IsOptional();
            this.Property(cp => cp.Objectives).HasColumnName("objectives").HasMaxLength(1000).IsOptional();

            this.Property(cp => cp.Mission).HasColumnName("mission").HasMaxLength(1000).IsOptional();
            this.Property(cp => cp.Experties).HasColumnName("expertise").HasMaxLength(1000).IsOptional();

            this.Property(cp => cp.Activities).HasColumnName("activities").HasMaxLength(1000).IsOptional();
            this.Property(cp => cp.Markets).HasColumnName("markets").HasMaxLength(1000).IsOptional();
            this.Property(cp => cp.PreviousWork).HasColumnName("previous_work").HasMaxLength(1000).IsOptional();
            this.Property(cp => cp.Address).HasColumnName("address").HasMaxLength(1000).IsOptional();
            this.Property(cp => cp.Location).HasColumnName("location").IsOptional();

            // relationship

            this.HasRequired(cp => cp.Companies)
                .WithOptional(cp => cp.CompanyProfiles)
                .Map(cp => cp.MapKey("companyid"));


        }
    }
}
