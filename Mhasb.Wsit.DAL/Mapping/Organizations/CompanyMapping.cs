using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mhasb.DAL.Mapping.Organizations
{
    public class CompanyMapping : EntityTypeConfiguration<Company>
    {
        public CompanyMapping()
        {
            // Key
            this.HasKey(c => c.Id);
            // ignor
            this.Ignore(c => c.State);

            this.Property(c => c.TradingName).HasMaxLength(100).HasColumnName("trading_name");
            this.Property(c => c.LegalName).HasMaxLength(100).HasColumnName("trading_name");
            this.Property(c => c.DisplayName).HasMaxLength(100).HasColumnName("display_name");
            this.Property(c => c.Tel).HasMaxLength(100).HasColumnName("tel");


            this.ToTable("com.company");

        }
    }
}
