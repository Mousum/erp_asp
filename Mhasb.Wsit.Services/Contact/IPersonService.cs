using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
   public interface IPersonService
    {
        bool CreatePersons(Person person);
        bool UpdatePersons(Person person);
        bool DeletePersons(long Id);
        List<Person> GetAllPersons();
        Person GetPersonById(long id);
        List<Person> GetAllContactsByCompany(int CompanyId);
    }
}
