using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Commons
{
   public class LanguageMaping : EntityTypeConfiguration<Language>
    {
       public LanguageMaping()
       {
           // key 
           this.HasKey(l => l.Id);
           this.Ignore(l => l.State);

           this.Property(l => l.Name).HasMaxLength(100).HasColumnName("language_name");
           this.ToTable("languages");
       }
    }
}
