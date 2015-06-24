using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
   public interface IContactGroupService
    {
       bool CreateContactGroup(ContactGroup contactGroup);
       bool DeleteContactGroup(int groupId);
    }
}
