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
        private readonly CrudOperation<PurchaseTransaction> _repPt = new CrudOperation<PurchaseTransaction>();
        private readonly CrudOperation<PurchaseTransactionDetail> _repPtdl = new CrudOperation<PurchaseTransactionDetail>();
        private readonly CrudOperation<PurchaseTransactionDocument> _repPtdc = new CrudOperation<PurchaseTransactionDocument>();
        private readonly CrudOperation<Item> _repit = new CrudOperation<Item>();
        public bool AddPurchaseTransaction(PurchaseTransaction purchasetransaction)
        {
            try {
                purchasetransaction.State = ObjectState.Added;
                _repPt.AddOperation(purchasetransaction);
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
                _repPt.UpdateOperation(purchasetransaction);
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
                _repPt.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }



        public List<PurchaseTransaction> GetAllPurchaseTransaction()
        {
            try { 
             var _obj=_repPt.GetOperation()
                 .Get()
                 .ToList();
                return _obj;
            }catch(Exception ex){
                var msg = ex.Message;
                return null;
            }
        }



    
        public List<PurchaseTransaction> GetRelationalDataByTansId(int tansId)
        {

           
            try {
                var _obj = _repPt.GetOperation()
                         .Include(i=>i.PurchaseTransactionDetails)
                         .Include(i => i.PurchaseTransactionDocuments)
                         .Include(i=>i.PurchaseTransactionDetails.Select(h=>h.Items))
                         .Filter(i => i.Id == tansId)
                         .Get().ToList();
                        

                return _obj;
            }catch(Exception Ex){
                var msg = Ex.Message;
                return null;
            }
        }


        //PurchaseTransaction GetRelationalDataByTansId()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
