using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhasb.Domain.Commons
{
    public class AreaTime : IObjectStateInt
    {
        [Required(ErrorMessage = "Zone Name  is required")]
        [StringLength(50)]
        [DataType(DataType.Text)]
        public string ZoneName { get; set; }
        public int Id
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
