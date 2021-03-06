﻿using Mhasb.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Accounts
{
   public interface IVoucherService
    {
       bool CreateVoucher(Voucher voucherObj);
       bool UpdateVoucher(Voucher voucherObj);
       bool DeleteVoucher(long Id);
       List<Voucher> GetAllVoucher();
       List<Voucher> GetAllVoucherByFinancialSettingId(int FinancialSettingId);
       List<Voucher> GetAllVoucherByVoucherTypeId(int VoucherTypeId);
       List<Voucher> GetAllVoucherByBranchId(int BranchId);
       long CountByBranchIdAndPrefix(int BranchId,string PreFix);
       List<Voucher> GetAllVoucherByCurrencyId(int CurrencyId);

       long GetMaxCountByBranchId(int id);
       Voucher GetSingleVoucher(int VId);

     //  List<Voucher> GetAllVoucherByVoucherDate(DateTime VoucherDate);


    }
}
