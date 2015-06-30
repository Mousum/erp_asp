using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mhasb.Wsit.Web.Admin.AuthSecurity
{
    public class IdentityRepresentation
    {
        private bool ia;

        public bool IsAuthenticated
        {
            get { return ia; }
            set { ia = value; }
        }

        private string n;

        public string Name
        {
            get { return n; }
            set { n = value; }
        }

        private string r;

        public string Roles
        {
            get { return r; }
            set { r = value; }
        }
    }
}