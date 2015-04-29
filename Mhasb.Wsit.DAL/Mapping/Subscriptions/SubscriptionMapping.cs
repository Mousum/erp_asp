using Mhasb.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Subscriptions
{
    public class SubscriptionMapping : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionMapping() {
            this.HasKey(s => s.Id);

            this.Ignore(s => s.State);
            this.Property(s => s.CompanyId).HasColumnName("CompanyId");
            this.Property(s => s.PackageId).HasColumnName("PackageId");
            this.Property(p => p.StartDate).HasColumnName("StartDate");
            this.Property(p => p.EndDate).HasColumnName("EndDate");

            this.ToTable("com.subscription");
        }
    }
}
