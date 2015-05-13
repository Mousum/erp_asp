using Mhasb.Domain.OrgSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.OrgSettings
{
    public interface ICurrency
    {
        bool AddCurrency(Currency cur);
        List<Currency> GetAllCurrency();
    }
}
