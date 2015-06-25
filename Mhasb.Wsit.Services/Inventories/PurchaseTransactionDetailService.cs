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
    public class PurchaseTransactionDetailService : IPurchaseTransactionDetailService
    {
        private readonly CrudOperation<PurchaseTransactionDetail> _rep = new CrudOperation<PurchaseTransactionDetail>();


        public bool AddPurchaseTransDetailService(PurchaseTransactionDetail PurchaseTS)
        {
            try {
                PurchaseTS.State=ObjectState.Added;
                _rep.AddOperation(PurchaseTS);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdatePurchaseTransDetailService(PurchaseTransactionDetail PurchaseTS)
        {
            try
            {
                PurchaseTS.State = ObjectState.Modified;
                _rep.UpdateOperation(PurchaseTS);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeletePurchaseTransDetailService(int Id)
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
