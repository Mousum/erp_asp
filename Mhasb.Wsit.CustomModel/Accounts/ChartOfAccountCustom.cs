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
        public int Id { get; set; }
        public int? TaxId { get; set; }

        public string ACode { get; set; }
        public string AName { get; set; }
        public string Description { get; set; }

        public bool ShowInDashboard { get; set; }
        public bool ShowInExpenseClaims { get; set; }
        public bool IsCostCenter { get; set; }
        public int Level { get; set; }
        public string AccountType { get; set; }
        public string Tax { get; set; }
    }
}
