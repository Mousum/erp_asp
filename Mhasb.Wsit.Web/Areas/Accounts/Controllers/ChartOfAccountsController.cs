using Mhasb.Domain.OrgSettings;
using Mhasb.Services.OrgSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class ChartOfAccountsController : Controller
    {
        // GET: OrgSettings/ChartOfAccounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() 
        {
            return View();
        }
    }
}