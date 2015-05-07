using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mhasb.Domain.Organizations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhasb.Domain.Notifications
{
    public class Invitation : IObjectStateLong
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company  is required")]
        public int CompanyId { get; set; }
        
        [Required(ErrorMessage = "Employee Type  is required")]
        public EmpTypeEnum EmployeeType { get; set; }

        [Required(ErrorMessage = "Role Name  is required")]
        public int RoleId { get; set; }
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
