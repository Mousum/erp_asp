using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Users
{
   public interface IRoleService
    {
       bool AddRole(Role role);
       bool EditRole(Role role);
       bool DeleteRole(int RoleId);
       Role GetSingleRole(int RoleId);
       List<Role> GetAllRoles();
       List<Role> GetAllRolesByCompany(int companyId);
    }
}
