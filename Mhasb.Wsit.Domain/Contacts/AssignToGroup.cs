using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Contacts
{
    public class AssignToGroup:IObjectStateLong
    {
        public long? ContactInfoId { get; set; }
        public int ContactGroupId { get; set; }
        public DateTime CreatedTime { get; set; }
        public long Id { get; set; }

        public ObjectState State { get; set; }

        public virtual ContactGroup ContactGroups { get; set; }
        public virtual ContactInformation ContactInformations { get; set; }


    }
}
