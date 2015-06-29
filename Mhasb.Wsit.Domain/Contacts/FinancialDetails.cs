﻿using Mhasb.Domain.Commons;
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

        public int BankAccountNumber { get; set; }
        public int BankAccountName { get; set; }
        public string Details { get; set; }
        public string BillsTerms { get; set; }
        public string SalesTerms { get; set; }
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
        AmountsAreTaxInclusive=1,
        AmountsAreTaxExclusive=2,
        AmountsDontIncludeTax=3
    }

}