using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
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



        public bool CreateCountry(Country country)
        {
            try
            {
                country.State = ObjectState.Added;
                countryRep.AddOperation(country);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateCountry(Country country)
        {
            try
            {
                country.State = ObjectState.Modified;
                countryRep.UpdateOperation(country);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteCountry(int Id)
        {
            try
            {
                countryRep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }
        public Country GetSingleCountry(int Id) 
        {
            try
            {
                var CountryObj = countryRep.GetOperation()
                    .Filter(c => c.Id == Id)
                    .Get().SingleOrDefault();
                return CountryObj;
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
                return null;

            }
        }
    }
}
