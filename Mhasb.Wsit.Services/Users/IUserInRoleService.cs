using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Users
{
    public interface IUserInRoleService
    {
        bool AddUserInRole(UserInRole userInRole);
        bool UpdateUserInRole(UserInRole userInRole);
        bool DeleteUserInRole(int userInRoleId);
        UserInRole GetSingleUserInRole(int userInRoleId);
        List<UserInRole> GetAllUserInRole();
    }
}
