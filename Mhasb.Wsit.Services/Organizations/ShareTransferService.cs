using Mhasb.Domain.Organizations;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public class ShareTransferService : IShareTransferService
    {
        private readonly CrudOperation<ShareTransfer> stRep = new CrudOperation<ShareTransfer>();
        public bool AddShareTransferTransection(ShareTransfer st)
        {
            try
            {
                st.State = ObjectState.Added;
                stRep.AddOperation(st);
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
