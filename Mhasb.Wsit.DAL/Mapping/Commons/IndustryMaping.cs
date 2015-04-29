using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Commons
{
    public class IndustryMaping : EntityTypeConfiguration<Industry>
    {
        public IndustryMaping()
        {
            // key 
            this.HasKey(l => l.Id);
            this.Ignore(l => l.State);

            this.Property(l => l.IndustryName).HasMaxLength(100).HasColumnName("industry_name");
            this.ToTable("com.industries");
        }
    }
}
