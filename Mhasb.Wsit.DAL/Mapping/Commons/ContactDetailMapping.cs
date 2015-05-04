using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Commons
{
    public class ContactDetailMapping : EntityTypeConfiguration<ContactDetail>
    {
        public ContactDetailMapping()
        {
            //  key

            this.HasKey(cd => cd.Id);
            this.Ignore(cd => cd.State);

            this.Property(cd => cd.EmployeeProfileId).HasColumnName("employee_profile_id");
            this.Property(cd => cd.CompanyProfileId).HasColumnName("company_profile_id");

            this.Property(cd => cd.FieldName).HasColumnName("field_name").HasMaxLength(100);
            this.Property(cd => cd.FieldUrl).HasColumnName("field_url").HasMaxLength(100);
            this.Property(cd => cd.FieldValueOne).HasColumnName("field_value_one").HasMaxLength(100);
            this.Property(cd => cd.FieldValueTwo).HasColumnName("field_value_two").HasMaxLength(100);
            this.Property(cd => cd.FieldValueThree).HasColumnName("field_value_three").HasMaxLength(100);

            this.ToTable("com.contact_detail");

            // relation
            this.HasOptional(cd => cd.EmployeeProfiles)
                .WithMany(cd => cd.ContactDetails)
                .HasForeignKey(cd=>cd.EmployeeProfileId);

            this.HasOptional(cd => cd.CompanyProfiles)
                .WithMany(cd => cd.ContactDetails)
                .HasForeignKey(cd => cd.CompanyProfileId);

        }
    }
}
