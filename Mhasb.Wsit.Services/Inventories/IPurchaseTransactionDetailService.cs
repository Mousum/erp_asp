using Mhasb.Domain.Inventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Inventories
{
    public interface IPurchaseTransactionDetailService
    {
        bool AddPurchaseTransDetailService(PurchaseTransactionDetail PurchaseTS);
        bool UpdatePurchaseTransDetailService(PurchaseTransactionDetail PurchaseTS);
        bool DeletePurchaseTransDetailService(int Id);

    }
}
