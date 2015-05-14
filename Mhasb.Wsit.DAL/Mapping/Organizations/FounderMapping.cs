using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Organizations;

namespace Mhasb.DAL.Mapping.Organizations
{
   public class FounderMapping:EntityTypeConfiguration<Founder>
   {
       public FounderMapping()
       {
           // primary key
           this.HasKey(f => f.Id);
           this.Ignore(f => f.State);

           this.Property(f => f.CountryId).HasColumnName("countryid");
           this.Property(f => f.LanguageId).HasColumnName("languageid");
           this.Property(f => f.CompanyId).HasColumnName("companyid");
           this.Property(f => f.FounderName).IsRequired().HasColumnName("name").HasMaxLength(100);
           this.Property(f => f.FounderResidence).HasColumnName("residence").HasMaxLength(200);
           this.Property(f => f.Tel).HasColumnName("tel").HasMaxLength(100);
           this.Property(f => f.Fax).HasColumnName("fax").HasMaxLength(100);
           this.Property(f => f.Email).HasColumnName("email").HasMaxLength(100);
           this.Property(f => f.PoBoax).HasColumnName("pobox").HasMaxLength(100);
           this.Property(f => f.SharesOwned).HasColumnName("shares_owned");
           this.Property(f => f.TotalSharesValue).HasColumnName("total_shares_value");
           this.ToTable("org.founders");

           // relationship
           this.HasRequired(f => f.Countries)
               .WithMany()
               .HasForeignKey(f => f.CountryId)
               .WillCascadeOnDelete(false);
           this.HasRequired(f => f.Companies)
               .WithMany()
               .HasForeignKey(f => f.CompanyId);
           this.HasRequired(f => f.Languages)
               .WithMany()
               .HasForeignKey(f => f.LanguageId)
               .WillCascadeOnDelete(false);
       }
   }
 }

