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
    public class UserInRoleService : IUserInRoleService
    {
        private readonly CrudOperation<UserInRole> userInRoleRep = new CrudOperation<UserInRole>();
        private readonly CrudOperation<Role> roleRep = new CrudOperation<Role>();

        public bool AddUserInRole(UserInRole userInRole)
        {
            try
            {
                var ur = userInRoleRep.GetOperation()
                              .Filter(u => u.RoleId == userInRole.RoleId && u.EmployeeId == userInRole.EmployeeId)
                              .Get()
                              .FirstOrDefault();
                if (ur == null)
                {
                    userInRole.State = ObjectState.Added;
                    userInRoleRep.AddOperation(userInRole);
                    return true;
                }
                else {
                    userInRole.Id = ur.Id;
                    return UpdateUserInRole(userInRole);
                }
                

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public bool UpdateUserInRole(UserInRole userInRole)
        {
            try
            {
                var dbObj = userInRoleRep.GetSingleObject(userInRole.Id);

                dbObj.IsActive = userInRole.IsActive;
                dbObj.State = ObjectState.Modified;
                userInRoleRep.UpdateOperation(dbObj);
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
        public List<UserInRole> GetAllUserInRole()
        {
            try
            {
                var uIRObj = userInRoleRep.GetOperation()
                                        .Include(r=>r.Roles)
                                        .Include(u => u.Employees.Users)
                                        .Get().ToList();
                return uIRObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }


        public List<UserInRole> GetRoleListByUser(long Id)
        {
            try
            {
                var roleList = roleRep.GetOperation()
                    .Get().ToList();
                var urList = userInRoleRep.GetOperation()
                                               .Include(u => u.Employees.Users)
                                               .Include(u => u.Roles)
                                               .Filter(u => u.Employees.Users.Id == Id)
                                               .Get()
                                               .ToList();

                var alData = from rl in roleList
                             join ur in urList
                                on rl.Id equals ur.RoleId into ar_al
                             from r_a in ar_al.DefaultIfEmpty(new UserInRole())
                             //.Where(a => a.ActionId == al.Id)
                             //.DefaultIfEmpty()


                             select new UserInRole
                             {
                                 Id = r_a.Id,
                                 RoleId = rl.Id,
                                 Roles = new Role { Id = rl.Id, RoleName = rl.RoleName },
                                 //ActionId=ra.ActionId,
                                 //Name = al.ActionName,
                                 IsActive = r_a.IsActive
                             };

                return alData.ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public List<UserInRole> GetRoleListByUserAndCompany(long userId, int companyId)
        {
            try
            {
                var roleList = roleRep.GetOperation()
                    .Get().ToList();
                var urList = userInRoleRep.GetOperation()
                                               .Include(u => u.Employees.Users)
                                               .Include(u => u.Roles)
                                               .Filter(u => u.Employees.Users.Id == userId)
                                               .Filter(c=>c.Roles.CompanyId==companyId)
                                               .Get()
                                               .ToList();

                var alData = from rl in roleList
                             join ur in urList
                                on rl.Id equals ur.RoleId into ar_al
                             from r_a in ar_al.DefaultIfEmpty(new UserInRole())
                             //.Where(a => a.ActionId == al.Id)
                             //.DefaultIfEmpty()


                             select new UserInRole
                             {
                                 Id = r_a.Id,
                                 RoleId = rl.Id,
                                 Roles = new Role { Id = rl.Id, RoleName = rl.RoleName },
                                 //ActionId=ra.ActionId,
                                 //Name = al.ActionName,
                                 IsActive = r_a.IsActive
                             };

                return alData.ToList();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
       

    }
}
