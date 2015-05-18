using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public interface ILookupService
    {
        List<Lookup> GetLookupByType(string LookupType);
    }
}
