using Mhasb.Domain.Organizations;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public class EmployeeProfileService : IEmployeeProfile
    {
        private readonly CrudOperation<EmployeeProfile> epRep = new CrudOperation<EmployeeProfile>();
        public bool AddEmployeeProfile(EmployeeProfile ep)
        {
            try
            {
                ep.State = ObjectState.Added;
                
                epRep.AddOperation(ep);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public bool UpdateEmployeeProfile(EmployeeProfile ep)
        {
            try
            {
                ep.State = ObjectState.Modified;
                epRep.UpdateOperation(ep);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }


    }
}
