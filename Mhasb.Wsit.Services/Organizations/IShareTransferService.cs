using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public interface IShareTransferService
    {
        bool AddShareTransferTransection(ShareTransfer st);
    }
}
