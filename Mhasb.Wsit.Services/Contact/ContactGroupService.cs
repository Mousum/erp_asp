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
    public class ContactGroupService : IContactGroupService
    {
        private readonly CrudOperation<ContactGroup> _finalCrud = new CrudOperation<ContactGroup>();

        public bool CreateContactGroup(ContactGroup contactGroup)
        {
            try
            {
                contactGroup.State = ObjectState.Added;
                _finalCrud.AddOperation(contactGroup);
                return true;
            }
            catch (Exception e) 
            {
                var msg = e.Message;
                return false;
            }
        }
        public bool DeleteContactGroup(int groupId) 
        {
            try
            {
                _finalCrud.DeleteOperation(groupId);
                return true;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return false;
            }
        }
        public List<ContactGroup> GetAllGroupsByCompanyId(int companyId) 
        {
            try
            {
                var _Obj = _finalCrud.GetOperation()
                    .Filter(g => g.CompanyId == companyId)
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
