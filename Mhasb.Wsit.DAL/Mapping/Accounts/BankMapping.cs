using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;

namespace Mhasb.DAL.Mapping.Accounts
{
   public class BankMapping:EntityTypeConfiguration<Bank>
   {
       public BankMapping()
       {
           // key
           this.HasKey(b => b.Id);
           this.Ignore(b => b.State);
           this.Property(b => b.BankName).HasColumnName("bank_name").HasMaxLength(200);
           this.Property(b => b.AccountName).HasColumnName("account_name").HasMaxLength(200);
           this.Property(b => b.AccountNumber).HasColumnName("account_number").HasMaxLength(100);
           this.Property(b => b.CreatedDate).HasColumnName("created_date");
           this.Property(b => b.CurrencyId).HasColumnName("currencyid");
           this.Property(b => b.AccountType).HasColumnName("account_type");

           this.ToTable("acc.banks");

           // relationship
           this.HasOptional(b => b.Currencies)
               .WithMany()
               .HasForeignKey(b => b.CurrencyId);


       }
    }
}
