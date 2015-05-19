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
            this.Property(vd => vd.TaxDr).HasColumnName("taxdr");
            this.Property(vd => vd.TaxCr).HasColumnName("taxcr");
            this.Property(c => c.Description).HasColumnName("description").HasMaxLength(500);
            this.ToTable("acc.voucher_details");

            // relationship
            this.HasRequired(vd => vd.Vouchers)
                .WithMany(vd => vd.VoucherDetails)
                .HasForeignKey(vd => vd.VoucherId);
            this.HasRequired(vd => vd.ChartOfAccounts)
                .WithMany(vd => vd.VoucherDetails)
                .HasForeignKey(vd => vd.CoaId);


        }

    }
}
