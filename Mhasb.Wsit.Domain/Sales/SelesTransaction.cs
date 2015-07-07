using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Contacts;
using Mhasb.Domain.Organizations;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.Domain;
using System.ComponentModel.DataAnnotations;

namespace Mhasb.Domain.Inventories
{
    public class SelesTransaction : IObjectStateLong
    {
        public long ContactId { get; set; }
        public long EmployeeId { get; set; }
        public int? Currencyid { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public string OrderNumber { get; set; }
        public string ReferenceNo { get; set; }

        public string PoDeliveryInstruction { get; set; }//PO--> purchase Order
        public string PoAttention { get; set; }
        public string PoTelephone { get; set; }
        public string PoAddress { get; set; }
        public EnumTransactionType TransactionType { get; set; }

        public int CompanyId { get; set; }

       
        public virtual ContactInformation ContactInformations { get; set; }

        // Navigation Property for Employee
        public virtual Employee Employees { get; set; }
        public virtual Currency Currencies { get; set; }

        public virtual Company Companies { get; set; }
        public virtual ICollection<PurchaseTransactionDetail> PurchaseTransactionDetails { get; set; }
        public virtual ICollection<PurchaseTransactionDocument> PurchaseTransactionDocuments { get; set; }
        public ObjectState State { get; set; }
       [Key]
        public long Id { get; set; }
    }

    public enum EnumTransactionType
    {
        Bill=1,
        RepeatingBill=2,
        CreditNote=3,
        PurchaseOrder=4
    }
}
