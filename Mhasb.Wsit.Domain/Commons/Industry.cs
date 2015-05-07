using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhasb.Domain.Commons
{
    public class Industry : IObjectStateInt
    {
        [Required(ErrorMessage = "Industry Name is required")]
        public string IndustryName { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public int Id { get; set; }
        public ObjectState State { get; set; }
    }
}
