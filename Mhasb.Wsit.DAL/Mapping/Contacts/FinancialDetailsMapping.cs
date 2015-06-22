using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Contacts
{
   public class FinancialDetailsMapping : EntityTypeConfiguration<FinancialDetails>
    {
        public FinancialDetailsMapping() {
            this.HasKey(i=>i.Id);
            // ignore 
            this.Ignore(i => i.State);
            // Entities
            this.Property(i=>i.ContactInfoId).HasColumnName("contactinfoid");
            this.Property(i => i.SalesDefaultTax).HasMaxLength(100).HasColumnName("salesdefaulttax");
            this.Property(i => i.SalesDefaultAccount).HasMaxLength(100).HasColumnName("salesdefaultaccount");
            this.Property(i => i.PurchasesDefaultTax).HasMaxLength(100).HasColumnName("purchasesdefaulttax");
            this.Property(i => i.PurchasesDefaultAccount).HasMaxLength(100).HasColumnName("purchasesdefaultaccount");
            this.Property(i => i.TaxNumber).HasMaxLength(100).HasColumnName("taxnumber");
            this.Property(i => i.SalesTax).HasMaxLength(100).HasColumnName("salestax");
            this.Property(i => i.PurchasesTax).HasMaxLength(100).HasColumnName("purchasestax");
            this.Property(i => i.DefaultCurrency).HasMaxLength(100).HasColumnName("defaultcurrency");
            this.Property(i => i.BankAccountNumber).HasMaxLength(100).HasColumnName("bankaccountnumber");
            this.Property(i => i.BankAccountName).HasMaxLength(100).HasColumnName("bankaccountname");
            this.Property(i => i.Details).HasMaxLength(100).HasColumnName("details");
            this.Property(i => i.BillsTerms).HasMaxLength(100).HasColumnName("billsTerms");
            this.Property(i => i.SalesTerms).HasMaxLength(100).HasColumnName("salesterms");
            this.Property(i => i.NetworkKey).HasMaxLength(100).HasColumnName("networkKey");
            this.ToTable("con.FinancialDetails");

            this.HasRequired(i => i.ContactInformations)
                .WithMany()
                .HasForeignKey(i=>i.ContactInfoId);

        }
    }
}
