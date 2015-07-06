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
using Mhasb.Wsit.Web.Controllers;
using Mhasb.Services.Loggers;
using Mhasb.Services.Organizations;
using Newtonsoft.Json.Linq;


namespace Mhasb.Wsit.Web.Areas.Accounts.Controllers
{
    public class ChartOfAccountsController : BaseController
    {
        private readonly IChartOfAccountService _cSer = new ChartOfAccountService();
        private readonly IUserService _uService = new UserService();
        private readonly ISettingsService _setService = new SettingsService();
        private readonly ILookupService _luSer = new LookupService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ICompanyService _iCompany = new CompanyService();

        // GET: OrgSettings/ChartOfAccounts

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult PartialAddAccount(string ActionFlag)
        {
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(UserId);

            var atypes = _cSer.GetAllChartOfAccountByCompanyIdForSecondLevel(CompanyId);

            if (atypes.Count == 0)
            {
                _cSer.AddBaseAccountTypes();
                atypes = _cSer.GetAllChartOfAccountByCompanyId(CompanyId);
            }
            var lookups = _luSer.GetLookupByType("Tax").Select(u => new { Id = u.Id, Value = u.Value + "(" + u.Quantity + "%)" });
            
            //ViewBags
            ViewBag.Lookups = new SelectList(lookups, "Id", "Value");
            ViewBag.CompanyCompleteFlag = logObj.Companies.CompleteFlag;
            ViewBag.ActionFlag = ActionFlag;
            ViewBag.ATypes = atypes;
            
            return View("_AddAccount");
        }

        //[HttpPost]
        //public JsonResult PartialAddAccount(ChartOfAccount chartOfAccount) 
        //{
        //    var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
        //    var accSet = _setService.GetAllByUserId(user.Id);


        //    var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
        //    int companyId = 0;

        //    if (logObj != null)
        //    {
        //        if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
        //    }

        //    chartOfAccount.CompanyId = companyId;
        //    if (_cSer.AddChartOfAccount(chartOfAccount))
        //    {
        //        // ReSharper disable once PossibleNullReferenceException
        //        if (logObj.Companies.CompleteFlag == 3)
        //        {
        //            if (_iCompany.UpdateCompleteFlag(companyId, flag))
        //            {
        //                return RedirectToAction("Finish", "Users", new { Area = "UserManagement" });
        //            }
        //            else
        //            {
        //                return RedirectToAction("Create", "ChartOfAccounts", new { area = "Accounts" });
        //            }
        //        }
        //        return RedirectToAction("Create", "ChartOfAccounts", new { Area = "Accounts" });

        //    }
        //    else
        //    {
        //        TempData.Add("errMsg", "Chart Of Account Addtion Failed");
        //    }

        //    return Json(new { id = obj.Id, name = obj.ItemName, code = obj.ItemCode });
        //}

        public ActionResult Create(int account=0)
        {
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(UserId);
            if (logObj.Companies.CompleteFlag <= 5 && logObj.Companies.CompleteFlag >= 3)
            {
                ViewBag.CompanyCompleteFlag = logObj.Companies.CompleteFlag;
                var atypes = _cSer.CodeWiseGetAllChartOfAccountByCompanyIdForLastLevel(CompanyId, account);

                return View("Create_new", atypes);
            }
            string absUrl;
            if (!checkCompanyFlow(out absUrl))
            {
                return Redirect(absUrl);
            }
            TempData.Add("errMsg", "Something Wrong...");
            return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
        }
        [HttpPost]
        public ActionResult Create(ChartOfAccount chartOfAccount)
        {
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(UserId);

            int flag = 4;
            chartOfAccount.CompanyId = CompanyId;
            if (_cSer.AddChartOfAccount(chartOfAccount))
            {
                // ReSharper disable once PossibleNullReferenceException
                if (logObj.Companies.CompleteFlag == 3)
                {
                    if (_iCompany.UpdateCompleteFlag(CompanyId, flag))
                    {
                        return RedirectToAction("Finish", "Users", new { Area = "UserManagement" });
                    }
                    else
                    {
                        return RedirectToAction("Create", "ChartOfAccounts", new { area = "Accounts" });
                    }
                }
                return RedirectToAction("Create", "ChartOfAccounts", new { Area = "Accounts" });

            }
            else
            {
                TempData.Add("errMsg", "Chart Of Account Addtion Failed");
                return RedirectToAction("Create", "ChartOfAccounts", new { area = "Accounts" });
            }
        }
        [HttpPost]
        public ActionResult GetCode(int id)
        {
            var chartHead = _cSer.GetSingleChartOfAccount(id);
            var acode = _cSer.GeneratedCode(chartHead.ACode, chartHead.Level + 1);
            return Json(new { code = acode, lavel = chartHead.Level + 1 });

        }
        public ActionResult CostCentresSettings()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            //var AccSet = setService.GetAllByUserId(user.Id);

            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }

            var centers = _cSer.GetAllChartOfAccountByComIdCostCentre(companyId);
            ViewBag.centers = new SelectList(centers, "Id", "AName");
            return View("Costcentre");
        }
        public ActionResult Edit(int id)
        {
            var lookups = _luSer.GetLookupByType("Tax").Select(u => new { Id = u.Id, Value = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = new SelectList(lookups, "Id", "Value");
            var model = _cSer.GetSingleChartOfAccount(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ChartOfAccount ca)
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _setService.GetAllByUserId(user.Id);
            ca.CompanyId = AccSet.Companies.Id;
            if (_cSer.UpdateChartOfAccount(ca))
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
            var coa = _cSer.GetSingleChartOfAccount(id);
            return Json(new { Acode = coa.ACode, des = coa.Description });
        }
        [HttpPost]
        public ActionResult SetCostCentreDetails(int id, string ACode, string description)
        {
            var coa = _cSer.GetSingleChartOfAccount(id);
            coa.Description = description;
            coa.ACode = ACode;
            coa.IsCostCenter = true;

            if (_cSer.UpdateChartOfAccount(coa))
            {
                return Json(new { coa.ACode, coa.AName, msg = "success" });

            }
            else
            {
                return Json(new { coa.ACode, coa.AName, msg = "failed" });
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
            var nodes = _cSer.TreeViewList(code, level);
            return Json(nodes);
        }

        public ActionResult CreateAccountAjax() {
            var item = Request["item"].ToString(); // Get the JSON string
            JArray itemData = JArray.Parse(item); // It is an array so parse into a JArray
            
             var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            //var AccSet = setService.GetAllByUserId(user.Id);

            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            ChartOfAccount obj = new ChartOfAccount();

            //defination
            obj.AName = itemData[0]["name"].ToString();
            obj.ACode = itemData[0]["code"].ToString();
            obj.CompanyId = companyId;
            obj.Description = itemData[0]["description"].ToString();
            obj.IsCostCenter = bool.Parse(itemData[0]["CostCenter"].ToString());
            obj.TaxId = int.Parse(itemData[0]["taxid"].ToString());
            obj.ShowInDashboard = bool.Parse(itemData[0]["dashboard"].ToString());
            obj.ShowInExpenseClaims = bool.Parse(itemData[0]["Eclaim"].ToString());
            obj.Level = int.Parse(itemData[0]["level"].ToString());

            //function call 
            if (_cSer.AddChartOfAccount(obj))
            {
                return Json(new { id = obj.Id, code = obj.ACode, name = obj.AName, msg = "passed" });
            }
            else 
            {
                return Json(new { msg="failed" });
            }
            
        }

    }
}