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
    public class UserService : IUserService
    {
        private readonly CrudOperation<User> userRep = new CrudOperation<User>();
        public void AddUser(User user)
        {
            try
            {
                user.State = ObjectState.Added;
                userRep.AddOperation(user);
            }
            catch (Exception ex) {
                var rr = ex.Message;
            }
            
        }


        public bool UserLogin(string email, string password)
        {
            //var userObj = userRep.GetOperation()
            //                      .Filter(u => u.Email == email)
            //                      .GetIQueryable();

            var userObj = userRep.GetOperation()
                                  .Filter(u => u.Email == email)
                                  .Get().SingleOrDefault();


            if (userObj != null)
            {
                if (userObj.Password == password)
                    return true;
                else
                    return false;
            }
            else {
                return false;
            }

        }
    }
}
