using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Organizations
{
    public class CompanyDocumentMapping : EntityTypeConfiguration<CompanyDocument>
    {
        public CompanyDocumentMapping()
        {
            // Key
            this.HasKey(cd => cd.Id);
            this.Property(cd => cd.CompanyId).HasColumnName("companyid");
            this.Property(cd => cd.DocumentLocation).HasMaxLength(100).HasColumnName("document_location");
            this.ToTable("org.companydocuments");
            
            
            //Relations
            this.HasRequired(c=>c.companies)
                        .WithMany(c=>c.Documents)
                        .HasForeignKey(cd => cd.CompanyId);

         }
    }
}
