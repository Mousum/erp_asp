using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;

namespace Mhasb.Services.Accounts
{
   public interface IVoucherDocument
    {
        bool AddDocument(VoucherDocument voucherDocument);
        List<VoucherDocument> GetDocumentByVoucherId(long voucherId);
    }
}
