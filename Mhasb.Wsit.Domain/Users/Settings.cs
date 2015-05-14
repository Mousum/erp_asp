using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Users
{
   public class Settings:IObjectStateLong
    {
        public long Id { get; set; }

       public int CompanyId { get; set; }
       public long userId { get; set; }

        public bool lgcompany { get; set; }
        public bool lgdash { get; set; }
        public bool lglast { get; set; }
        public int? TimezoneId { get; set; }
        


        public virtual Company Companies { get; set; }
        public virtual User Users { get; set; }

        public virtual AreaTime AreaTimes { get; set; }


        public ObjectState State
        {
            get;
            set;
        }

    }
    
}
