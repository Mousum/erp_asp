using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;

namespace Mhasb.Domain.OrgSettings
{
   public  class FinalcialSetting:IObjectStateInt
    {

       public int FoundedYear { get; set; }

       public int CompanyId { get; set; }
       public int PeriodId { get; set; }
       public int CurrencyId { get; set; }
       public double Capital { get; set; }
       public double PaidUpCapital { get; set; }
       public double SharePrice { get; set; }
       public double TotalShares { get; set; }
       public int SharesCurrencyId { get; set; }
       public bool IsActive { get; set; }
       public DateTime StartingDate { get; set; }
       public DateTime? EndingDate { get; set; }
       public DateTime? PeriodLockDate { get; set; }
       public DateTime? YearLockDate { get; set; }

       public int Id { get; set; }
       public ObjectState State { get; set; }

       public virtual Company Companies { get; set; }
       public virtual Currency Currencies { get; set; }
       public virtual Currency SharesCurrencies { get; set; }
       public virtual FinalcialPeriod FinalcialPeriods { get; set; }


    }
}
