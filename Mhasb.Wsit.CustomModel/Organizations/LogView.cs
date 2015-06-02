using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.CustomModel.Organizations
{
    public class LogView
    {
       public int CompanyId { get; set; }
       public long UserId { get; set; }
       public string CompanyName { get; set; }
       public DateTime LastViewedTime { get; set; }
       public string LastViewerFirstName { get; set; }
       public string LastViewerLastName { get; set; }
       public string RoleName { get; set; }
       public string RoleDescription { get; set; }
       public string Subscriper { get; set; }
       public string SubscriptionType { get; set; }

    }
}
