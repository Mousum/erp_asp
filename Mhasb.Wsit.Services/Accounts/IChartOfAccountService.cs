using Mhasb.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Accounts
{
    public interface IChartOfAccountService
    {
        bool AddChartOfAccount(ChartOfAccount chartOfAccount);
        bool UpdateChartOfAccount(ChartOfAccount chartOfAccount);
        bool DeleteChartOfAccount(int CaId);
        List<ChartOfAccount> GetAllChartOfAccount();
        ChartOfAccount GetSingleChartOfAccount(int caId);
    }
}
