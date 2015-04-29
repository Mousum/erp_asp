using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public class CountryService : ICountryService
    {
        private readonly CrudOperation<Country> countryRep = new CrudOperation<Country>();

        public List<Country> GetAllCountries()
        {
            try
            {
                var countryObj = countryRep.GetOperation()
                                        .Get().ToList();

                return countryObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }
    
    }
}
