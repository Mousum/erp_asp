using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;

namespace Mhasb.Services.Accounts
{
   public interface IBankService
   {
       bool AddBank(Bank bank);
       bool UpdateBank(Bank bank);
       bool DeleteBank(int id);
   }
}
