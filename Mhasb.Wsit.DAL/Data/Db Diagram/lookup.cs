//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mhasb.DAL.Data.Db_Diagram
{
    using System;
    using System.Collections.Generic;
    
    public partial class lookup
    {
        public lookup()
        {
            this.chartofaccounts = new HashSet<chartofaccount>();
            this.items = new HashSet<item>();
            this.items1 = new HashSet<item>();
            this.purchase_transaction_details = new HashSet<purchase_transaction_details>();
        }
    
        public int lookupid { get; set; }
        public string lookuptype { get; set; }
        public string key { get; set; }
        public decimal quantity { get; set; }
        public string value { get; set; }
        public string description { get; set; }
        public int order { get; set; }
    
        public virtual ICollection<chartofaccount> chartofaccounts { get; set; }
        public virtual ICollection<item> items { get; set; }
        public virtual ICollection<item> items1 { get; set; }
        public virtual ICollection<purchase_transaction_details> purchase_transaction_details { get; set; }
    }
}
