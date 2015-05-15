using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.OrgSettings
{
    class ChartOfAccountService : IChartOfAccountService
    {
        private readonly CrudOperation<ChartOfAccount> _finalCrudOperation = new CrudOperation<ChartOfAccount>();


    }
}
