using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Users
{
    public interface IRoleVsActionService
    {
        bool AddRoleVsAction(RoleVsAction rolevsaction);
        bool UpdateRoleVsAction(RoleVsAction rolevsaction);

        bool DeleteRoleVsAction(int Id);

        RoleVsAction GetSingleRoleVsAction(int Id);

        //List<RoleVsAction> GetActionByRoleID(int Id);
        List<RoleVsAction> GetAllRoleVsAction();


    }
}
