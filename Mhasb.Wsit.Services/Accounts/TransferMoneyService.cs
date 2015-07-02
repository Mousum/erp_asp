using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.Accounts
{
   public class TransferMoneyService:ITransferMoneyService
    {
       private readonly ICrudOperation<TransferMoney> _crudOperation = new CrudOperation<TransferMoney>();
       public bool AddTransferMoney(TransferMoney transferMoney)
       {
           try
           {
            transferMoney.State = ObjectState.Added;
               _crudOperation.AddOperation(transferMoney);
               return true;
           }
           catch (Exception ex)
           {
               var rr = ex.Message;
               return false;
           }
       }

       public bool UpdateTransferMoney(TransferMoney transferMoney)
       {
           try
           {
               var dbObj = _crudOperation.GetSingleObject(transferMoney.Id);
               dbObj.Amount = transferMoney.Amount;
               dbObj.ReferenceNo = transferMoney.ReferenceNo;
               dbObj.FromBankId = transferMoney.FromBankId;
               dbObj.ToBankId = transferMoney.ToBankId;
               dbObj.State = ObjectState.Modified;
               _crudOperation.AddOperation(transferMoney);
               return true;
           }
           catch (Exception ex)
           {
               var rr = ex.Message;
               return false;
           }
       }

       public bool DeleteTransferMoney(long id)
       {
           try
           {
               _crudOperation.DeleteOperation(id);
               return true;
           }
           catch (Exception ex)
           {
               var rr = ex.Message;
               return false;
           }
       }
    }
}
