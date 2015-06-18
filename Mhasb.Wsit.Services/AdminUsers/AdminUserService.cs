using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.AdminUsers;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.AdminUsers
{
    public class AdminUserService : IAdminUserServices
    {
        private readonly CrudOperation<AdminUser> adRep = new CrudOperation<AdminUser>();
        public bool AdminLogin(string email, string password)
        {
            //var userObj = userRep.GetOperation()
            //                      .Filter(u => u.Email == email)
            //                      .GetIQueryable();

            var userObj = adRep.GetOperation()
                                  .Filter(u => u.Email == email)
                                  .Get().FirstOrDefault();


            if (userObj != null)
            {
                if (userObj.Password == password)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }

        }

        public bool AddAdminUser(AdminUser admin)
        {
            try
            {
                admin.State = ObjectState.Added;
                adRep.AddOperation(admin);
                return true;
            }
            catch (Exception e) 
            {
               var msg = e.Message;
               return false;
            }

        }
        public bool UpdateAdminUser(AdminUser admin) 
        {
            try
            {
                admin.State = ObjectState.Modified;
                adRep.UpdateOperation(admin);
                return true;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return false;
            }
        }
        public bool DeleteAdminUser(int AdminUserId) 
        {
            try
            {
                adRep.DeleteOperation(AdminUserId);
                return true;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return false;
            }
        }
        public List<AdminUser> GetAllAdminUsers() 
        {
            try
            {
                var voObj = adRep.GetOperation()
                                        .Get().ToList();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
        public AdminUser GetSingleAdminUser(int UserId) 
        {
            try
            {
                var voObj = adRep.GetOperation()
                                        .Filter(ad => ad.Id == UserId)
                                        .Get().SingleOrDefault();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
    }
}
