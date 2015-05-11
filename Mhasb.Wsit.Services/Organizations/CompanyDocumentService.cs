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

        public CompanyDocument GetCompanyDocumentById(int id) 
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var conObj = companyRep.GetOperation()
                                        .Filter(c => c.Id == id)
                                        .Get().SingleOrDefault();

                //companyRep.GetSingleObject(companyId);
                return conObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public bool DeleteCompanyDocument(int id)
        {
            try
            {
                var cd = GetCompanyDocumentById(id);
                cd.State = ObjectState.Deleted;
                companyRep.DeleteOperation(id);
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
