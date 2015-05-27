using Mhasb.Domain.Organizations;
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
        private readonly CrudOperation<Company> comRep = new CrudOperation<Company>();
        public bool AddUser(User user)
        {
            try
            {
                user.CreatedTime = DateTime.Now;
                
                user.State = ObjectState.Added;
                userRep.AddOperation(user);
                return true;
            }
            catch (Exception ex) {
                var rr = ex.Message;
                return false;
            }
            
        }
        public bool UpdateUser(User user)
        {

            try
            {

                user.State = ObjectState.Modified;
                userRep.UpdateOperation(user);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }


        public bool UserLogin(string email, string password)
        {
            //var userObj = userRep.GetOperation()
            //                      .Filter(u => u.Email == email)
            //                      .GetIQueryable();

            var userObj = userRep.GetOperation()
                                  .Filter(u => u.Email == email)
                                  .Get().FirstOrDefault();


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

        public bool CheckUserExistence(string email) {
            var userObj = userRep.GetOperation()
                                  .Filter(u => u.Email == email)
                                  .Get().SingleOrDefault();
            if (userObj != null)
                return true;
            else 
                return false;
            
        }

        public User GetSingleUserByEmail(string  email) {

            var userObj = userRep.GetOperation()
                                  .Filter(u => u.Email == email)
                                  .Get().FirstOrDefault();

            return userObj;
        
        }

        public List<User> GetAllUsers() 
        {
            try
            {
            
                var UserObj = userRep.GetOperation()
                                    .Get() .ToList();

                
                return UserObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }



        public bool isSupperAdmin(long userId, int CompanyId)
        {
            try {
                var comObj = comRep.GetOperation()
                     .Filter(c => c.Id == CompanyId && c.Users.Id == userId)
                    .Get().Count();
                if (comObj == 1)
                {
                    return true;
                }
                else {
                    return false;
                }
                
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }

        }


        public List<Company> GetcompanyByUserID(long userId)
        {
            try
            {
                var comObj = comRep.GetOperation()
                     .Filter(c=>c.Users.Id == userId)
                     .Get().ToList();
                return comObj;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
               
                return null;
            }
        }
    }
}
