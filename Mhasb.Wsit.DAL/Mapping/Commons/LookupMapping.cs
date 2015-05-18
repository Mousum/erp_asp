using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Commons
{
    public class LookupMapping:EntityTypeConfiguration<Lookup>
    {
        public LookupMapping(){

           this.HasKey(l => l.LookupId);

           this.Ignore(l => l.State);
           this.Property(l => l.LookupType).HasMaxLength(20).HasColumnName("lookuptype");
           this.Property(l => l.Key).HasMaxLength(20).HasColumnName("key");
           this.Property(l => l.Quantity).HasColumnName("quantity");
           this.Property(l => l.Value).HasMaxLength(200).HasColumnName("value");
           this.Property(l => l.Description).HasMaxLength(1000).HasColumnName("description");
           this.Property(l => l.Order).HasColumnName("description");

           this.ToTable("com.Lookup");


        }
       
    }
}
