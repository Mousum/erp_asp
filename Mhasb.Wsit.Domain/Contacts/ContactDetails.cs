using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Contacts
{
    public class ContactDetails : IObjectStateLong
    {
        public long Id { get; set; }
        public long ContactInfoId { get; set; }
        public string Attention { get; set; } 
        public string Address { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string ZipeCode { get; set; }
        public int Country { get; set; }
        public string Type { get; set; }
        public virtual ContactInformation ContactInformations { get; set; }

        public ObjectState State
        {
            get;
            set;
        }

    }
}
