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
    public class LegalEntity : IObjectStateInt
    {
        [Required(ErrorMessage = "Industry Name is required")]
        public string LegalEntityName { get; set; }
        public int Id { get; set; }
        public ObjectState State { get; set; }
        
    }
}
