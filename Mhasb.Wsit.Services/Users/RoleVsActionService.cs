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

        public bool UpdateRoleVsAction(RoleVsAction rolevsaction)
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


        //public void GetActionByRoleID(int roleId)
        //{
        //    try
        //    {
        //        var roleactionList = rVcRep.GetOperation()
        //            .Include(r => r.Roles)
        //            .Filter(r => r.RoleId == roleId)
        //            .Get().ToList();
        //        var actionList = acRep.GetOperation().Get().ToList();

        //        var alData = from al in actionList
        //                     join ra in roleactionList 
        //                        on  al equals ra  into ar_al
        //                     from ra in ar_al.DefaultIfEmpty()
        //                         //.Where(a => a.ActionId == al.Id)
        //                         //.DefaultIfEmpty()


        //                     select new 
        //                     {
        //                         ActionId = ar_al.Id,
        //                         RoleId=ra.RoleId,
        //                         //ActionId=ra.ActionId,
        //                         //Name = al.ActionName,
        //                         IsActive = (ar_al==null)? false: ar_al.IsActive
        //                     };

        //                         //rVcRep.GetOperation()
        //            //.Include(c => c.ActionId)
        //            //.Include(c => c.RoleId)
        //            //.Filter(c => c.RoleId == roleId).Get().ToList();
        //        var oo =  alData.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message;
        //       // return null;
        //    }
        //}
        public static List<T> MakeList<T>(T itemOftype)
        {
            List<T> newList = new List<T>();
            return newList;
        }    
    }
}
