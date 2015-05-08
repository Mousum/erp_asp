using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mhasb.Domain.Commons;

namespace Mhasb.Domain.Organizations
{
    public class CompanyProfile : IObjectStateInt
    {
        public int Id { get; set; }


        public long UserId { get; set; }

        [StringLength(100)]
        public string BusinessName { get; set; }

        [StringLength(1000)]
        public string Vision { get; set; }

        public int TurnOver { get; set; }

        [StringLength(1000)]
        public string Objectives { get; set; }

        [StringLength(1000)]
        public string Mission { get; set; }

        [StringLength(1000)]
        public string Experties { get; set; }

        [StringLength(1000)]
        public string Activities { get; set; }

        [StringLength(1000)]
        public string Markets { get; set; }

        [StringLength(1000)]
        public string PreviousWork { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(200)]
        public string ImageLocation { get; set; }

        public int Location { get; set; }

        //[StringLength(50)]
        //public string Phone { get; set; }

        //[StringLength(50)]
        //public string Website { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public ObjectState State { get; set; }

        public virtual Company Companies { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }

    }
}
