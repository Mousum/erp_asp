using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;

namespace Mhasb.DAL.Mapping.Organizations
{
   public class DesignationMapping:EntityTypeConfiguration<Designation>
   {
       public DesignationMapping()
       {
           //
           this.HasKey(d => d.Id);
           this.Ignore(d => d.State);
           this.Property(d => d.DesignationName).IsRequired().HasColumnName("designation_name").HasMaxLength(100);

           this.ToTable("org.designations");
       }
    }
}
