using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Accounts
{
    public class VoucherType : IObjectStateInt
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name Field  is required")]
        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public ObjectState State { get; set; }
    }
}
