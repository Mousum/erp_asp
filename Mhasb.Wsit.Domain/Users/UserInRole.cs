using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Users
{
    public class UserInRole : IObjectStateInt
    {


        public long EmployeeId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }

        public virtual Role Roles { get; set; }

        public virtual User Users { get; set; }

        public virtual Company Companies { get; set; }

        public ObjectState State { get; set; }
    }
}
