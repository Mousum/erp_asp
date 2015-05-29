using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Commons
{
    public class CountryMaping : EntityTypeConfiguration<Country>
    {
        public CountryMaping() {
            // key 
            this.HasKey(l => l.Id);
            this.Ignore(l => l.State);

            this.Property(l => l.CountryName).HasMaxLength(100).HasColumnName("country_name");
            this.Property(l => l.CountryCode).HasMaxLength(100).HasColumnName("country_code");
            this.Property(l => l.ShortName).HasMaxLength(100).HasColumnName("short_name");
            this.Property(l => l.ShortName1).HasMaxLength(100).HasColumnName("short_name1");

            this.ToTable("com.countries");

        }
    }
}
