using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Commons;
using Mhasb.Wsit.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mhasb.Domain.Organizations
{
   public  class Founder: IObjectStateInt
    {
       public string FounderName { get; set; }
       public string FounderResidence { get; set; }
       public string Tel { get; set; }
       public string Fax { get; set; }
       public string PoBoax { get; set; }
       public string Email { get; set; }
       public int SharesOwned { get; set; }
       public double TotalSharesValue { get; set; }
       [Required(ErrorMessage = "Founders Nationality Is Resquired")]
       public int CountryId { get; set; }
       [Required(ErrorMessage = "Founders Language Is Resquired")]
       public int LanguageId { get; set; }
       public int CompanyId { get; set; }
       public int Id { get; set; }
       public ObjectState State { get; set; }

       public virtual Country Countries { get; set; }
       public virtual Company Companies { get; set; }
       public virtual Language Languages { get; set; }
    }
}
