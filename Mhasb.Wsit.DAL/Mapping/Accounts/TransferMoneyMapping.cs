
using System.Data.Entity.ModelConfiguration;
using Mhasb.Domain.Accounts;

namespace Mhasb.DAL.Mapping.Accounts
{
   public  class TransferMoneyMapping:EntityTypeConfiguration<TransferMoney>
   {
       public TransferMoneyMapping()
       {
           // key
           this.HasKey(c => c.Id);
           this.Ignore(b => b.Id);

           this.Property(b => b.FromBankId).HasColumnName("from_bankid");
           this.Property(b => b.ToBankId).HasColumnName("to_bankid");
           this.Property(b => b.TransferDate).HasColumnName("transfer_date");
           this.Property(b => b.Amount).HasColumnName("amount");
           this.Property(b => b.ReferenceNo).HasColumnName("referenceno").HasMaxLength(200);

           this.ToTable("acc.money_transfer");
           this.HasOptional(t => t.FromBank)
               .WithMany(f => f.FromTransferMoney)
               .HasForeignKey(f => f.FromBankId);
           this.HasOptional(t => t.ToBank)
               .WithMany(f => f.ToTransferMoney)
               .HasForeignKey(f => f.ToBankId);

       }
    }
}
