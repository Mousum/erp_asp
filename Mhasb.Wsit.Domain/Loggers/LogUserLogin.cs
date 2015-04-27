using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Loggers
{
    public class LogUserLogin:IObjectStateLong
    {
        public long UserId { get; set; }
        public int CompanyId { get; set; }
        public DateTime LoginTime { get; set; }
        public string IPAddress { get; set; }
        public long Id { get; set; }
        public ObjectState State { get; set; }
    }
}
