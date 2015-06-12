using Mhasb.Domain.Accounts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.AdminUsers
{
    public interface IAdminUserServices
    {
      bool AdminLogin(string email, string password);
    }
}
