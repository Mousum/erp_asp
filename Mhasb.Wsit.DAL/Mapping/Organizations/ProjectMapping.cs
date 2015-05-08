using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Organizations
{
    public class ProjectMapping : EntityTypeConfiguration<Project>
    {
        public ProjectMapping()
        {
            // key
            this.HasKey(p => p.Id);
            this.Ignore(p => p.NumberOfMember);
            this.Ignore(p => p.State);
            this.Ignore(p=>p.TaskNumber);

            // proprty
            this.Property(p=>p.ManagerId).HasColumnName("managerid");
            this.Property(p => p.ProjectName).HasMaxLength(100).IsRequired().HasColumnName("project_name");
            this.Property(p => p.ProjectDate).HasColumnName("project_date");
            this.Property(p => p.StartingDate).HasColumnName("starting_date");
            this.Property(p => p.FinishingDate).HasColumnName("finishingDate").IsOptional();
            this.Property(p => p.Status).HasColumnName("status");

            // relationship
            this.HasRequired(p => p.Employees)
                .WithMany(p=>p.Projects)
                .HasForeignKey(p=>p.ManagerId);

        }
    }
}
