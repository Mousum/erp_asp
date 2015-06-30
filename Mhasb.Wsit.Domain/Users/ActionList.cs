using System.Collections.Generic;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Users
{
    public class ActionList:IObjectStateInt
    {

        public string ModuleName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string NameToShow { get; set; }
        public bool IsShowInMenu { get; set; }
        public int OrderNo { get; set; }

        public virtual ICollection<RoleVsAction> RoleVsActions { get; set; }
        public int Id { get; set; }
        public ObjectState State { get; set; }
    }
}
