using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;

namespace Mhasb.DAL.Mapping.OrgSettings
{
   public class AuditorMapping : EntityTypeConfiguration<Auditor>
    {
       public AuditorMapping()
       {
           // primary key
           this.HasKey(a => a.Id);
           this.Ignore(a => a.State);
           this.Property(a => a.AuditorType).HasColumnName("auditor_type");
           this.Property(a => a.AuditorName).HasColumnName("auditor_name").HasMaxLength(100);
           this.Property(a => a.AuditorTel).HasColumnName("tel").HasMaxLength(100);
           this.Property(a => a.AuditorEmail).HasColumnName("email").HasMaxLength(100);
           this.Property(a => a.Position).HasColumnName("position");

           this.Property(a => a.CompanyId).HasColumnName("companyid");
           this.Property(a => a.ManagerId).HasColumnName("managerid");
           this.ToTable("set.auditors");

           //relationship
           this.HasRequired(a => a.Employees)
               .WithMany()
               .HasForeignKey(a => a.ManagerId)
                .WillCascadeOnDelete(false);
           this.HasRequired(a => a.Companies)
               .WithMany()
               .HasForeignKey(a => a.CompanyId);
       }
    }
}
