using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Models
{
    public class AccountSetting
    {
        public User Users { get; set; }
        public Settings AccSettings { get; set; }
    }
}