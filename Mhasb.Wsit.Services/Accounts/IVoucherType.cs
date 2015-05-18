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
    }
}
