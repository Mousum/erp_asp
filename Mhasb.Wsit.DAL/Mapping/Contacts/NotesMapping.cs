using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Contacts
{
   public class NotesMapping : EntityTypeConfiguration<Notes>
    {
        public NotesMapping()
        {
            this.HasKey(i=>i.Id);
            // ignore 
            this.Ignore(i => i.State);
            // Entities
            this.Property(i => i.ContactInfoId).HasColumnName("ContactInfoId");
            this.Property(i => i.UserId).HasColumnName("userid");
            this.Property(i => i.Details).HasMaxLength(250).HasColumnName("details");
            this.Property(i => i.Details).HasColumnName("details");
            this.Property(i => i.Date).HasColumnName("date");

            this.ToTable("con.Notes");
            this.HasRequired(i=>i.Users)
                .WithMany(i=>i.Notess)
                .HasForeignKey(i=>i.UserId)
                .WillCascadeOnDelete(false);
            this.HasRequired(i=>i.ContactInformations)
                .WithMany(i=>i.Notes)
                .HasForeignKey(i=>i.ContactInfoId);
        }
    }
}
