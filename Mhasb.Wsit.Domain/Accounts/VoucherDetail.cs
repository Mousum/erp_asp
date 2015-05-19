using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Accounts
{
   public class VoucherDetail:IObjectStateLong
    {
       public long VoucherId { get; set; }
       public int CoaId { get; set; }
       
       public decimal Debit { get; set; }
       public decimal Credit { get; set; }

       //public decimal ConvDr { get; set; }
       //public decimal ConvCr { get; set; }
       
       //chat of Account
       public virtual ChartOfAccount ChartOfAccounts { get; set; }
       //Voucher
       public virtual Voucher Vouchers { get; set; }

       public long Id { get; set; }
        public ObjectState State { get; set; }
    }
}
