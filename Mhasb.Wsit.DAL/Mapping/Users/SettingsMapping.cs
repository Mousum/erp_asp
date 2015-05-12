using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Users
{
    public class SettingsMapping : EntityTypeConfiguration<Settings>
    {
        public SettingsMapping()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.userId).HasColumnName("user_Id");
            this.Property(c => c.lgcompany).HasColumnName("lg_company");
            this.Property(c => c.lgdash).HasColumnName("lg_dash");
            this.Property(c => c.lglast).HasColumnName("lg_last");
            this.Property(c => c.TimezoneId).HasColumnName("Time_zoneId");

            this.ToTable("sec.settings");
            this.HasRequired(c => c.Users)
                .WithMany()
                .HasForeignKey(c => c.userId);

            this.HasOptional(c => c.AreaTimes)
            .WithMany()
            .HasForeignKey(c => c.TimezoneId);

            this.HasOptional(c => c.Companies)
                .WithOptionalDependent()
                .Map(c=>c.MapKey("companyid"));
        }
    }
}
