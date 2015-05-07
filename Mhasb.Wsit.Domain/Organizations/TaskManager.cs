using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Organizations
{
   public class TaskManager:IObjectStateLong
    {

       public long TaskTo { get; set; }
       public long ProjectId { get; set; }
       public string Tite { get; set; }
       public DateTime TaskDate { get; set; }
       public DateTime StartingDate { get; set; }
       public DateTime FinishingDate { get; set; }
       public EnumStatus ProjectStatus { get; set; }

       public virtual Project Projects { get; set; }
       public virtual Employee Employees { get; set; }
        public long Id { get; set; }
        public ObjectState State { get; set; }
    }

   
}
