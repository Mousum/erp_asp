using Mhasb.Domain.Inventories;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Inventories
{
    public class SelesTransactionDocumentMapping : EntityTypeConfiguration<SelesTransactionDocument>
    {
        public PurchaseTransactionDocumentMapping()
        {
            //key
            this.HasKey(d => d.Id);
            this.Ignore(d => d.State);
            this.Property(d => d.EmployeeId).HasColumnName("employeeid");
            this.Property(d => d.PurchaseTransactionId).HasColumnName("purchase_transactionid");
            this.Property(d => d.DocumentType).HasColumnName("document_type").HasMaxLength(50).IsRequired();
            this.Property(d => d.CreatedDate).HasColumnName("created_date");
            this.Property(d => d.Description).HasColumnName("description").HasMaxLength(500).IsOptional();
            this.Property(d => d.FileLocation).HasColumnName("file_location").HasMaxLength(200).IsOptional();

            this.ToTable("inv.purchase_transaction_documents");

            // Relationship
            this.HasRequired(e => e.SelesTransactions)
                .WithMany(e => e.SelesTransactionDocuments)
                .HasForeignKey(e => e.PurchaseTransactionId)
                .WillCascadeOnDelete(false);

            this.HasRequired(e => e.Employees)
               .WithMany()
               .HasForeignKey(e => e.EmployeeId)
               .WillCascadeOnDelete(false);
        }
    }
}
