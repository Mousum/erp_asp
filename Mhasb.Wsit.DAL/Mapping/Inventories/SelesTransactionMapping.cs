using Mhasb.Domain.Inventories;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Inventories
{
 
        public class SelesTransactionMapping : EntityTypeConfiguration<SelesTransaction>
        {
            public SelesTransactionMapping()
            {
                this.HasKey(t => t.Id);
                this.Ignore(t => t.State);
                this.Property(t => t.ContactId).HasColumnName("contactid");
                this.Property(t => t.EmployeeId).HasColumnName("employeeid");
                this.Property(t => t.Currencyid).HasColumnName("currencyid");
                this.Property(t => t.TransactionDate).HasColumnName("transaction_date");
                this.Property(t => t.DeliveryDate).HasColumnName("delivery_date");

                this.Property(t => t.ReferenceNo).HasColumnName("referenceno").HasMaxLength(100);
                this.Property(t => t.OrderNumber).HasColumnName("order_number").HasMaxLength(100);
                this.Property(t => t.TransactionType).HasColumnName("transaction_type");

                this.Property(t => t.PoDeliveryInstruction).HasColumnName("po_delivery_instruction").HasMaxLength(500);
                this.Property(t => t.PoAttention).HasColumnName("po_attention").HasMaxLength(100);

                this.Property(t => t.PoTelephone).HasColumnName("po_telephone").HasMaxLength(100);
                this.Property(t => t.PoAddress).HasColumnName("po_address").HasMaxLength(300);
                this.Property(t => t.CompanyId).HasColumnName("company_id");

                this.ToTable("inv.sales_transaction");

                // relationship
                this.HasRequired(t => t.ContactInformations)
                    .WithMany(t => t.SelesTransactions)
                    .HasForeignKey(t => t.ContactId);

                this.HasRequired(t => t.Currencies)
                    .WithMany()
                    .HasForeignKey(t => t.Currencyid);

                this.HasRequired(t => t.Employees)
                    .WithMany(t => t.SelesTransactions)
                    .HasForeignKey(t => t.EmployeeId);

                this.HasRequired(t => t.Companies).
                   WithMany(t => t.SelesTransections).
                   HasForeignKey(t => t.CompanyId)
                   .WillCascadeOnDelete(false);
            }
        }
    }

