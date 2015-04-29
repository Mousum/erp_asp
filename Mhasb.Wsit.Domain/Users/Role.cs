using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Users
{
    public class Role:IObjectStateInt
    {  
        public string RoleName { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<RoleVsAction> RoleVsActions { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public int Id {get;set;}

        public ObjectState State { get; set; }
    }
}
