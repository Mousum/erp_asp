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
    public class AssignToGroupService:IAssignToGroupService
    {
        private readonly CrudOperation<AssignToGroup> _finalCrud = new CrudOperation<AssignToGroup>();

        public bool CreateAssignToGroup(AssignToGroup assignToGroup) 
        {
            try {
                assignToGroup.State = ObjectState.Added;
                _finalCrud.AddOperation(assignToGroup); 
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }
        public List<AssignToGroup> GetAllContactsByGroupId(int groupId) 
        {
            try
            {
                var _Obj = _finalCrud.GetOperation()
                    .Filter(i => i.ContactGroupId == groupId)
                    .Include(i=>i.ContactInformations)
                    .Include(i=>i.ContactGroups)
                    .Include(i=>i.ContactInformations.Persons)
                    .Include(i=>i.ContactInformations.TelePhones)
                    .Get().ToList();
                return _Obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
        public bool DeleteAssignToGroup(int Id) 
        {
            try
            {

                _finalCrud.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }
        public AssignToGroup GetSingleAssignToGroup(int GroupId,int InfoId) {
            try
            {
                var _Obj = _finalCrud.GetOperation()

                    .Filter(i => i.ContactGroupId == GroupId&&i.ContactInfoId==InfoId)
                    
                    .Get().FirstOrDefault();
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
