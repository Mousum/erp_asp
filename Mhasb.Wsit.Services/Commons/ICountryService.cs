using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public interface ICountryService
    {
        List<Country> GetAllCountries();
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(int Id);



    }
}
