using Mhasb.Domain.Contacts;
using Mhasb.Domain.Inventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.CustomModel.Inventories
{
  public  class InventoriesEditBill
    {
        public PurchaseTransaction PurchaseTransactions { get; set; }
        public List<PurchaseTransactionDetail> PurchaseTransactionDetails { get; set; }
        public List<PurchaseTransactionDocument> PurchaseTransactionDocuments { get; set; }
    }
}
