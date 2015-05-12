using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Organizations
{
    public class CompanyDocument : IObjectStateLong
    {
        public long Id { get; set; }

        public int CompanyId { get; set; }
        public string DocumentLocation { get; set; }

        public string DocumentOriginalName { get; set; }

        public virtual Company companies {get; set;} 

        public ObjectState State
        {
            get;
            set;
        }
    }
}
