using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;
using Mhasb.Wsit.Domain;
using Mhasb.Domain.Commons;

namespace Mhasb.Domain.Inventories
{
    public class SelesTransactionDetail : IObjectStateLong
    {

        public long PurchaseTransactionId { get; set; }
        public long ItemId { get; set; }
        public int CoaId { get; set; }
        public EnumTaxOnTransaction TaxOnTransaction { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public int? TaxId { get; set; }
        public string Description { get; set; }
        
        //
        public virtual SelesTransaction SelesTransactions { get; set; }
        public virtual Item Items { get; set; }

        public virtual Lookup Lookups { get; set; }
        public virtual ChartOfAccount ChartOfAccounts { get; set; }// on side relation

        public ObjectState State { get; set; }
        public long Id { get; set; }
    }

   
}
