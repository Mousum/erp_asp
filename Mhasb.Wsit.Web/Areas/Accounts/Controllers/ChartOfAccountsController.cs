using Mhasb.Domain.Accounts;
using Mhasb.Services.Accounts;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhasb.Services.Commons;


namespace Mhasb.Wsit.Web.Areas.Accounts.Controllers
{
    public class ChartOfAccountsController : Controller
    {
        private IChartOfAccountService cSer = new ChartOfAccountService();
        private IUserService uService = new UserService();
        private ISettingsService setService = new SettingsService();
        private ILookupService luSer = new LookupService();

        // GET: OrgSettings/ChartOfAccounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() 
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            var Atypes = cSer.GetAllChartOfAccountByCompanyId(AccSet.Companies.Id);
            var lookups = luSer.GetLookupByType("Tax").Select(u => new { Id= u.Id, Value =u.Value+"("+u.Quantity+"%)"});
            ViewBag.Lookups = new SelectList(lookups,"Id","Value");

            ViewBag.ATypes = new SelectList(Atypes, "Id", "AName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(ChartOfAccount chartOfAccount) 
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            chartOfAccount.CompanyId = AccSet.Companies.Id;

            if (cSer.AddChartOfAccount(chartOfAccount))
            {
                return Content("Success");
            }
            else {
                return Content("Failed");
            }
        }
        [HttpPost]
        public string GetCode(int id) 
        {
           var chartHead = cSer.GetSingleChartOfAccount(id);
           string Acode = id.ToString() + string.Format("{0:00}", cSer.GetAllChartOfAccount().Count()+1); //Prints 01
           return Acode;

        }
        public ActionResult CostCentresSettings() {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            var centers= cSer.GetAllChartOfAccountByComIdCostCentre(AccSet.Companies.Id);
            ViewBag.centers = new SelectList(centers, "Id", "AName");
            return View("Costcentre");
        }
        public ActionResult Edit(int id) 
        {
            var lookups = luSer.GetLookupByType("Tax").Select(u => new { Id = u.Id, Value = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = new SelectList(lookups, "Id", "Value");
            var model = cSer.GetSingleChartOfAccount(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ChartOfAccount ca)
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            ca.CompanyId = AccSet.Companies.Id;
            if (cSer.UpdateChartOfAccount(ca))
            {
                return Content("Success");
            }
            else
            {
                return Content("Failed");
            }
        }

    }
}