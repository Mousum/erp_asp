using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;

namespace Mhasb.DAL.Mapping.OrgSettings
{
   public  class FinalcialSettingMapping:EntityTypeConfiguration<FinalcialSetting>
   {
       public FinalcialSettingMapping()
       {
           // primay key
           this.HasKey(f => f.Id);
           //ignor

           this.Ignore(f => f.State);

           // property
           this.Property(f => f.FoundedYear).HasColumnName("founded_year");

           this.Property(f => f.FinalcialPeriod).HasColumnName("periodid");
           this.Property(f => f.CurrencyId).HasColumnName("currencyid");
           this.Property(f => f.SharesCurrencyId).HasColumnName("shares_currencyid");
           this.Property(f => f.Capital).HasColumnName("capital");
           this.Property(f => f.PaidUpCapital).HasColumnName("paid_up_capital");
           this.Property(f => f.SharePrice).HasColumnName("share_price");
           this.Property(f => f.TotalShares).HasColumnName("total_shares");
           this.Property(f => f.StartingDate).HasColumnName("starting_date");
           this.Property(f => f.EndingDate).HasColumnName("ending_date");
           this.Property(f => f.PeriodLockDate).HasColumnName("period_lock_date");
           this.Property(f => f.YearLockDate).HasColumnName("year_lock_date");
           this.Property(f => f.IsActive).HasColumnName("is_active");

           this.ToTable("set.finalcial_setting");

           // relationship
           this.HasRequired(f => f.Companies)
               .WithMany(f=>f.FinalcialSettings)
               .HasForeignKey(f=>f.CompanyId);

           this.HasRequired(f => f.Currencies)
               .WithMany(f => f.FinalcialSettings)
               .HasForeignKey(f => f.CurrencyId);

           this.HasRequired(f => f.SharesCurrencies)
               .WithMany(f => f.SharesFinalcialSettings)
               .HasForeignKey(f => f.SharesCurrencyId)
               .WillCascadeOnDelete(false);
           }
    }
}
