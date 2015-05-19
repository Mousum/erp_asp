using Mhasb.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Accounts
{
    public interface IVoucherType
    {
        bool AddVoucherType(VoucherType voucherType);
        bool UpdateVoucherType(VoucherType voucherType);
        List<VoucherType> GetAllVoucherType();
        VoucherType GetVoucherTypeById(int id);
        bool DeleteVoucherTypeById(int id);
    }
}
