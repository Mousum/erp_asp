using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Contacts
{
   public class Person : IObjectStateLong
    {
        public long Id { get; set; }
        public long ContactInfoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsIncludeEmail { get; set; }
        public bool IsPrimaryPerson { get; set; }
        public virtual ContactInformation ContactInformations { get; set; }
        public ObjectState State
        {
            get;
            set;
        }
        

    }
}
