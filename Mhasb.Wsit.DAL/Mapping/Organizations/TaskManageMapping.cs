using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Organizations
{
    public class TaskManageMapping : EntityTypeConfiguration<TaskManager>
    {
        public TaskManageMapping()
        {
            // key
            this.HasKey(p => p.Id);
            this.Ignore(p => p.State);

            // proprty
            this.Property(p => p.TaskTo).HasColumnName("employeeid");
            this.Property(p => p.Tite).HasMaxLength(100).IsRequired().HasColumnName("title");
            this.Property(p => p.ProjectId).HasColumnName("projectid");
            this.Property(p => p.TaskDate).HasColumnName("task_date");
            this.Property(p => p.StartingDate).HasColumnName("starting_date");
            this.Property(p => p.FinishingDate).HasColumnName("finishing_Date").IsOptional();
            this.Property(p => p.Status).HasColumnName("status");

            // relationship
            this.HasRequired(p => p.Employees)
                .WithMany(p => p.TaskManagers)
                .HasForeignKey(p => p.TaskTo);

            this.HasRequired(p => p.Projects)
                .WithMany(p => p.TaskManagers)
                .HasForeignKey(p=>p.ProjectId)
                .WillCascadeOnDelete(false);
        }
    }
}
