using System.Data.Entity.ModelConfiguration;
using Mhasb.Domain.Inventories;

namespace Mhasb.DAL.Mapping.Inventories
{
    public class ItemMapping : EntityTypeConfiguration<Item>
    {
        public ItemMapping()
        {
            this.HasKey(i => i.Id);
            this.Ignore(i => i.State);

            this.Property(i => i.ItemName).HasColumnName("item_name").HasMaxLength(200);
            this.Property(i => i.ItemCode).HasColumnName("item_code").HasMaxLength(50);
            this.Property(i => i.AssetAccountId).HasColumnName("asset_accountid");

            this.Property(i => i.Quantity).HasColumnName("quantity");

            this.Property(i => i.PurchaseUnitPrice).HasColumnName("purchase_unit_price");
            this.Property(i => i.PurchasesAccountId).HasColumnName("purchase_accountid");
            this.Property(i => i.PTaxRateId).HasColumnName("purchase_tax_rateid");
            this.Property(i => i.PurchaseDescription).HasColumnName("purchase_description").HasMaxLength(500);

            this.Property(i => i.SellUnitPrice).HasColumnName("sell_unit_price");
            this.Property(i => i.SalesAccountId).HasColumnName("sales_accountid");
            this.Property(i => i.STaxRateId).HasColumnName("sales_tax_rateid");
            this.Property(i => i.SalesDescription).HasColumnName("Sales_description").HasMaxLength(500);
            this.Property(i => i.CompanyId).HasColumnName("company_id");


            this.ToTable("inv.items");

            this.HasOptional(i => i.AssetAccount)
                .WithMany()
                .HasForeignKey(i => i.AssetAccountId);
            this.HasOptional(i => i.PurchasesAccount)
                .WithMany()
                .HasForeignKey(i => i.PurchasesAccountId);
            this.HasOptional(t => t.PTaxRate)
                .WithMany()
                .HasForeignKey(t => t.PTaxRateId);

            this.HasOptional(i => i.SalesAccount)
               .WithMany()
               .HasForeignKey(i => i.SalesAccountId);
            this.HasOptional(s => s.STaxRate)
                .WithMany()
                .HasForeignKey(s => s.STaxRateId);

            this.HasRequired(i => i.Companies).
               WithMany(c => c.items).
               HasForeignKey(t => t.CompanyId)
               .WillCascadeOnDelete(false);
        }

    }
}

