using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mhasb.Domain.Organizations;

namespace Mhasb.Domain.Users
{
    public class Role:IObjectStateInt
    {

        [Required(ErrorMessage = "Role Name Is Required")]
        [StringLength(100, ErrorMessage = "Must be between 3 and 100 characters", MinimumLength = 3)]
        
        public string RoleName { get; set; }
        public string Remarks { get; set; }
        public int CompanyId { get; set; }
        public virtual ICollection<RoleVsAction> RoleVsActions { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual Company Companies { get; set; }
        //[Key]
        public int Id {get;set;}

        public ObjectState State { get; set; }
    }
}
