﻿using Mhasb.Domain.Contacts;
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
            this.Property(t => t.SalesDefaultTax).HasColumnName("sales_default_tax");
            this.Property(t => t.SalesCoaId).HasColumnName("sales_coaid");

            this.Property(t => t.PurchasesDefaultTax).HasColumnName("purchases_default_tax");
            this.Property(t => t.PurchasesCoaId).HasColumnName("purchases_coaid");


            this.Property(i => i.TaxNumber).HasMaxLength(150).HasColumnName("taxnumber");
            this.Property(i => i.SalesTax).HasColumnName("salestax");
            this.Property(i => i.PurchasesTax).HasColumnName("purchasestax");

            this.Property(i => i.CurrencyId).HasColumnName("currencyid");
            this.Property(i => i.BankAccountNumber).HasMaxLength(150).HasColumnName("bankaccountnumber");
            this.Property(i => i.BankAccountName).HasMaxLength(150).HasColumnName("bankaccountname");
            this.Property(i => i.Details).HasMaxLength(100).HasColumnName("details");
            this.Property(i => i.BillsTerms).HasColumnName("billsterms");
            this.Property(i => i.SalesTerms).HasColumnName("salesterms");
            this.Property(i => i.BillsDue).HasColumnName("billsdue");
            this.Property(i => i.SakesDue).HasColumnName("salesdue");
            this.Property(i => i.NetworkKey).HasMaxLength(100).HasColumnName("networkKey");
            this.ToTable("con.financial_details");

            this.HasRequired(i => i.ContactInformations)
                .WithMany()
                .HasForeignKey(i=>i.ContactInfoId);
            this.HasRequired(i => i.SaleLookups)
                .WithMany()
                .HasForeignKey(i => i.SalesTax)
                .WillCascadeOnDelete(false);

            this.HasRequired(i => i.PurchaseLookups)
                .WithMany()
                .HasForeignKey(i => i.PurchasesTax)
                .WillCascadeOnDelete(false);

            this.HasRequired(i => i.PurchasesAccounts)
                .WithMany()
                .HasForeignKey(i => i.PurchasesCoaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(i => i.SalesAccounts)
                .WithMany()
                .HasForeignKey(i => i.SalesCoaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(i => i.Currencies)
               .WithMany()
               .HasForeignKey(i => i.CurrencyId);
        }
    }
}
