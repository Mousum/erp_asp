using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Commons
{
    public class AreaTimeMaping : EntityTypeConfiguration<AreaTime>
    {
        public AreaTimeMaping()
       {
           // key 
           this.HasKey(l => l.Id);
           this.Ignore(l => l.State);

           this.Property(l => l.ZoneName).HasMaxLength(100).HasColumnName("zone_name");
           this.ToTable("com.areazone");
       }
    }
}
