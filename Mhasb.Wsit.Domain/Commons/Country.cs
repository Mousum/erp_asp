using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Commons
{
    public class Country : IObjectStateInt
    {
        [Required(ErrorMessage = "Country Name is required")]
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string ShortName { get; set; }
        public string ShortName1 { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public int Id { get; set; }
        public ObjectState State { get; set; }

    }
}
