using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Mhasb.Domain.Organizations
{
    public class ShareTransfer : IObjectStateInt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int FromFounderId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int ToFounderId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int TransferAmount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public double UnitShareValue { get; set; }

        public DateTime TransferTime { get; set; }


        public virtual Founder FromFonder { get; set; }

        public virtual Founder ToFonder { get; set; }

        public ObjectState State
        { get; set; }
    }
}
