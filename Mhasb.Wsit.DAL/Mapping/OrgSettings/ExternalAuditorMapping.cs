using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;

namespace Mhasb.DAL.Mapping.OrgSettings
{
   public class ExternalAuditorMapping : EntityTypeConfiguration<ExternalAuditor>
    {
       public ExternalAuditorMapping()
       {
           // primary key
           this.HasKey(a => a.Id);
           this.Ignore(a => a.State);

           this.Property(a => a.AuditorName).HasColumnName("auditor_name").HasMaxLength(100);
           this.Property(a => a.AuditorTel).HasColumnName("tel").HasMaxLength(100);
           this.Property(a => a.AuditorEmail).HasColumnName("email").HasMaxLength(100);
           this.Property(a => a.Position).HasColumnName("position").HasMaxLength(100);
           this.Property(a => a.ManagerId).HasColumnName("managerid");
           this.ToTable("set.external_auditor");

           //relationship
           this.HasRequired(a => a.Employees)
               .WithMany()
               .HasForeignKey(a => a.ManagerId);

       }
    }
}
