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
    public class VoucherTypeService : IVoucherType
    {

        private readonly CrudOperation<VoucherType> voucherRep = new CrudOperation<VoucherType>();
        public bool AddVoucherType(VoucherType voucherType)
        {
            try
            {
                voucherType.State = ObjectState.Added;
                voucherRep.AddOperation(voucherType);
                voucherType.State = ObjectState.Unchanged;
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
