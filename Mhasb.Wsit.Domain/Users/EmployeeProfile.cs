using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Users
{
   public class EmployeeProfile:IObjectStateLong
    {
       public long UserId { get; set; }
       public string ImageLocation { get; set; }
       public string Bio { get; set; }
       public int Location { get; set; }
       public string Phone { get; set; }
       public string Website { get; set; }
       public virtual User Users { get; set; }
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
