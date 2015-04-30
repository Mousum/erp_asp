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
    public class RoleService : IRoleService
    {
        private readonly CrudOperation<Role> roleRep = new CrudOperation<Role>();

        public bool AddRole(Role role)
        {
            try
            {
                role.State = ObjectState.Added;
                roleRep.AddOperation(role);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public bool EditRole(Role role)
        {
            try
            {
                role.State = ObjectState.Modified;
                roleRep.UpdateOperation(role);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public bool DeleteRole(int RoleId)
        {
            try
            {

                roleRep.DeleteOperation(RoleId);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }
        public Role GetSingleRole(int RoleId)
        {
            try
            {
                //role.State = ObjectState.Unchanged;
                var comObj = roleRep.GetOperation()
                                        .Filter(r =>r.Id == RoleId)
                                        .Get().SingleOrDefault();

                //roleRep.GetSingleObject(companyId);
                return comObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }

        public List<Role> GetAllRoles()
        {
            try
            {
                //role.State = ObjectState.Unchanged;
                var comObj = roleRep.GetOperation()
                                    .Get().ToList();

                //roleRep.GetSingleObject(roleId);
                return comObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }
    }
}
