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
            this.Property(i => i.SalesDefaultTax).HasColumnName("salesdefaulttax");
            this.Property(i => i.SalesDefaultAccount).HasColumnName("salesdefaultaccount");
            this.Property(i => i.PurchasesDefaultTax).HasColumnName("purchasesdefaulttax");
            this.Property(i => i.PurchasesDefaultAccount).HasColumnName("purchasesdefaultaccount");
            this.Property(i => i.TaxNumber).HasMaxLength(150).HasColumnName("taxnumber");
            this.Property(i => i.SalesTax).HasColumnName("salestax");
            this.Property(i => i.PurchasesTax).HasColumnName("purchasestax");
            this.Property(i => i.DefaultCurrency).HasColumnName("defaultcurrency");
            this.Property(i => i.BankAccountNumber).HasColumnName("bankaccountnumber");
            this.Property(i => i.BankAccountName).HasColumnName("bankaccountname");
            this.Property(i => i.Details).HasMaxLength(100).HasColumnName("details");
            this.Property(i => i.BillsTerms).HasMaxLength(100).HasColumnName("billsterms");
            this.Property(i => i.SalesTerms).HasMaxLength(100).HasColumnName("salesterms");
            this.Property(i => i.NetworkKey).HasMaxLength(100).HasColumnName("networkKey");
            this.ToTable("con.FinancialDetails");

            this.HasRequired(i => i.ContactInformations)
                .WithMany()
                .HasForeignKey(i=>i.ContactInfoId);
            this.HasRequired(i => i.Lookups)
                .WithMany()
                .HasForeignKey(i => i.SalesDefaultTax);
            this.HasRequired(i => i.Lookups)
                .WithMany()
                .HasForeignKey(i => i.PurchasesDefaultTax);
            this.HasRequired(i => i.Lookups)
                .WithMany()
                .HasForeignKey(i => i.PurchasesTax);
            this.HasRequired(i => i.Lookups)
                .WithMany()
                .HasForeignKey(i => i.SalesTax);

            this.HasRequired(i => i.FinancialSettings)
            .WithMany()
            .HasForeignKey(i => i.SalesDefaultAccount);

            this.HasRequired(i => i.FinancialSettings)
            .WithMany()
            .HasForeignKey(i => i.PurchasesDefaultAccount);

            this.HasRequired(i => i.Currencies)
           .WithMany()
           .HasForeignKey(i => i.DefaultCurrency);
        }
    }
}
