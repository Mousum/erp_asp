using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Mhasb.Wsit.Web.Admin.AuthSecurity
{
    public interface ICustomIdentity:IIdentity
    {
        bool IsInRole(string role);
        string ToJson();
    }
}