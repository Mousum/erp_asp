﻿using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}