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
    public class VoucherService : IVoucherService
    {
        private readonly CrudOperation<Voucher> _finalCrudOperation = new CrudOperation<Voucher>();


        public bool CreateVoucher(Voucher voucherObj)
        {
            try
            {
                voucherObj.State = ObjectState.Added;
                _finalCrudOperation.AddOperation(voucherObj);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public bool UpdateVoucher(Voucher voucherObj)
        {
            try
            {
                voucherObj.State = ObjectState.Modified;
                _finalCrudOperation.UpdateOperation(voucherObj);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteVoucher(long Id)
        {
            try
            {
                _finalCrudOperation.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }




        public List<Voucher> GetAllVoucher()
        {
            try
            {
                var voObj = _finalCrudOperation.GetOperation()
                                        .Get().ToList();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public List<Voucher> GetAllVoucherByFinancialSettingId(int FinancialSettingId)
        {
            try
            {
                var voObj = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.FinancialSettingId == FinancialSettingId)
                                        .Get().ToList();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public List<Voucher> GetAllVoucherByVoucherTypeId(int VoucherTypeId)
        {
            try
            {
                var voObj = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.VoucherTypeId == VoucherTypeId)
                                        .Get().ToList();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public List<Voucher> GetAllVoucherByBranchId(int BranchId)
        {
            try
            {
                var voObj = _finalCrudOperation.GetOperation()
                                        .Include(c=>c.VoucherDetails)
                                        .Include(c=>c.VoucherTypes)
                                        .Include(c=>c.FinancialSettings)
                                        .Include(c=>c.Currencies)
                                        .Filter(c => c.BranchId == BranchId)
                                        .Get().ToList();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

       

        public List<Voucher> GetAllVoucherByCurrencyId(int CurrencyId)
        {
            try
            {
                var voObj = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.CurrencyId == CurrencyId)
                                        .Get().ToList();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }


        //public List<Voucher> GetAllVoucherByVoucherDate(DateTime VoucherDate)
        //{
        //    try
        //    {
        //        var voObj = _finalCrudOperation.GetOperation()
        //                                .Filter(c => c.VoucherDate >= VoucherDate)
        //                                .Filter(c => c.VoucherDate <= VoucherDate)
        //                                .Get().ToList();
        //        return voObj;
        //    }
        //    catch (Exception ex)
        //    {
        //        var rr = ex.Message;
        //        return null;
        //    }
        //}

        public long GetMaxCountByBranchId(int id)
        {
            try
            {
                var mId = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.BranchId == id)
                                        .Get().Count();
                return mId;
            }
            catch (Exception ex)
            {
                var tt = ex.Message;
                return -1;
            }
        }


        public long CountByBranchIdAndPrefix(int BranchId, string PreFix)
        {
            try
            {
                var mId = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.BranchId == BranchId && c.RefferenceNo.StartsWith(PreFix))
                                        .Get().Count();
                return mId;
            }
            catch (Exception ex)
            {
                var tt = ex.Message;
                return -1;
            }
        }
        public Voucher GetSingleVoucher(int VId)
        {
            try
            {
                var voObj = _finalCrudOperation.GetOperation()
                                        .Include(c => c.VoucherDetails)
                                        .Include(c => c.VoucherDetails.Select(d=>d.ChartOfAccounts))
                                        .Include(c => c.VoucherTypes)
                                        .Include(c => c.FinancialSettings)
                                        .Include(c => c.Currencies)
                                        .Filter(c => c.Id == VId)
                                        .Get().SingleOrDefault();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
    }
}
