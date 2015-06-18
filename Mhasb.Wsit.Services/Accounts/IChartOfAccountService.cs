using Mhasb.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Wsit.CustomModel.Accounts;

namespace Mhasb.Services.Accounts
{
    public interface IChartOfAccountService
    {
        bool AddChartOfAccount(ChartOfAccount chartOfAccount);
        bool UpdateChartOfAccount(ChartOfAccount chartOfAccount);
        bool DeleteChartOfAccount(int CaId);
        List<ChartOfAccount> GetAllChartOfAccount();
        ChartOfAccount GetSingleChartOfAccount(int caId);
        List<ChartOfAccount> GetAllChartOfAccountByCompanyId(int CompanyId);
        List<ChartOfAccount> GetAllChartOfAccountByComIdCostCentre(int CompanyId);
        List<ChartOfAccount> GetAllChartOfAccountByCompanyIdForSecondLevel(int CompanyId);
        List<ChartOfAccountCustom> CodeWiseGetAllChartOfAccountByCompanyIdForLastLevel(int CompanyId, int code);
        string GeneratedCode(string pCode, int level);
        List<TreeViewNode> TreeViewList(string pcode, int level);
        bool AddBaseAccountTypes();
    }
}
