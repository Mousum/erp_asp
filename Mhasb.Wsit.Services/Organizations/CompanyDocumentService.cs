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
    public class CompanyDocumentService : ICompanyDocument
    {
        private readonly CrudOperation<CompanyDocument> companyRep = new CrudOperation<CompanyDocument>();
        public bool AddCompanyDocument(CompanyDocument cd)
        {
            try
            {
                cd.State = ObjectState.Added;
                companyRep.AddOperation(cd);
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
