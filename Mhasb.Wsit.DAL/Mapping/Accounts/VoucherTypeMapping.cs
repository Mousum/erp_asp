using Mhasb.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Accounts
{
    public class VoucherTypeMapping : EntityTypeConfiguration<VoucherType>
    {
        public VoucherTypeMapping()
        {
            this.HasKey(c=>c.Id);
            this.Property(c => c.Name).HasColumnName("name").HasMaxLength(50);
            this.Property(c => c.Code).HasColumnName("code").HasMaxLength(10);
            this.Property(c => c.Description).HasColumnName("description").HasMaxLength(1000);

            this.ToTable("acc.vouchertype");

       }
    }
}
