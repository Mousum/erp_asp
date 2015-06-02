using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Loggers;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.Loggers
{
   public class CompanyViewLogService:ICompanyViewLog
   {
       private readonly CrudOperation<CompanyViewLog> _crudOperation = new CrudOperation<CompanyViewLog>();
        public bool AddCompanyViewLog(CompanyViewLog companyViewLog)
        {
            try
            {
                companyViewLog.State = ObjectState.Added;
                _crudOperation.AddOperation(companyViewLog);
                return true;
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                return false;
            }
        }
        public CompanyViewLog GetLogByUserAndCompanyId(long userId, int companyId)
        {
            var dbObj = _crudOperation.GetOperation()
                .Include(u=>u.Users)
                .Include(c=>c.Companies)
                .Filter(e=>e.UserId== userId && e.CompanyId==companyId)
                .Get().SingleOrDefault();
            return dbObj;
        }


        public CompanyViewLog GetLastViewCompanyByUserId(long userId)
        {
            try {
                var dbObj = _crudOperation.GetOperation()
                .Include(u => u.Users)
                .Include(c => c.Companies)
                .Filter(e => e.UserId == userId)
                .Get().Max();
                return dbObj;
            }catch(Exception ex){
                return null;
            }
        }
   }
}
