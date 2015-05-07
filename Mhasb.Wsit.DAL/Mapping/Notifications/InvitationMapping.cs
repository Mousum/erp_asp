using Mhasb.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Notifications
{
    public class InvitationMapping : EntityTypeConfiguration<Invitation>
    {
        public InvitationMapping()
        {
            this.HasKey(i=>i.Id);
            // ignore 
            this.Ignore(i=>i.State);
            this.Property(i => i.SendDate).HasColumnName("send_date");
            this.Property(i => i.UpdateDate).HasColumnName("update_date");
            this.Property(i => i.Token).HasMaxLength(100).HasColumnName("token");
            this.Property(i => i.CompanyId).HasColumnName("companyid");
            this.Property(i => i.Email).HasMaxLength(100).HasColumnName("email");
            this.Property(i => i.EmployeeType).HasColumnName("employee_type");
            this.Property(i => i.RoleId).HasColumnName("roleid");
            this.Property(i => i.Status).HasColumnName("status");

            this.ToTable("nts.invitations");
        }
    }
}
