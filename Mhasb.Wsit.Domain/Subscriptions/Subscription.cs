using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Subscriptions
{
   public class Subscription:IObjectStateLong
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public int PackageId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
       public DateTime SuspendDate { get; set; }

       public ObjectState State { get; set; }

    }
}
