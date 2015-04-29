using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Organizations
{
    public class EmployeeMapping : EntityTypeConfiguration<Employee>
    {
        public EmployeeMapping() {
            this.HasKey(emp=>emp.Id);
            this.Ignore(emp=>emp.State);
            this.Property(emp => emp.UserId).HasColumnName("userid");
            this.Property(emp => emp.CompanyId).HasColumnName("companyid");
            this.Property(emp => emp.DesignationId).HasColumnName("designationid");
            this.Property(emp => emp.BranchId).HasColumnName("branchid");
            this.Property(emp => emp.SignatureLocation).HasColumnName("signaturelocation").HasMaxLength(100);

            this.ToTable("org.employees");

            this.HasRequired(emp => emp.Users).
                WithMany(emp => emp.Employees).
                HasForeignKey(emp => emp.UserId);

            this.HasRequired(emp => emp.Companies).
                WithMany(emp => emp.Employees).
                HasForeignKey(emp => emp.CompanyId);





        }
    }
}
