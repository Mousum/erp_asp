using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public interface ICompanyDocument
    {
        bool AddCompanyDocument(CompanyDocument cd);
        bool DeleteCompanyDocument(int id);
        CompanyDocument GetCompanyDocumentById(int id);

    }
}
