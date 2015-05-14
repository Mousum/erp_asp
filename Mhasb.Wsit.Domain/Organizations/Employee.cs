using Mhasb.Domain.Users;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Organizations
{
    public class Employee : IObjectStateLong
    {
        public long UserId { get; set; }
        public int CompanyId { get; set; }
        public int DesignationId { get; set; }
        public int BranchId { get; set; }
        public string SignatureLocation { get; set; }

        //public virtual 
        public virtual User Users { get; set; }
        public virtual Company Companies { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<TaskManager> TaskManagers { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual Designation Designations { get; set; }
        public long Id { get; set; }

        public ObjectState State { get; set; }
    }
}