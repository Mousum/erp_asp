using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Loggers;

namespace Mhasb.DAL.Mapping.Loggers
{
   public class CompanyViewLogMapping:EntityTypeConfiguration<CompanyViewLog>
   {
       public CompanyViewLogMapping()
       {
           this.HasKey(u => u.Id);
           this.Ignore(u => u.State);
           this.Property(u => u.UserId).HasColumnName("userid");
           this.Property(u => u.CompanyId).HasColumnName("companyid");
           this.Property(u => u.IpAddress).HasColumnName("ip_address").HasMaxLength(50).IsOptional();
           this.Property(u => u.LoginTime).HasColumnName("visiting_time");

           this.ToTable("log.company_visitors");
           // relationship
           this.HasRequired(u=>u.Users)
               .WithMany()
               .HasForeignKey(u=>u.UserId)
               .WillCascadeOnDelete(false);
           this.HasOptional(u=>u.Companies)
               .WithMany()
               .HasForeignKey(u=>u.CompanyId)
               .WillCascadeOnDelete(false);
       }
    }
}
