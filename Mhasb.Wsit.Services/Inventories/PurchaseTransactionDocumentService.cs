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
    public class PurchaseTransactionDocumentService : IPurchaseTransactionDocumentService
    {
        private readonly CrudOperation<PurchaseTransactionDocument> _rep = new CrudOperation<PurchaseTransactionDocument>();
        public bool AddPurchaseTransDocument(PurchaseTransactionDocument purtransdoc)
        {
            try { 
             purtransdoc.State=ObjectState.Added;
             _rep.AddOperation(purtransdoc);
             return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdatePurchaseTransDocument(PurchaseTransactionDocument purtransdoc)
        {
            try
            {
                purtransdoc.State = ObjectState.Modified;
                _rep.UpdateOperation(purtransdoc);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool AddPurchaseTransDocument(int Id)
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
