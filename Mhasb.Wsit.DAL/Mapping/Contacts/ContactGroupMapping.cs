using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Contacts;

namespace Mhasb.DAL.Mapping.Contacts
{
    public class ContactGroupMapping : EntityTypeConfiguration<ContactGroup>
    {
        public ContactGroupMapping()
        {
            this.HasKey(i => i.Id);
            this.Ignore(i => i.State);

            this.Property(p => p.GroupName).HasColumnName("group_name").HasMaxLength(200);
            this.ToTable("con.contact_group");
        }
    }
}
