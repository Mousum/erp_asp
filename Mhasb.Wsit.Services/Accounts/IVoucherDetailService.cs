using Mhasb.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Accounts
{
    public interface IVoucherDetailService
    {
        bool CreateVoucherDetail(VoucherDetail voucherdetail);
        bool UpdateVoucherDetail(VoucherDetail voucherdetail);
        bool DeleteVoucherDetail(long Id);
        List<VoucherDetail> GetAllVoucherDetail();
        List<VoucherDetail> GetAllVoucherDetailById(long Id);
        List<VoucherDetail> GetAllVoucherDetailByVoucherId(int VoucherId);
        List<VoucherDetail> GetAllVoucherDetailByCoaId(int CoaId);
    }
}
