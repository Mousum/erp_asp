using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;

namespace Mhasb.DAL.Mapping.Accounts
{
    public class VoucherDocumentMapping:EntityTypeConfiguration<VoucherDocument>
    {
        public VoucherDocumentMapping()
        {
            //key
            this.HasKey(d => d.Id);
            this.Ignore(d => d.State);
            this.Property(d => d.EmployeeId).HasColumnName("employeeid");
            this.Property(d => d.VoucherId).HasColumnName("voucherid");
            this.Property(d => d.DocumentType).HasColumnName("document_type").HasMaxLength(50).IsRequired();
            this.Property(d => d.CreatedDate).HasColumnName("created_date");
            this.Property(d => d.Description).HasColumnName("description").HasMaxLength(500).IsOptional();
            this.Property(d => d.FileLocation).HasColumnName("file_location").HasMaxLength(200).IsOptional();

            this.ToTable("acc.voucher_documents");

            // Relationship
            this.HasRequired(e => e.Vouchers)
                .WithMany(e => e.VoucherDocuments)
                .HasForeignKey(e => e.VoucherId);

            this.HasRequired(e => e.Employees)
               .WithMany()
               .HasForeignKey(e => e.EmployeeId)
               .WillCascadeOnDelete(false);
        }
    }
}
