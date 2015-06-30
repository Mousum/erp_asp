using Mhasb.Domain.Commons;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;

namespace Mhasb.Domain.Contacts
{
    public class FinancialDetails : IObjectStateLong
    {
        public long Id { get;set;}
        public long ContactInfoId { get; set; }
        public EnumDefaultTax SalesDefaultTax { get; set; }
        public int? SalesCoaId { get; set; }
        public EnumDefaultTax PurchasesDefaultTax { get; set; }
        public int? PurchasesCoaId { get; set; }

        public string TaxNumber { get; set; }
        public int? SalesTax { get; set; }
        public int? PurchasesTax { get; set; }
        public int? CurrencyId { get; set; }

        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string Details { get; set; }
        public DueOption BillsTerms { get; set; }
        public DueOption SalesTerms { get; set; }
        public double BillsDue { get; set; }
        public double SakesDue { get; set; }
        public string NetworkKey { get; set; } 
        public virtual ContactInformation ContactInformations { get; set; }

        public virtual FinancialSetting FinancialSettings { get; set; }

        public virtual Lookup SaleLookups { get; set; }
        public virtual Lookup PurchaseLookups { get; set; }
        public virtual Currency Currencies { get; set; }
        public virtual ChartOfAccount PurchasesAccounts { get; set; }
        public virtual ChartOfAccount SalesAccounts { get; set; }
        public ObjectState State
        {
            get;
            set;
        }


    }

    public enum EnumDefaultTax
    {
        None=0,
        AmountsAreTaxInclusive=1,
        AmountsAreTaxExclusive=2,
        AmountsDontIncludeTax=3
    }

    public enum DueOption
    {
        OfTheFollowingMonth = 0,
        DaysAfterTheBillDate = 1,
        DaysAfterTheEndOfTheBillMonth= 2,
        OfTheCurrentMonth = 3
    }

}
