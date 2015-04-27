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
            user.State = ObjectState.Added;
            userRep.AddOperation(user);
        }
    }
}
