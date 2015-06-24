using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;
using Mhasb.Domain.Commons;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Inventories
{
   public class Item:IObjectStateLong
    {
       public string ItemName { get; set; }
       public string ItemCode { get; set; }
       public int? AssetAccountId { get; set; }

       public double PurchaseUnitPrice { get; set; }
       public int? PurchasesAccountId { get; set; }
       public int? PTaxRateId { get; set; }
       public string PurchaseDescription { get; set; }

       public double SellUnitPrice { get; set; }
       public int? SalesAccountId { get; set; }
       public int? STaxRateId { get; set; }
       public string SalesDescription { get; set; }


       public virtual ChartOfAccount AssetAccount { get; set; }
       public virtual ChartOfAccount PurchasesAccount { get; set; }
       public virtual ChartOfAccount SalesAccount { get; set; }
       public virtual Lookup PTaxRate { get; set; }
       public virtual Lookup STaxRate { get; set; }
       public ObjectState State { get; set; }
       public long Id { get; set; }
    }
}
