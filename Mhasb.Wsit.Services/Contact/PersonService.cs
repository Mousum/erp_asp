using Mhasb.Domain.Contacts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
    public class PersonService : IPersonService
    {
        private readonly CrudOperation<Person> _rep = new CrudOperation<Person>();
        public bool CreatePersons(Person person)
        {
            try {
                person.State = ObjectState.Added;
                _rep.AddOperation(person);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdatePersons(Person person)
        {
            try
            {
                person.State = ObjectState.Modified;
                _rep.UpdateOperation(person);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeletePersons(long Id)
        {
            try
            {
                _rep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public List<Person> GetAllPersons()
        {
            try
            {
                var _obj = _rep.GetOperation()
                    .Get().ToList();
                return _obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public Person GetPersonById(long Id)
        {
            try
            {
                var _obj = _rep.GetOperation()
                    .Filter(i=>i.Id==Id)
                    .Get().SingleOrDefault();
                return _obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
        public List<Person> GetAllContactsByCompany(int CompanyId) 
        {
            try
            {
                var _obj = _rep.GetOperation()
                    .Filter(c=>c.ContactInformations.CompanyId==CompanyId)
                    .Include(c=>c.ContactInformations)
                    .Include(c=>c.ContactInformations.TelePhones)
                    .Get().ToList();
                return _obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}
