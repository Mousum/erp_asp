using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;

namespace Mhasb.Domain.Loggers
{
    public class CompanyViewLog:IObjectStateLong
    {

        public long UserId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime LoginTime { get; set; }
        public string IpAddress { get; set; }
        public long Id { get; set; }
        public ObjectState State { get; set; }
        
        public virtual User Users { get; set; }

        public virtual Company Companies { get; set; }

    }
}
