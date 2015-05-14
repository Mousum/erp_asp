using Mhasb.Domain.OrgSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.OrgSettings
{
    public interface IAuditor
    {
        bool AddAuditor(Auditor auditor);
    }
}
