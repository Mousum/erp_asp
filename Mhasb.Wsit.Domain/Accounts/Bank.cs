using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Accounts
{
   public class Bank:IObjectStateInt
    {
       public string BankName { get; set; }
       public string AccountName { get; set; }
       public string AccountNumber { get; set; }
       public DateTime CreatedDate { get; set; }
       public int? CurrencyId { get; set; }
       public EnumAccountType AccountType { get; set; }
       public ObjectState State { get; set; }
       public int Id { get; set; }
       public virtual Currency Currencies { get; set; }
       public virtual ICollection<TransferMoney> FromTransferMoney { get; set; }
       public virtual ICollection<TransferMoney> ToTransferMoney { get; set; }
    }

    public enum EnumAccountType
    {
        BankAccount=1,
        CreditCard=2,
        PayPal=3
    }
}
