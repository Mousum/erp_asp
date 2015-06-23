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
    public class ContactDetailsService : IContactDetailsService
    {
        private readonly CrudOperation<ContactDetails> _rep = new CrudOperation<ContactDetails>();

        public bool CreateContactDetails(Domain.Contacts.ContactDetails contactdetails)
        {
            try {
               contactdetails.State=ObjectState.Added;
               _rep.AddOperation(contactdetails); 
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateContactDetails(Domain.Contacts.ContactDetails contactdetails)
        {
            try
            {
                contactdetails.State = ObjectState.Modified;
                _rep.AddOperation(contactdetails);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteContactDetails(long Id)
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

        public ContactDetails GetContactDetailsById(int id)
        {
            try
            {
                var _Obj=_rep.GetOperation()
                    .Filter(i=>i.Id==id)
                    .Get().SingleOrDefault() ;
                return _Obj;
               
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public List<ContactDetails> GetAllContactDetails()
        {
            try
            {
                var _Obj = _rep.GetOperation()
                    .Get().ToList();
                return _Obj;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}
