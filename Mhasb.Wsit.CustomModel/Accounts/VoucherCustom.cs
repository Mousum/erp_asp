using Mhasb.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.CustomModel.Accounts
{
    public class VoucherCustom
    {
        public Voucher voucher { get; set; }

        public List<VoucherDetail> voucherDetails { get; set; }
    }
}
