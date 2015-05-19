using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.OrgSettings
{
   public  class Currency:IObjectStateInt
    {
       public string Name { get; set; }
       public string Symbol { get; set; }
       public int Id { get; set; }
        public ObjectState State { get; set; }

       public virtual ICollection<FinancialSetting> FinalcialSettings { get; set; }
       public virtual ICollection<FinancialSetting> SharesFinalcialSettings { get; set; }
    }
}
