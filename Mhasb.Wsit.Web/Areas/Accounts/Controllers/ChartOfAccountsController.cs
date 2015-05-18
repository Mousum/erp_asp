using Mhasb.Domain.Accounts;
using Mhasb.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mhasb.Wsit.Web.Areas.Accounts.Controllers
{
    public class ChartOfAccountsController : Controller
    {
        private IChartOfAccountService cSer = new ChartOfAccountService();

        // GET: OrgSettings/ChartOfAccounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() 
        {
           var  Atypes  = cSer.GetAllChartOfAccount();
            return View();
        }
    }
}