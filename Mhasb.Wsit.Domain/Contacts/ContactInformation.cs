﻿using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Inventories;

namespace Mhasb.Domain.Contacts
{
    public class ContactInformation : IObjectStateLong
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public string ContactName { get; set; }
        public string AccountNumber { get; set; }
        public long PostalAddId { get; set; }
        public long PhysicalAddId { get; set; }
        public EnumContactType ContactType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public long CreateBy { get; set; }
        public long UpdateBy { get; set; }
        public virtual ICollection<ContactDetails> ContactDtails{ get; set; }
        //public virtual FinancialDetails FinancialDetails { get; set; }
        public virtual ICollection<TelePhone> TelePhones { get; set; }
        public virtual ICollection<Notes> Notes { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<PurchaseTransaction> PurchaseTransactions { get; set; }
        public virtual ICollection<SelesTransaction> SelesTransactions { get; set; }


        public ObjectState State
        {
            get;
            set;
        }
    }

    public enum EnumContactType
    {
        Customer=1,
        Supplier=2,
        Employee=3,
        Archive=4,
        All = 5
    }
}
