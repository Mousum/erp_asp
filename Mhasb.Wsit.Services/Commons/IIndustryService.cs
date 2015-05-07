using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public interface IIndustryService
    {
        List<Industry> GetAllIndustries();
        bool CreateIndustry(Industry industry);
        bool UpdateIndustry(Industry industry);
        bool DeleteIndustry(int Id);
        Industry GetSingleIndustry(int Id);

        

    }
}
