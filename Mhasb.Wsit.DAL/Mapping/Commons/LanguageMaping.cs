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

           this.Property(l => l.LanguageName).HasMaxLength(100).HasColumnName("language_name");
           this.Property(l => l.Code).HasMaxLength(50).HasColumnName("code");
           this.Property(l => l.Code1).HasMaxLength(50).HasColumnName("code1");
           this.Property(l => l.Native).HasMaxLength(50).HasColumnName("native");

           this.ToTable("com.languages");
       }
    }
}
