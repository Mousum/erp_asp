using Mhasb.Domain.Accounts;
using Mhasb.Domain.AdminUsers;
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
        bool AddAdminUser(AdminUser admin);
        bool UpdateAdminUser(AdminUser admin);
        bool DeleteAdminUser(int adminUserId);
        List<AdminUser> GetAllAdminUsers();
        AdminUser GetSingleAdminUser(int userId);
        bool InsertDefaultData();
    }
}
