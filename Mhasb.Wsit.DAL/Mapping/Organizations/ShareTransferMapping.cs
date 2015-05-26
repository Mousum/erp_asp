using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Organizations
{
    public class ShareTransferMapping : EntityTypeConfiguration<ShareTransfer>
    {

        public ShareTransferMapping()
       {
           // primary key
           this.HasKey(f => f.Id);
           this.Ignore(f => f.State);

           this.Property(f => f.FromFounderId).HasColumnName("fromfounderid");
           this.Property(f => f.ToFounderId).HasColumnName("tofounderid");
           this.Property(f => f.TransferAmount).HasColumnName("transferamount");
           this.Property(f => f.TransferTime).HasColumnName("transfertime");
           this.ToTable("org.sharetransfer");

           // relationship
           this.HasRequired(f => f.FromFonder)
               .WithMany()
               .HasForeignKey(f => f.FromFounderId)
               .WillCascadeOnDelete(false);
           this.HasRequired(f => f.ToFonder)
               .WithMany()
               .HasForeignKey(f => f.ToFounderId)
               .WillCascadeOnDelete(false);
       }


    }
}
