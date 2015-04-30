using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mhasb.DAL.Mapping.Organizations
{
    public class CompanyMapping : EntityTypeConfiguration<Company>
    {
        public CompanyMapping()
        {
            // Key
            this.HasKey(c => c.Id);
            // ignor
            this.Ignore(c => c.State);

            this.Property(c => c.TradingName).HasMaxLength(100).HasColumnName("trading_name");
            this.Property(c => c.LegalName).HasMaxLength(100).HasColumnName("legal_name");
            this.Property(c => c.DisplayName).HasMaxLength(100).HasColumnName("display_name");
            this.Property(c => c.Tel).HasMaxLength(100).HasColumnName("tel");
            this.Property(c=>c.Fax).HasMaxLength(100).HasColumnName("fax");
            this.Property(c => c.P_O_Box).HasColumnName("p_o_box");
            this.Property(c => c.Email).HasMaxLength(100).HasColumnName("email");
            this.Property(c => c.Location).HasMaxLength(200).HasColumnName("location");
            this.Property(c => c.Website).HasMaxLength(100).HasColumnName("web_site");
            this.Property(c => c.CountryId).HasColumnName("countryid");
            this.Property(c => c.LanguageId).HasColumnName("languageid");
            this.Property(c => c.IndustryId).HasColumnName("industryid");

            this.Property(c => c.LegalEntityId).HasColumnName("legal_entity_id");
            this.Property(c => c.TimezoneId).HasColumnName("time_zone_id");
            this.Property(c => c.SealLocation).HasMaxLength(100).HasColumnName("seal_location");
            this.Property(c => c.LogoLocation).HasMaxLength(100).HasColumnName("logo_location");
            this.Property(c => c.DocumentLocation).HasMaxLength(100).HasColumnName("document_location");
            this.ToTable("org.companies");

            //Relationship
            this.HasOptional(t => t.AreaTimes)
                .WithMany()
                .HasForeignKey(t => t.TimezoneId);
            this.HasOptional(t => t.LegalEntities)
                .WithMany()
                .HasForeignKey(t => t.LegalEntityId);

            this.HasRequired(c => c.Countries)
                .WithMany(co=>co.Companies)
                .HasForeignKey(c=>c.CountryId);

            this.HasRequired(c => c.Languages)
                .WithMany(co => co.Companies)
                .HasForeignKey(c => c.LanguageId);
            this.HasRequired(c => c.Industries)
                .WithMany(co => co.Companies)
                .HasForeignKey(c => c.IndustryId);
            this.HasRequired(u => u.Users)
                .WithOptional();

        }
    }
}
