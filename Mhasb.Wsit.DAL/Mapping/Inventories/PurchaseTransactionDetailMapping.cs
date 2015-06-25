using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Inventories;

namespace Mhasb.DAL.Mapping.Inventories
{
    public class PurchaseTransactionDetailMapping : EntityTypeConfiguration<PurchaseTransactionDetail>
   {
        public PurchaseTransactionDetailMapping()
        {
            this.HasKey(t => t.Id);
            this.Ignore(t => t.State);

            this.Property(t => t.PurchaseTransactionId).HasColumnName("purchase_transactionid");
            this.Property(t => t.ItemId).HasColumnName("itemid");
            this.Property(t => t.CoaId).HasColumnName("coaid");
            this.Property(t => t.TaxId).HasColumnName("taxid");
            this.Property(t => t.Quantity).HasColumnName("quantity");
            this.Property(t => t.UnitPrice).HasColumnName("unit_price");
            this.Property(t => t.Discount).HasColumnName("discount");
            this.Property(t => t.Description).HasColumnName("description").HasMaxLength(500);

            this.ToTable("inv.purchase_transaction_details");

            this.HasRequired(t => t.Items)
                .WithMany(t => t.PurchaseTransactionDetails)
                .HasForeignKey(t => t.ItemId);

            this.HasRequired(t => t.ChartOfAccounts)
               .WithMany(t=>t.PurchaseTransactionDetails)
               .HasForeignKey(t => t.CoaId);

            this.HasRequired(t => t.PurchaseTransactions)
              .WithMany(t => t.PurchaseTransactionDetails)
              .HasForeignKey(t => t.PurchaseTransactionId);
            this.HasRequired(t => t.Lookups)
             .WithMany()
             .HasForeignKey(t => t.TaxId);
        }
    }
}
