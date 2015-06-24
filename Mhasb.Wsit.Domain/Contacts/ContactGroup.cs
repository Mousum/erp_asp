using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Wsit.Domain;
using Mhasb.Domain.Organizations;

namespace Mhasb.Domain.Contacts
{
    public class ContactGroup : IObjectStateInt
    {
        public string GroupName { get; set; }

        public int Id { get; set; }

        public int CompanyId { get; set; }
        public ObjectState State { get; set; }

        public virtual ICollection<AssignToGroup> AssignToGroups { get; set; }

        public virtual Company Companies { get; set; }
    }
}
