using Mhasb.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.CustomModel.Accounts
{
    public class ChartOfAccountCustom
    {
        public ChartOfAccount COA { get; set; }
        public string Type { get; set; }
        public string Tax { get; set; }
    }
}
