using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Contacts
{
    public class TelePhoneMapping : EntityTypeConfiguration<TelePhone>
    {
        public TelePhoneMapping() {
            this.HasKey(i=>i.Id);
            this.Ignore(i=>i.State);
            this.Property(i => i.ContactInfoId).HasColumnName("contactinfoId");
            this.Property(i => i.CellPhone).HasColumnName("cellphone").HasMaxLength(100);
            this.Property(i => i.Mobile).HasColumnName("mobile").HasMaxLength(100);
            this.Property(i => i.DirectDial).HasColumnName("directdial").HasMaxLength(100);
            this.Property(i => i.Skype).HasColumnName("skype").HasMaxLength(100);
            this.Property(i => i.WebSite).HasColumnName("webSite").HasMaxLength(100);
            this.Property(i => i.Fax).HasColumnName("fax").HasMaxLength(100);

            this.ToTable("con.telephone");

            this.HasRequired(i=>i.ContactInformations)
                .WithMany(i=>i.TelePhones)
                .HasForeignKey(i=>i.ContactInfoId);
        }
    }
}
