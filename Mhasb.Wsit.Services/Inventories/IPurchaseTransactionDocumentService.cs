using Mhasb.Domain.Inventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Inventories
{
  public  interface IPurchaseTransactionDocumentService
    {
        bool AddPurchaseTransDocument(PurchaseTransactionDocument purtransdoc);
        bool UpdatePurchaseTransDocument(PurchaseTransactionDocument purtransdoc);
        bool AddPurchaseTransDocument(int Id);

    }
}
