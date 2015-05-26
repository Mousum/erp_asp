using Mhasb.Domain.Accounts;
using Mhasb.Services.Accounts;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhasb.Services.Commons;
using Mhasb.Wsit.Web.Utilities;


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
                TempData.Add("SucMasg","Chart Of Account Added Sucessfully!");
                return RedirectToAction("Create", "ChartOfAccounts", new { area = "Accounts" });
            }
            else {
                TempData.Add("errMsg", "Chart Of Account Addtion Failed");
                return RedirectToAction("Create", "ChartOfAccounts", new { area = "Accounts" });
            }
        }
        [HttpPost]
        public ActionResult GetCode(int id) 
        {
           var chartHead = cSer.GetSingleChartOfAccount(id);
          var  Acode = cSer.GeneratedCode(chartHead.ACode, chartHead.Level + 1);
          return Json(new { code = Acode, lavel = chartHead.Level + 1 });

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
                TempData.Add("SucMasg", "Chart Of Account Updated Sucessfully!");
                return RedirectToAction("Edit", "ChartOfAccounts", new { id = ca.Id });
            }
            else
            {
                TempData.Add("errMsg", "Chart Of Account Updated Sucessfully!");
                return RedirectToAction("Edit", "ChartOfAccounts", new { id = ca.Id });
            }
        }
        [HttpPost]
        public ActionResult GetCostCentreDetails(int id) 
        {
            var coa = cSer.GetSingleChartOfAccount(id);
            return Json(new { Acode = coa.ACode,des = coa.Description});
        }
        [HttpPost]
        public ActionResult SetCostCentreDetails(int id,string ACode,string description)
        {
            var coa = cSer.GetSingleChartOfAccount(id);
            coa.Description = description;
            coa.ACode = ACode;
            coa.IsCostCenter = true;

            if (cSer.UpdateChartOfAccount(coa))
            {
                return Json(new {ACode= coa.ACode,AName = coa.AName ,msg = "success" });

            }
            else 
            {
                return Json(new { ACode = coa.ACode, AName = coa.AName, msg = "failed" });
            }

        }

        public ActionResult CoaTree()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CoaTreeList(string root)
        {
            var code = root.Split('_')[0];
            //int level;
            //try
            //{
            //    level = Convert.ToInt32(root.Split('_')[1]);
            //}
            //catch
            //{
            //    level = 0;
            //}

           code = root == "source" ? "0" : root;
            var level = UtilityManager.GetLedgerLevel(code);
            var nodes = cSer.TreeViewList(code, level);
            return Json(nodes);
        }

    }
}