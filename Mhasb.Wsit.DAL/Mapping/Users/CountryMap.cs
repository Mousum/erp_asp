using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSpace.Domain.Products;

namespace WorkSpace.Dal.Mapping.Products
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            // primary key
            this.HasKey(c => c.Id);

            //Properties
            this.Property(c => c.CountryName)
                .IsRequired()
                .HasMaxLength(100);

            this.Ignore(t => t.State);
            this.Ignore(t => t.EntryBy);
            this.Ignore(t => t.EntryDate);

            // Table & Column Mapping
            this.ToTable("com.countries");
            this.Property(c => c.Id).HasColumnName("pkid");
            this.Property(c => c.CountryName).HasColumnName("country_name");
            this.Property(c => c.CountryCode).HasColumnName("country_code");

            this.Property(c => c.ShortName).HasColumnName("short_name");

        }
    }
}
