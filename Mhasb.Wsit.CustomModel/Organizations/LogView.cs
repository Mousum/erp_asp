using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.CustomModel.Organizations
{
    public class LogView
    {
        int companyId { get; set; }
        long userId { get; set; }
        string companyName { get; set; }
        DateTime lastViewedTime { get; set; }
        string lastViewerFirstName { get; set; }
        string lastViewerLastName { get; set; }
        string roleName { get; set; }
        string roleDescription { get; set; }
        string subscriper { get; set; }
        string subscriptionType { get; set; }

    }
}
