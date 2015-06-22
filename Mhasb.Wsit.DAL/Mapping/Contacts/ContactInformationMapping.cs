using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Contacts
{
  public  class ContactInformationMapping : EntityTypeConfiguration<ContactInformation>
    {
        public ContactInformationMapping() {
            this.HasKey(i => i.Id);
            // ignore 
            this.Ignore(i => i.State);
            this.Property(i => i.ContactName).HasMaxLength(100).HasColumnName("contactname");
            this.Property(i => i.AccountNumber).HasMaxLength(100).HasColumnName("accountnumber");
            this.Property(i => i.PostalAddId).HasColumnName("postaladdid");
            this.Property(i => i.PhysicalAddId).HasColumnName("physicaladdid");
            this.Property(i => i.ContactType).HasMaxLength(100).HasColumnName("contacttype");
            this.Property(i => i.CreateBy).HasColumnName("createby");
            this.Property(i => i.UpdateBy).HasColumnName("updateby");
            this.Property(i => i.CreateDate).HasColumnName("createdate");
            this.Property(i => i.UpdateDate).HasColumnName("updatedate");
            this.ToTable("con.contactinformation");

            //this.HasRequired(i => i.ContactDtails)
            // .WithMany(i => i.)
            // .HasForeignKey(i => i.PostalAddId);

            this.HasRequired(i=>i.Users)
                .WithMany()
                .HasForeignKey(i => i.CreateBy)
                .WillCascadeOnDelete(false);
            this.HasRequired(i => i.Users)
               .WithMany()
               .HasForeignKey(i => i.UpdateBy)
               .WillCascadeOnDelete(false);
            this.HasRequired(i => i.ContactDtails)
              .WithMany()
              .HasForeignKey(i => i.PostalAddId);
            this.HasRequired(i => i.ContactDtails)
              .WithMany()
              .HasForeignKey(i => i.PhysicalAddId);
            this.HasRequired(i => i.Companies)
              .WithMany()
              .HasForeignKey(i => i.CompanyId);

        }
    }
}
