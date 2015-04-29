using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Commons
{
    public class LegalEntityMaping : EntityTypeConfiguration<LegalEntity>
    {
        public LegalEntityMaping()
        {
            // key 
            this.HasKey(l => l.Id);
            this.Ignore(l => l.State);

            this.Property(l => l.LegalEntityName).HasMaxLength(100).HasColumnName("legal_entity_name");
            this.ToTable("com.legal_entities");
        }
    }
}
