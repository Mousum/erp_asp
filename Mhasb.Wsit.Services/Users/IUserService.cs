using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Users
{
   public interface IUserService
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool UserLogin(string email,string password);
        bool CheckUserExistence(string email);
        User GetSingleUserByEmail(string email);
        List<User> GetAllUsers();
        bool isSupperAdmin(long userId, int CompanyId);
        List<Company> GetcompanyByUserID(long userId);

    }
}
