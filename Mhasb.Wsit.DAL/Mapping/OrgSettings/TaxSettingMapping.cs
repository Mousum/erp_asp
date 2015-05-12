using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;

namespace Mhasb.DAL.Mapping.OrgSettings
{
   public class TaxSettingMapping:EntityTypeConfiguration<TaxSetting>
   {
       public TaxSettingMapping()
       {
           // primary key
           this.HasKey(t => t.Id);
           this.Ignore(t => t.State);

           this.Property(t => t.CompanyId)
               .HasColumnName("companyid");
           this.Property(t => t.PeriodId)
               .HasColumnName("periodid");

           this.Property(t => t.TaxBasis)
               .HasColumnName("tax_basis")
               .HasMaxLength(100);
           this.Property(t => t.TaxId)
               .HasColumnName("taxid")
               .IsRequired()
               .HasMaxLength(100);
           this.Property(t => t.DisplayName)
               .HasColumnName("display_name")
               .HasMaxLength(100);

           this.ToTable("set.tax_settings");

           // relationship
           this.HasRequired(t => t.Companies)
               .WithMany()
               .HasForeignKey(t => t.CompanyId);
           this.HasRequired(t => t.FinalcialPeriods)
               .WithMany()
               .HasForeignKey(t => t.PeriodId);
       }
    }
}
