using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;

namespace Mhasb.DAL.Mapping.OrgSettings
{
   public class CurrencyMapping:EntityTypeConfiguration<Currency>
   {
       public CurrencyMapping()
       {
           //primary key
           this.HasKey(c => c.Id);
           this.Ignore(c => c.State);
           this.Property(c => c.Name)
               .HasColumnName("name")
               .HasMaxLength(100)
               .IsRequired();

           this.ToTable("set.currencies");
       }
    }
}
