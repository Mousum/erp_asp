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
using Mhasb.Domain.Accounts;


namespace Mhasb.Domain.Users
{
    public class User : IObjectStateLong
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        [ScaffoldColumn(false)]
        public string Status { get; set; }
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


public class MustNotExist: ValidationAttribute
{
    
}
}
