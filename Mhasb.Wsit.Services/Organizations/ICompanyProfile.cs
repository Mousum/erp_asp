using Mhasb.Domain.Organizations;
using Mhasb.Wsit.CustomModel.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public interface ICompanyProfile
    {
        bool AddCompanyProfile(CompanyProfile cp);
        bool UpdateCompanyProfile(CompanyProfile cp);
        CompanyProfileCustom GetCompanyProfile(long companyId);
    }
}
