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
    public class CompanyService : ICompanyService
    {
        private readonly CrudOperation<Company> companyRep = new CrudOperation<Company>();

        public bool AddCompany(Company company)
        {
            try
            {
                company.State = ObjectState.Added;
                companyRep.AddOperation(company);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public bool UpdateCompany(Company company)
        {
            try
            {
                company.State = ObjectState.Modified;
                companyRep.UpdateOperation(company);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public bool DeleteCompany(int companyId)
        {
            try
            {
                //var company = companyRep.GetSingleObject(companyId);
                //company.State = ObjectState.Deleted;
                companyRep.DeleteOperation(companyId);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public Company GetSingleCompany(int companyId)
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var comObj = companyRep.GetOperation()
                                        .Include(c => c.Countries)
                                        .Include(l => l.Languages)
                                        .Include(i => i.Industries)
                                        .Include(t => t.AreaTimes)
                                        .Include(l => l.LegalEntities)
                                        .Include(u => u.Users)
                                        .Include(cd=>cd.Documents)
                                        .Filter(c => c.Id == companyId)
                                        .Get().SingleOrDefault();

                //companyRep.GetSingleObject(companyId);
                return comObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }

        public List<Company> GetAllCompanies()
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var comObj = companyRep.GetOperation()
                                        .Include(c => c.Countries)
                                        .Include(l => l.Languages)
                                        .Include(i => i.Industries)
                                        .Include(t => t.AreaTimes)
                                        .Include(l => l.LegalEntities)
                                        .Include(u=>u.Users)
                                        .Include(cd => cd.Documents)
                                        .Get().ToList();

                //companyRep.GetSingleObject(companyId);
                return comObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }

        public int GetMaxId()
        {
            try {
                var mId = companyRep.GetOperation().Get().Max(c => c.Id);
                return mId;
            }
            catch (Exception ex)
            {
                var tt = ex.Message;
                return 0;
            }
            
        }
        public List<Company> GetAllCompaniesByUserId(long UserId)
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var comObj = companyRep.GetOperation()
                                        .Include(c => c.Countries)
                                        .Include(l => l.Languages)
                                        .Include(i => i.Industries)
                                        .Include(t => t.AreaTimes)
                                        .Include(l => l.LegalEntities)
                                        .Include(u => u.Users)
                                        .Include(cd => cd.Documents)
                                        .Filter(u=>u.Users.Id == UserId)
                                        .Get().ToList();

                //companyRep.GetSingleObject(companyId);
                return comObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }

    }
}
