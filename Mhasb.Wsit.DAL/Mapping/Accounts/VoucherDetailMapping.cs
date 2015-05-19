using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;

namespace Mhasb.DAL.Mapping.Accounts
{
    public class VoucherDetailMapping:EntityTypeConfiguration<VoucherDetail>
    {
        public VoucherDetailMapping()
        {
            // key
            this.HasKey(vd => vd.Id);
            this.Ignore(vd => vd.State);

            this.Property(vd => vd.VoucherId).HasColumnName("voucherid");
            this.Property(vd => vd.CoaId).HasColumnName("coaid");
            this.Property(vd => vd.Debit).HasColumnName("debit");
            this.Property(vd => vd.Credit).HasColumnName("credit");
            this.ToTable("acc.voucher_details");

            // relationship
            this.HasRequired(vd => vd.Vouchers)
                .WithMany(vd => vd.VoucherDetails)
                .HasForeignKey(vd => vd.VoucherId);
        }

    }
}
