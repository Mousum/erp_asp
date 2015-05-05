using Mhasb.Domain.Commons;
using Mhasb.Domain.Users;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Organizations
{
   public class EmployeeProfile:IObjectStateLong
    {
       public string ImageLocation { get; set; }
       public string JobTitle { get; set; }
       public string Bio { get; set; }
       public int Location { get; set; }
       public string Phone { get; set; }
       public string Website { get; set; }
       public bool IsAction { get; set; }
       public virtual User Users { get; set; }
       public virtual ICollection<ContactDetail> ContactDetails { get; set; }
       public long Id
       {
           get;
           set;
       }

       public ObjectState State
       {
           get;
           set;
       }
    }
}
