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
    public class ActionListService : IActionListService
    {
        private readonly CrudOperation<ActionList> ActionRep = new CrudOperation<ActionList>();
        public bool AddActionList(ActionList actionlist)
        {
            try
            {
                actionlist.State = ObjectState.Added;
                ActionRep.AddOperation(actionlist);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }


        public bool UpdateActionList(ActionList actionlist)
        {
            try
            {

                actionlist.State = ObjectState.Modified;
                ActionRep.AddOperation(actionlist);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }


        public bool DeleteActionList(int Id)
        {
            try
            {
                //var company = ActionRep.GetSingleObject(Id);
                //company.State = ObjectState.Deleted;
                ActionRep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }


        public ActionList GetSingleActionList(int actionId)
        {
            try {
                var AcListObj = ActionRep.GetOperation()
                    .Filter(ac => ac.Id==actionId)
                    .Get()  
                    .SingleOrDefault();


                return AcListObj;

            }
            catch(Exception ex){
                var rr = ex.Message;
                return null;
            }
        }


        public List<ActionList> GetAllActionList()
        {
            try { 
                var ActionList=ActionRep.GetOperation()
                    .Get().ToList();
                return ActionList;
            }catch(Exception ex){
                var rr = ex.Message;
                return null;
            }
        }
    }
}
