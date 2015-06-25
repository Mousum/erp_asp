using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Accounts
{
    public class TransferMoney : IObjectStateLong
    {
        public int? FromBankId { get; set; }
        public int? ToBankId { get; set; }
        public DateTime TransferDate { get; set; }
        public double Amount { get; set; }

        public string ReferenceNo { get; set; }
        public ObjectState State { get; set; }
        public long Id { get; set; }

        public virtual Bank FromBank { get; set; }
        public virtual Bank ToBank { get; set; }
    }
}
