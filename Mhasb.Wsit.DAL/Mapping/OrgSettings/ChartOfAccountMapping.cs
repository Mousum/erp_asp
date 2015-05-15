﻿using Mhasb.Domain.OrgSettings;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.OrgSettings
{
    public class ChartOfAccountMapping : EntityTypeConfiguration<ChartOfAccount>
    {
        public ChartOfAccountMapping(){
            this.HasKey(c=>c.Id);
            this.Ignore(c => c.State);
            this.Property(c => c.CompanyId).HasColumnName("companyid");
            this.Property(c => c.AType).HasColumnName("atype");
            this.Property(c => c.ACode).HasColumnName("acode");
            this.Property(c => c.AName).HasColumnName("aname");
            this.Property(c => c.Description).HasMaxLength(2000).HasColumnName("Description");
            this.Property(c => c.Tax).HasColumnName("tax");
            this.Property(c => c.ShowInDashboard).HasColumnName("showsnsashboard");
            this.Property(c => c.ShowInExpenseClaims).HasColumnName("showinexpenseclaims");
            this.Property(c => c.IsCostCenter).HasColumnName("iscostcenter");

            this.ToTable("set.chartofaccount");

            this.HasRequired(c => c.Companies)
                .WithMany()
                .HasForeignKey(c => c.CompanyId);
       }
    }
}
