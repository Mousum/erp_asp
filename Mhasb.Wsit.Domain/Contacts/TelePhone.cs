using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Contacts
{
    public class TelePhone : IObjectStateLong
    {
        public long Id { get; set; }
        //public long ContactInfoId { get; set; }
        public string CellPhone { get; set; }
        public string Mobile { get; set; }
        public string DirectDial { get; set; }
        public string Skype { get; set; }
        public string WebSite { get; set; }
        public virtual ContactInformation ContactInformations { get; set; }
   
        public ObjectState State
        {
            get;
            set;
        }

    }
}
