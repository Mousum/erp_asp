using Mhasb.Domain.Organizations;
using Mhasb.Wsit.CustomModel.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public interface ICompanyService
    {
        bool AddCompany(Company company);

        bool UpdateCompany(Company company);

        bool DeleteCompany(int id);

        Company GetSingleCompany(int id);

        List<Company> GetAllCompanies();
        List<Company> GetAllCompaniesByUserId(long UserId);
        int GetMaxId();
        List<Company> GetAllCompaniesByUserEmployee(long userId);
        List<LogView> GetLastVisitorWiseCompanyList(long userId);

        bool UpdateCompleteFlag(int id, int flag);
    }
}
