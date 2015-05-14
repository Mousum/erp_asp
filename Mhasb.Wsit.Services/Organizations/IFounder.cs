using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Organizations;

namespace Mhasb.Services.Organizations
{
    public interface IFounder
    {
        bool AddFounder(Founder founder);
        bool UpdateFounder(Founder founder);
        List<Founder> GetFounders(int companyId);
        Founder GetSingleFounder(int FounderId);
    }
}
