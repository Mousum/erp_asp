using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
    public interface IFinancialDetailsService
    {
        bool CreateFinancialDetails(FinancialDetails financialdetails);
        bool UpdateFinancialDetails(FinancialDetails financialdetails);
        bool DeleteFinancialDetails(long Id);
        FinancialDetails GetFinancialDetailsById(long Id);
        List<FinancialDetails> GetAllFinancialDetails();
      

    }
}
