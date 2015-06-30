using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;

namespace Mhasb.Services.OrgSettings
{
    public interface IFinalcialSetting
    {
        bool AddFinalcialSetting(FinancialSetting finalcialSetting);
        bool UpdateFinalcialSetting(FinancialSetting finalcialSetting);
        FinancialSetting GetFinalcialSetting(int finalcialSettingId);

        FinancialSetting GetCurrentFinalcialSettingByComapny(int companyId);


    }
}
