using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.CustomModel.Contact
{
    public class ContactCustome
    {
        public ContactInformation ContactInformation { get; set; }
        public ContactDetails PostalAddress { get; set; }
        public ContactDetails PhysicalAddress { get; set; }
        public Person PrimaryPerson { get; set; }
        public List<Person> Person { get; set; }
        public FinancialDetails FinancialDetails { get; set; }
        public Notes Notes { get; set; }
        public TelePhone TelePhone { get; set; }


    }
}
