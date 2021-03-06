﻿using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Inventories;

namespace Mhasb.Domain.Accounts
{
    public class ChartOfAccount : IObjectStateInt
    {

        [Key]
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? TaxId { get; set; }

        public string ACode { get; set; }
        public string AName { get; set; }
        public string Description { get; set; }

        public bool ShowInDashboard { get; set; }
        public bool ShowInExpenseClaims { get; set; }
        public bool IsCostCenter { get; set; }
        public int Level { get; set; }
        public ObjectState State { get; set; }
        public virtual Company Companies { get; set; }
        public virtual Lookup Lookups { get; set; }
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
        public virtual ICollection<PurchaseTransactionDetail> PurchaseTransactionDetails { get; set; }




    }
}
