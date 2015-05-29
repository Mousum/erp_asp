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

           this.Property(l => l.Offset).HasMaxLength(50).HasColumnName("off_set");
           this.Property(l => l.ZoneName).HasMaxLength(50).HasColumnName("summer");
           this.Property(l => l.CountryCode).HasMaxLength(50).HasColumnName("country_code");
           this.Property(l => l.Cicode).HasMaxLength(50).HasColumnName("cicode");
           this.Property(l => l.Cicodesummer).HasMaxLength(50).HasColumnName("cicodesummer");
           this.ToTable("com.areazone");
       }
    }
}
