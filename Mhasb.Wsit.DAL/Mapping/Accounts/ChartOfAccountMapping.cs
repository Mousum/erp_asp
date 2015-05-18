using System.Data.Entity.ModelConfiguration;
using Mhasb.Domain.Accounts;

namespace Mhasb.DAL.Mapping.Accounts
{
    public class ChartOfAccountMapping : EntityTypeConfiguration<ChartOfAccount>
    {
        public ChartOfAccountMapping(){
            this.HasKey(c=>c.Id);
            this.Ignore(c => c.State);
            this.Property(c => c.LookupId).HasColumnName("lookupid");
            this.Property(c => c.CompanyId).HasColumnName("companyid");
            this.Property(c => c.ACode).HasColumnName("acode").HasMaxLength(10).IsRequired();
            this.Property(c => c.AName).HasColumnName("aname");
            this.Property(c => c.Description).HasMaxLength(2000).HasColumnName("Description");
            this.Property(c => c.Tax).HasColumnName("tax");
            this.Property(c => c.ShowInDashboard).HasColumnName("showsnsashboard");
            this.Property(c => c.ShowInExpenseClaims).HasColumnName("showinexpenseclaims");
            this.Property(c => c.IsCostCenter).HasColumnName("iscostcenter");

            this.ToTable("acc.chartofaccount");

            this.HasRequired(c => c.Companies)
                .WithMany()
                .HasForeignKey(c => c.CompanyId);
            this.HasRequired(c => c.Lookups)
                .WithMany()
                .HasForeignKey(c => c.LookupId);
       }
    }
}
