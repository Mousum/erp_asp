using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;

namespace Mhasb.DAL.Mapping.Accounts
{
  public  class VoucherMapping:EntityTypeConfiguration<Voucher>
  {
      public VoucherMapping()
      {
          // key
          this.HasKey(v => v.Id);
          this.Ignore(v => v.State);

          this.Property(v => v.FinancialSettingId).HasColumnName("financial_settingid");
          this.Property(v => v.VoucherTypeId).HasColumnName("voucher_typeid");
          this.Property(v => v.BranchId).HasColumnName("branchid");
          this.Property(v => v.CurrencyId).HasColumnName("currencyid");
          this.Property(v => v.EmployeeId).HasColumnName("employeeid");


          this.Property(v => v.VoucherDate).HasColumnName("voucher_date");
          this.Property(v => v.VoucherNo).HasColumnName("voucherno").HasMaxLength(50).IsOptional();
          this.Property(v => v.RefferenceNo).HasColumnName("refferenceno").HasMaxLength(100);
          this.Property(v => v.Description).HasMaxLength(500).HasColumnName("description").IsOptional();
          this.Property(v => v.ChequeNo).HasColumnName("chequeno").HasMaxLength(50).IsOptional();
          this.Property(v => v.ChequeDate).HasColumnName("cheque_date").IsOptional();

          this.Property(v => v.BillNo).HasColumnName("billno").HasMaxLength(50).IsOptional();
          this.Property(v => v.BillDate).HasColumnName("bill_date").IsOptional();
          this.Property(v => v.ChallenNo).HasColumnName("challen_no").HasMaxLength(50).IsOptional();
          this.Property(v => v.OrderNo).HasColumnName("orderno").HasMaxLength(50).IsOptional();

          this.Property(v => v.Posted).HasColumnName("posted");
          this.Property(v => v.Disbursed).HasColumnName("disbursed");
          this.Property(v => v.Df).HasColumnName("df");
          this.ToTable("acc.vouchers");
          // Relationship

          this.HasRequired(v => v.VoucherTypes)
              .WithMany(v => v.Vouchers)
              .HasForeignKey(v => v.VoucherTypeId);

          this.HasRequired(v => v.FinancialSettings)
              .WithMany(v => v.Vouchers)
              .HasForeignKey(v => v.FinancialSettingId)
              .WillCascadeOnDelete(false);
          this.HasRequired(v => v.Currencies)
              .WithMany()
              .HasForeignKey(v => v.CurrencyId);

          this.HasRequired(e=>e.Employees)
              .WithMany(e=>e.Vouchers)
              .HasForeignKey(e=>e.EmployeeId)
              .WillCascadeOnDelete(false);




      }
    }
}
