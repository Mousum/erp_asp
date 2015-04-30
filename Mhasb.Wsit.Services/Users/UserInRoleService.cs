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
    public class UserInRoleService :IUserInRoleService
    {
        private readonly CrudOperation<UserInRole> userInRoleRep = new CrudOperation<UserInRole>();

        public bool AddUserInRole(UserInRole userInRole) 
        {
            try
            {
                userInRole.State = ObjectState.Added;
                userInRoleRep.AddOperation(userInRole);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public  bool UpdateUserInRole(UserInRole userInRole)
        {
            try
            {
                userInRole.State = ObjectState.Added;
                userInRoleRep.UpdateOperation(userInRole);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public bool DeleteUserInRole(int userInRoleId)
        {
            try
            {

                userInRoleRep.DeleteOperation(userInRoleId);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public UserInRole GetSingleUserInRole(int userInRoleId) 
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var uIRObj = userInRoleRep.GetOperation()
                                        .Include(u => u.Employees)
                                        
                                        .Get().SingleOrDefault();

                //companyRep.GetSingleObject(companyId);
                return uIRObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }
        public List<UserInRole> GetAllUserInRole() {
            try
            {
                var uIRObj = userInRoleRep.GetOperation()
                                        .Include(u => u.Employees)
                                        .Get().ToList();
                return uIRObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

    }
}
