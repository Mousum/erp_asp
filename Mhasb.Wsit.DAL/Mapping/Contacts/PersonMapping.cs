using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Contacts
{
    public class PersonMapping : EntityTypeConfiguration<Person>
    {
        public PersonMapping() {
            this.HasKey(i=>i.Id);
            //Ignore this fld
            this.Ignore(i => i.State);
            this.Property(i => i.ContactInfoId).HasColumnName("ContactInfoId");
            this.Property(i => i.FirstName).HasMaxLength(200).HasColumnName("firstname");
            this.Property(i => i.LastName).HasMaxLength(200).HasColumnName("lastname");
            this.Property(i => i.Email).HasMaxLength(200).HasColumnName("email");
            this.Property(i => i.Email).HasMaxLength(200).HasColumnName("email");
            this.Property(i => i.IsIncludeEmail).HasColumnName("IsIncludeEmail");
            this.Property(i => i.IsPrimaryPerson).HasColumnName("IsPrimaryPerson");

            this.ToTable("con.person");

            this.HasRequired(i=>i.ContactInformations)
                .WithMany()
                .HasForeignKey(i=>i.ContactInfoId);
        }
    }
}
