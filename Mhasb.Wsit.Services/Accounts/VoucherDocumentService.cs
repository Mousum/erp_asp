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
   public class VoucherDocumentService : IVoucherDocument
    {
       private readonly CrudOperation<VoucherDocument> _crudOperation = new CrudOperation<VoucherDocument>();
        public bool AddDocument(VoucherDocument voucherDocument)
        {

            try
            {
                voucherDocument.State = ObjectState.Added;
                _crudOperation.AddOperation(voucherDocument);
                voucherDocument.State = ObjectState.Unchanged;
                return true;
            }
            catch (Exception ex)
            {

                var rr = ex.Message;
                return false;
            }
        }

       public List<VoucherDocument> GetDocumentByVoucherId(long voucherId)
       {
           var vdList = _crudOperation.GetOperation()
               .Filter(vd => vd.VoucherId == voucherId)
               .Get().ToList();

           return vdList;
       }

       public bool UpdateDocumentVoucher(VoucherDocument voucherDocument)
       {


           try
           {
               var dbObj = _crudOperation.GetSingleObject(voucherDocument.Id);

               dbObj.Description = voucherDocument.Description;
               voucherDocument.State = ObjectState.Modified;
               _crudOperation.UpdateOperation(dbObj);
               return true;
           }
           catch (Exception ex)
           {

               var rr = ex.Message;
               return false;
           }
       }

       public bool DeleteDocumentVoucher(long voucherDocId)
       {
           try
           {
               var dbObj = _crudOperation.GetSingleObject(voucherDocId);

               dbObj.State = ObjectState.Deleted;
               _crudOperation.DeleteOperation(voucherDocId);
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
