using Mhasb.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Subscriptions
{
   public class PackageMapping : EntityTypeConfiguration<Package>
    {
        public PackageMapping() {
            this.HasKey(p => p.Id);

            this.Ignore(p => p.State);
            this.Property(p => p.Name).HasMaxLength(100).HasColumnName("Name");
            this.Property(p => p.Amount).HasColumnName("Amount");
            this.Property(p => p.Duration).HasColumnName("Duration");
            this.Property(p => p.Descriptions).HasColumnName("Description");
            this.Property(p => p.Status).HasColumnName("Status");

            // ignor
            
            this.ToTable("com.packages");
        }

    }
}
