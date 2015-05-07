using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Organizations
{
   public class Project :IObjectStateLong
    {
       public string ProjectName { get; set; }
       public long ManagerId { get; set; }

       public DateTime ProjectDate { get; set; }
       public DateTime? StartingDate { get; set; }
       public DateTime? FinishingDate { get; set; }
       public int NumberOfMember { get; set; }
       public int TaskNumber { get; set; }

       public EnumStatus Status { get; set; }
       public virtual Employee Employees { get; set; }
       public virtual ICollection<TaskManager> TaskManagers { get; set; }
      
       public long Id { get; set; }

       public ObjectState State { get; set; }
    }
}
