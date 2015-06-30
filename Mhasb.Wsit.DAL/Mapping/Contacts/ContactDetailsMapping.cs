using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Contacts
{
    public class ContactsDetailsMapping : EntityTypeConfiguration<ContactDetails>
    {
        public ContactsDetailsMapping()
        {
            this.HasKey(i => i.Id);
            // ignore 
            this.Ignore(i => i.State);
            this.Property(i => i.ContactInfoId).HasColumnName("contactinfoid");
            this.Property(i => i.Attention).HasMaxLength(100).HasColumnName("attention");
            this.Property(i => i.Address).HasMaxLength(100).HasColumnName("address");
            this.Property(i => i.City).HasMaxLength(100).HasColumnName("city");
            this.Property(i => i.StateRegion).HasMaxLength(100).HasColumnName("stateregion");
            this.Property(i => i.ZipeCode).HasMaxLength(100).HasColumnName("zipecode");
            this.Property(i => i.CountryId).HasColumnName("countryid");
            this.Property(i => i.Type).HasColumnName("type");
            this.ToTable("con.contactdetails");

            this.HasRequired(i=>i.ContactInformations)
                .WithMany(i=>i.ContactDtails)
                .HasForeignKey(i=>i.ContactInfoId);

            this.HasOptional(i => i.Country)
                .WithMany()
                .HasForeignKey(i=>i.CountryId)
                .WillCascadeOnDelete(false);
        }
    }
}
