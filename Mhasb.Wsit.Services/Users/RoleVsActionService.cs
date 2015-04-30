using Mhasb.Domain.Users;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Users
{
    class RoleVsActionService : IRoleVsActionService
    {
        private readonly CrudOperation<RoleVsAction> rVcRep = new CrudOperation<RoleVsAction>();

        public bool AddRoleVsAction(RoleVsAction rolevsaction)
        {
            try
            {
                rolevsaction.State = ObjectState.Added;
                rVcRep.AddOperation(rolevsaction);
                return true;

            }
            catch (Exception ex)
            {
                var rvc = ex.Message;
                return false;
            }
        }

        public bool UpdateRoleVsAction(Domain.Users.RoleVsAction rolevsaction)
        {
            try
            {
                rolevsaction.State = ObjectState.Added;
                rVcRep.AddOperation(rolevsaction);
                return true;

            }
            catch (Exception ex)
            {
                var rvc = ex.Message;
                return false;
            }
        }

        public bool DeleteRoleVsAction(int Id)
        {

            try
            {
                rVcRep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var rvc = ex.Message;
                return false;
            }

        }

        public RoleVsAction GetSingleRoleVsAction(int rVcId)
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var rVcObj = rVcRep.GetOperation()
                    .Include(c=>c.Roles)
                    .Include(c=>c.ActionLists)
                    .Filter(c => c.Id == rVcId)
                    .Get().SingleOrDefault();

                //companyRep.GetSingleObject(companyId);
                return rVcObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public List<RoleVsAction> GetAllRoleVsAction()
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var rVcObj = rVcRep.GetOperation()
                    .Include(c => c.Roles)
                    .Include(c => c.ActionLists)
                    .Get().ToList();
                //companyRep.GetSingleObject(companyId);
                return rVcObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
    }
}
