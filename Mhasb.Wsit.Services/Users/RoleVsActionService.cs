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
    public class RoleVsActionService : IRoleVsActionService
    {
        private readonly CrudOperation<RoleVsAction> rVcRep = new CrudOperation<RoleVsAction>();
        private readonly CrudOperation<ActionList> acRep = new CrudOperation<ActionList>();

        public bool AddRoleVsAction(RoleVsAction rolevsaction)
        {
            try
            {
                var roleaction = rVcRep.GetOperation()
                    .Filter(r => r.RoleId == rolevsaction.RoleId && r.ActionId == rolevsaction.ActionId)
                    .Get().FirstOrDefault();

                if (roleaction == null)
                {
                    rolevsaction.State = ObjectState.Added;
                    rVcRep.AddOperation(rolevsaction);
                    rolevsaction.State = ObjectState.Unchanged;
                    return true;
                }
                else
                {
                    rolevsaction.Id = roleaction.Id;
                    return UpdateRoleVsAction(rolevsaction);
                }


            }
            catch (Exception ex)
            {
                var rvc = ex.Message;
                return false;
            }
        }

        public bool UpdateRoleVsAction(RoleVsAction rolevsaction)
        {
            try
            {
                var dbObj = rVcRep.GetSingleObject(rolevsaction.Id);
                dbObj.State = ObjectState.Modified;
                dbObj.IsActive = rolevsaction.IsActive;
                rVcRep.UpdateOperation(dbObj);
                dbObj.State = ObjectState.Unchanged;
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
                    .Include(c => c.Roles)
                    .Include(c => c.ActionLists)
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


        public List<RoleVsAction> GetAllAndSelectedActionByRoleId(int roleId)
        {
            try
            {
                var roleactionList = rVcRep.GetOperation()
                    .Include(r => r.Roles)
                    .Include(r => r.ActionLists)
                    .Filter(r => r.RoleId == roleId && r.IsActive == true)
                    .Get().ToList();
              //  return roleactionList;



                var actionList = acRep.GetOperation().Get().ToList();

                var alData = from al in actionList
                             join ra in roleactionList
                                on al.Id equals ra.ActionId into ar_al
                             from r_a in ar_al.DefaultIfEmpty(new RoleVsAction())
                             //.Where(a => a.ActionId == al.Id)
                             //.DefaultIfEmpty()


                             select new RoleVsAction
                             {
                                 ActionId = al.Id,
                                 RoleId = r_a.RoleId,
                                 ActionLists = new ActionList { Id = al.Id, ActionName = al.ActionName, ControllerName = al.ControllerName, ModuleName = al.ModuleName },
                                 //ActionId=ra.ActionId,
                                 //Name = al.ActionName,
                                 IsActive = r_a.IsActive
                             };

                //rVcRep.GetOperation()
                //.Include(c => c.ActionId)
                //.Include(c => c.RoleId)
                //.Filter(c => c.RoleId == roleId).Get().ToList();

                // var tt = alData.ToList();
                return alData.ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public List<RoleVsAction> GetActionByRoleId(int roleId)
        {
            try
            {
                var roleactionList = rVcRep.GetOperation()
                    .Include(r => r.Roles)
                    .Include(r => r.ActionLists)
                    .Filter(r => r.RoleId == roleId && r.IsActive == true)
                    .Get().ToList();

                return roleactionList;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public static List<T> MakeList<T>(T itemOftype)
        {
            List<T> newList = new List<T>();
            return newList;
        }
    }
}
