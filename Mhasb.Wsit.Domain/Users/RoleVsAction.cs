using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Users
{  
   public class RoleVsAction:IObjectStateInt
    {
       public int RoleId { get; set; }
       public int ActionId { get; set; }
       public bool IsActive { get; set; }

       public virtual Role Roles { get; set; }

       public virtual ActionList ActionLists { get; set; }

        public int Id { get; set; }
        public ObjectState State { get; set; }
    }
}
