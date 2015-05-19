using System;
using System.Collections.Generic;
using Mhasb.Domain.Accounts;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.OrgSettings
{
   public  class FinancialSetting:IObjectStateInt
    {

       public int FoundedYear { get; set; }

       public int CompanyId { get; set; }
       public EnumFinalcialPeriod FinalcialPeriod { get; set; }
       public int CurrencyId { get; set; }
       public double Capital { get; set; }
       public double PaidUpCapital { get; set; }
       public double SharePrice { get; set; }
       public double TotalShares { get; set; }
       public int SharesCurrencyId { get; set; }
       public bool IsActive { get; set; }
       public DateTime StartingDate { get; set; }
       public DateTime EndingDate { get; set; }
       public DateTime? PeriodLockDate { get; set; }
       public DateTime? YearLockDate { get; set; }

       public int Id { get; set; }
       public ObjectState State { get; set; }

       public virtual Company Companies { get; set; }
       public virtual Currency Currencies { get; set; }
       public virtual Currency SharesCurrencies { get; set; }
       public ICollection<Voucher> Vouchers { get; set; }
    }

   public enum EnumFinalcialPeriod
    {
        Weekly=1,
        MonMonthly=2,
        Quarterly=3,
        Biannual=4,
        Yearly=5
    }

}
