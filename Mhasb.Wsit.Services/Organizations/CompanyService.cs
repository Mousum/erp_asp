using Mhasb.Domain.Organizations;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Wsit.CustomModel.Organizations;
using Mhasb.Wsit.DAL.Data;

namespace Mhasb.Services.Organizations
{
    public class CompanyService : ICompanyService
    {
        private readonly CrudOperation<Company> companyRep = new CrudOperation<Company>();
        private readonly CrudOperation<Employee> empCrudOperation = new CrudOperation<Employee>();


        public bool AddCompany(Company company)
        {
            try
            {
                company.CompleteFlag = 0;
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
                                        .Filter(c => c.Id == companyId )
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

        // Owner wise get all Company 
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
        public List<Company> GetAllCompaniesByUserEmployee(long userId)
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var comObj1 = companyRep.GetOperation()
                    .Include(c => c.Countries)
                    .Include(l => l.Languages)
                    .Include(i => i.Industries)
                    .Include(t => t.AreaTimes)
                    .Include(l => l.LegalEntities)
                    .Include(u => u.Users)
                    .Include(cd => cd.Documents)
                    .Filter(e=>e.Employees.Any(r=>r.UserId==userId))
                    .Get().ToList();
                    //.Where(e => e.Employees.All(r => r.UserId == userId)).ToList();

                var comObj2 = companyRep.GetOperation()
                                        .Include(c => c.Countries)
                                        .Include(l => l.Languages)
                                        .Include(i => i.Industries)
                                        .Include(t => t.AreaTimes)
                                        .Include(l => l.LegalEntities)
                                        .Include(u => u.Users)
                                        .Include(cd => cd.Documents)
                                        .Filter(u => u.Users.Id == userId)
                                        .Get().ToList();
                var comObj = comObj1.Union(comObj2).ToList();

                //companyRep.GetSingleObject(companyId);
                return comObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }
        

        public List<LogView> GetLastVisitorWiseCompanyList(long userId)
        {
            using (var context = new WsDbContext())
            {
                var param1 = new SqlParameter("@queryoption", 1);
                var param2 = new SqlParameter("@userid", userId);
                var param3 = new SqlParameter("@companyid", 1);

                const string query = "EXEC spget_lastvisitorwisecompanylist @queryoption,@userid,@companyid";
                var rr = context.Database.SqlQuery<LogView>(query, param1,param2,param3).ToList();

                return rr;
            }
        }



        public bool UpdateCompleteFlag(int id, int flag)
        {
            try {
                var dbObj = companyRep.GetSingleObject(id);
                dbObj.CompleteFlag = flag;
                dbObj.State = ObjectState.Modified;
                companyRep.UpdateOperation(dbObj);
                return true;
            }catch(Exception ex){
                return false;
            }
        }
    }
}
