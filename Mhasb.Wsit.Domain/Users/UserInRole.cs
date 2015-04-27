using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Users
{
    class UserInRole : IObjectStateInt
    {

        public long EmployeeId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
        public ObjectState State { get; set; }
    }
}
