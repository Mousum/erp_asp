using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.AdminUsers;

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
    }
}
