using System;
using System.Collections.Generic;
using System.Linq;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.Organizations
{
   public class FounderService:IFounder
    {
       private readonly CrudOperation<Founder> _crudOperation = new CrudOperation<Founder>();
        public bool AddFounder(Founder founder)
        {
            try
            {
                founder.State = ObjectState.Added;
                _crudOperation.AddOperation(founder);
                return true;
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                return false;
            }
        }

        public bool UpdateFounder(Founder founder)
        {
            try
            {
                var dbObj = _crudOperation.GetSingleObject(founder.Id);
                dbObj.FounderName = founder.FounderName;
                dbObj.Email = founder.Email;
                dbObj.Fax = founder.Fax;
                dbObj.SharesOwned = founder.SharesOwned;
                dbObj.TotalSharesValue = founder.TotalSharesValue;
                dbObj.FounderResidence = founder.FounderResidence;
                
                dbObj.State = ObjectState.Modified;
                _crudOperation.UpdateOperation(dbObj);
                return true;

            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                return false;
            }
        }

        public List<Founder> GetFounders(int companyId)
        {
            var objList = _crudOperation.GetOperation()
                .Include(d => d.Companies)
                .Include(f => f.Countries)
                .Filter(f => f.CompanyId == companyId)
                .Get().ToList();
            return objList;
        }
        public Founder GetSingleFounder(int FounderId) 
        {
            var obj = _crudOperation.GetOperation()
                .Include(d => d.Companies)
                .Include(f => f.Countries)
                .Filter(f => f.Id == FounderId)
                .Get().SingleOrDefault();
            return obj;

        }
    }
}
