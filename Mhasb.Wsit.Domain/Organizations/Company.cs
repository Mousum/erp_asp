using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mhasb.Domain.Users;
using Mhasb.Domain.Commons;
using Mhasb.Domain.OrgSettings;

namespace Mhasb.Domain.Organizations
{
    public class Company : IObjectStateInt
    {
        public int Id { get; set; }

        //public long UserId { get; set; }

        [Required(ErrorMessage = "Trading Name is required")]
        [StringLength(100)]
        public string TradingName { get; set; }

        [StringLength(100)]
        public string LegalName { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Industry is required")]
        public int IndustryId { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public int LanguageId { get; set; }

        [StringLength(50)]
        public string Tel { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }
        public int? P_O_Box { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200)]
        public string Location { get; set; }

        [StringLength(50)]
        public string Website { get; set; }

        public int? LegalEntityId { get; set; }

        public int? TimezoneId { get; set; }

        public string LogoLocation { get; set; }

        public string SealLocation { get; set; }

        //public string DocumentLocation { get; set; }

        public virtual User Users { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual Country Countries { get; set; }
        public virtual Language Languages { get; set; }
        public virtual Industry Industries { get; set; }

        public virtual AreaTime AreaTimes { get; set; }
        public virtual LegalEntity LegalEntities { get; set; }
        public virtual CompanyProfile CompanyProfiles { get; set; }

        public virtual ICollection<CompanyDocument> Documents { get; set; }
        public virtual ICollection<FinancialSetting> FinalcialSettings { get; set; }
        public virtual ICollection<TaxSetting> TaxSettings { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }


        public ObjectState State { get; set; }

    }
}