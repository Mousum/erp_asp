using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
   public interface IAssignToGroupService
    {
        bool CreateAssignToGroup(AssignToGroup assignToGroup);
        List<AssignToGroup> GetAllContactsByGroupId(int groupId);
    }
}
