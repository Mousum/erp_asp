using Mhasb.Domain.Users;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Contacts
{
    public class Notes : IObjectStateLong
    {
        public long Id { get; set; }
        public long ContactInfoId { get; set; }
        public long UserId { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public virtual ContactInformation ContactInformations { get; set; }
        public virtual User Users { get; set; }
        public ObjectState State
        {
            get;
            set;
        }
    }
}
