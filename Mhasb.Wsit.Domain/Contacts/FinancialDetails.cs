using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Contacts
{
    public class FinancialDetails : IObjectStateLong
    {
        public long Id { get;set;}
        public long ContactInfoId { get; set; }
        public string SalesDefaultTax { get; set; }
        public string SalesDefaultAccount { get; set; }
        public string PurchasesDefaultTax { get; set; }
        public string PurchasesDefaultAccount { get; set; }
        public string TaxNumber { get; set; }
        public string SalesTax { get; set; }
        public string PurchasesTax { get; set; }
        public string DefaultCurrency { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string Details { get; set; }
        public string BillsTerms { get; set; }
        public string SalesTerms { get; set; }
        public string NetworkKey { get; set; }
        public virtual ContactInformation ContactInformations { get; set; }
        public ObjectState State
        {
            get;
            set;
        }


    }
}
