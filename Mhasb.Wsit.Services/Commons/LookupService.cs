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
    public class LookupService : ILookupService
    {
        private readonly CrudOperation<Lookup> _finalCrudOperation = new CrudOperation<Lookup>();

        public bool AddLookup(Lookup lookup) 
        {
            try
            {
                lookup.State = ObjectState.Added;
                _finalCrudOperation.AddOperation(lookup);
                return true;
            }
            catch(Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public bool UpdateLookup(Lookup lookup)
        {
            try
            {
                lookup.State = ObjectState.Modified;
                _finalCrudOperation.UpdateOperation(lookup);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public Lookup GetSingleLookup(int lookupId) 
        {
            try
            {
                //role.State = ObjectState.Unchanged;
                var labObj = _finalCrudOperation.GetOperation()
                                        .Filter(r => r.Id == lookupId)
                                        .Get().SingleOrDefault();

                //roleRep.GetSingleObject(companyId);
                return labObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
        public List<Lookup> GetAllLookups()
        {
            try
            {

                var LookupObj = _finalCrudOperation.GetOperation()
                                                   .Get().ToList();
                return LookupObj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public List<Lookup> GetLookupByType(string LookupType)
        {
            try {

                var LookupObj = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.LookupType == LookupType)
                                        .Get().ToList();
                return LookupObj;
            }catch(Exception ex){
                var msg = ex.Message;
                return null;
            }
        }
    }
}
