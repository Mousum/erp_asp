using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Loggers;

namespace Mhasb.Services.Loggers
{
   public interface ICompanyViewLog
   {
       bool AddCompanyViewLog(CompanyViewLog companyViewLog);
       CompanyViewLog GetLogByUserAndCompanyId(long userId,int companyId);

   }
}
