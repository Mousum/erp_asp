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
    public class TelePhoneService : ITelePhoneService
    {
        private readonly CrudOperation<TelePhone> _rep = new CrudOperation<TelePhone>();
        public bool CreateTelePhone(TelePhone telephone)
        {
            try { 
              telephone.State=ObjectState.Added;
               _rep.AddOperation(telephone);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateTelePhone(TelePhone telephone)
        {
            try
            {
                telephone.State = ObjectState.Modified;
                _rep.AddOperation(telephone);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteTelePhone(long Id)
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

        public TelePhone GetTelePhoneById(int Id)
        {
            try
            {
                var _obj = _rep.GetOperation()
                    .Filter(i => i.Id == Id)
                    .Get().SingleOrDefault();
                return _obj;
                    
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public List<TelePhone> GetAllTelePhone()
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
    }
}
