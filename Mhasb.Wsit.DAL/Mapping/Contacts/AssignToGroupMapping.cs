using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Contacts;

namespace Mhasb.DAL.Mapping.Contacts
{
    public class AssignToGroupMapping : EntityTypeConfiguration<AssignToGroup>
    {
        public AssignToGroupMapping()
        {
            this.HasKey(i => i.Id);
            this.Ignore(i => i.State);

            this.Property(p => p.ContactInfoId).HasColumnName("contact_Infoid");
            this.Property(p => p.ContactGroupId).HasColumnName("contact_groupid");
            this.Property(p => p.CreatedTime).HasColumnName("created_time");

            this.ToTable("con.assign_to_group");

            this.HasRequired(p => p.ContactGroups)
                .WithMany(p => p.AssignToGroups)
                .HasForeignKey(p => p.ContactGroupId);

            this.HasRequired(p => p.ContactInformations)
                .WithMany()
                .HasForeignKey(p => p.ContactInfoId)
                .WillCascadeOnDelete(false);
        }
    }
}
