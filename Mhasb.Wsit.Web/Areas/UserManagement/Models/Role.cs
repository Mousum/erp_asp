using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Models
{
    public class RolesModel
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role Name Is Required")]
        [StringLength(100, ErrorMessage = "Must be between 3 and 100 characters", MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string RoleName { get; set; }

        [DataType(DataType.Text)]
        public string Remarks { get; set; }
    }
}