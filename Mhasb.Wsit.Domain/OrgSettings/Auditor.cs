using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.OrgSettings
{
    public class Auditor : IObjectStateInt
    {
        public int CompanyId { get; set; }
        public long EmployeeId { get; set; }
        public EnumAuditorType AuditorType { get; set; }
        public string AuditorName { get; set; }
        public string AuditorTel { get; set; }
        public string AuditorEmail { get; set; }
        public string Position { get; set; }
        public long ManagerId { get; set; }
        public int Id { get; set; }
        public ObjectState State { get; set; }

        public virtual Employee Employees { get; set; }
        public virtual Employee Managers { get; set; }
        public virtual Company Companies { get; set; }
    }


    public enum EnumAuditorType
    {
        Internal=1,
        External=2
    }
}
