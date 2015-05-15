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

        public List<Auditor> GetAllAuditorsByCompanyAndType(int companyId, EnumAuditorType type)
        {
            try
            {
                var curObj = auditorRep.GetOperation()
                                        .Include(a=>a.Employees)
                                        .Include(b=>b.Employees.Designations)
                                        .Include(d=>d.Employees.Users)
                                        .Include(e=>e.Managers.Users)
                                        .Include(m=>m.Managers.Designations)
                                        .Filter(c=>c.CompanyId==companyId && c.AuditorType==type)
                                        .Get().ToList();

                return curObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

    }
}
