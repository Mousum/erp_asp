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
        public string Offset { get; set; }
        public string Summer { get; set; }
        public string CountryCode { get; set; }
        public string Cicode { get; set; }
        public string Cicodesummer { get; set; }
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
