using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mhasb.Domain.Contacts;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;

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
        
        public virtual EmployeeProfile EmployeeProfiles { get; set; }

        public virtual ICollection<Notes> Notess { get; set; }
        public virtual ContactInformation ContactInformations { get; set; }

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

}
