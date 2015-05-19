using System.ComponentModel.DataAnnotations;
using Mhasb.Wsit.Domain;

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
