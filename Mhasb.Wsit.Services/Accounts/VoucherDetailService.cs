using Mhasb.Domain.Accounts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Accounts
{
    public class VoucherDetailService : IVoucherDetailService
    {

        private readonly CrudOperation<VoucherDetail> _finalCrudOperation = new CrudOperation<VoucherDetail>();
        public bool CreateVoucherDetail(Domain.Accounts.VoucherDetail voucherdetail)
        {
            try
            {
                voucherdetail.State = ObjectState.Added;
                _finalCrudOperation.AddOperation(voucherdetail);
                voucherdetail.State = ObjectState.Unchanged;
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public bool UpdateVoucherDetail(Domain.Accounts.VoucherDetail voucherdetail)
        {
            try
            {
                voucherdetail.State = ObjectState.Modified;
                _finalCrudOperation.AddOperation(voucherdetail);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public bool DeleteVoucherDetail(long Id)
        {
            try
            {
                _finalCrudOperation.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }


        public List<VoucherDetail> GetAllVoucherDetailByVoucherId(int VoucherId)
        {
            try
            {
                var vodObj = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.VoucherId == VoucherId)
                                        .Get().ToList();

                return vodObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public List<VoucherDetail> GetAllVoucherDetailByCoaId(int CoaId)
        {
            try
            {
                var vodObj = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.CoaId == CoaId)
                                        .Get().ToList();

                return vodObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }


        public List<VoucherDetail> GetAllVoucherDetail()
        {
            try
            {
                var vodObj = _finalCrudOperation.GetOperation()
                                        .Get().ToList();

                return vodObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public List<VoucherDetail> GetAllVoucherDetailById(long Id)
        {
            try
            {
                var vodObj = _finalCrudOperation.GetOperation()
                                        .Filter(c=>c.Id==Id)
                                        .Get().ToList();

                return vodObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
    }
}
