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
            this.Property(i => i.CellPhone).HasColumnName("cellphone");
            this.Property(i => i.Mobile).HasColumnName("mobile");
            this.Property(i => i.DirectDial).HasColumnName("directdial");
            this.Property(i => i.Skype).HasColumnName("skype");
            this.Property(i => i.WebSite).HasColumnName("webSite");

            this.ToTable("con.telephone");

            this.HasRequired(i=>i.ContactInformations)
                .WithMany()
                .HasForeignKey(i=>i.ContactInfoId);
        }
    }
}
