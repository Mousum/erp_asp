using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Notifications
{
   public class Invitation:IObjectStateLong
    {
        public string Email { get; set; }
        public int   CompanyId { get; set; }

        public EmpTypeEnum EmployeeType { get; set; }
        public string Token { get; set; }
        public StatusEnum Status { get; set; }

        public DateTime SendDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public long Id { get; set; }

        public ObjectState State { get; set; }
        
    }
   public enum EmpTypeEnum
   {
       EmployeeType = 1,
       Owner = 2,
       test3 = 3,
       test4 = 4
   }
   public enum StatusEnum
   {
       test1 = 1,
       test2 = 2,
       test3 = 3,
       test4 = 4
   }

}
 