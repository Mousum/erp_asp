using Mhasb.Domain.Inventories;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Inventories
{
    public class PurchaseTransactionService : IPurchaseTransactionService
    {
        private readonly CrudOperation<PurchaseTransaction> _rep = new CrudOperation<PurchaseTransaction>();
        public bool AddPurchaseTransaction(PurchaseTransaction purchasetransaction)
        {
            try {
                purchasetransaction.State = ObjectState.Added;
                _rep.AddOperation(purchasetransaction);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdatePurchaseTransaction(Domain.Inventories.PurchaseTransaction purchasetransaction)
        {
            try
            {
                purchasetransaction.State = ObjectState.Modified;
                _rep.UpdateOperation(purchasetransaction);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeletePurchaseTransaction(int Id)
        {
            try
            {
                _rep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }
    }
}
