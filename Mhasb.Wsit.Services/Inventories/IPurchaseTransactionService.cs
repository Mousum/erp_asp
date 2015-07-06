using Mhasb.Domain.Inventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Inventories
{
   public interface IPurchaseTransactionService
    {
        bool AddPurchaseTransaction(PurchaseTransaction purchasetransaction);
        bool UpdatePurchaseTransaction(PurchaseTransaction purchasetransaction);
        bool DeletePurchaseTransaction(int Id);
        List<PurchaseTransaction> GetAllPurchaseTransaction();
        //PurchaseTransaction GetRelationalDataByTansId();
        List<PurchaseTransaction> GetRelationalDataByTansId(int tansId);

    }
}
