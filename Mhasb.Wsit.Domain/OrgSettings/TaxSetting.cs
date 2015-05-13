using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.OrgSettings
{
   public class TaxSetting :IObjectStateInt
    {

       public string TaxBasis { get; set; }
       public string TaxId { get; set; }
       public string DisplayName { get; set; }
       public int CompanyId { get; set; }
       public EnumFinalcialPeriod FinalcialPeriod { get; set; }
       public int Id { get; set; }
       public ObjectState State { get; set; }

       public virtual Company Companies { get; set; }

    }
}
