using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.OrgSettings
{
    public class AuditorService : IAuditor
    {



        private readonly CrudOperation<Auditor> auditorRep = new CrudOperation<Auditor>();
        public bool AddAuditor(Auditor auditor)
        {
            try
            {
                auditor.State = ObjectState.Added;
                auditorRep.AddOperation(auditor);
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
