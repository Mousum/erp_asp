using Mhasb.Domain.Inventories;
using Mhasb.Services.Accounts;
using Mhasb.Services.Commons;
using Mhasb.Services.Inventories;
using Mhasb.Services.Loggers;
using Mhasb.Services.Users;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Inventories.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IUserService _uService = new UserService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ISettingsService _sService = new SettingsService();
        private readonly IChartOfAccountService _coaService = new ChartOfAccountService();
        private readonly ILookupService _luSer = new LookupService();
        private readonly IItemService ItemSer = new ItemService();
        // GET: Inventories/Items
        public ActionResult Index()
        {
            return View();
        }

        // GET: Inventories/Items/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inventories/Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Items/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventories/Items/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inventories/Items/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventories/Items/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventories/Items/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public PartialViewResult CreateItem(string ActionFlag)
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
            var coalist = _coaService.GetAllChartOfAccountByComIdCostCentre(companyId);
            var lookups = _luSer.GetLookupByType("Tax");//.Select(u => new { u.Id, TValue = u.Value + "(" + u.Quantity + "%)" });
          
            //ViewBags
            ViewBag.Lookups = lookups;
            ViewBag.CoaList = coalist;
            ViewBag.ActionFlag = ActionFlag;
            
            return PartialView();
        }
        public ActionResult CreateItemAjax()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
           
            var item = Request["item"].ToString(); // Get the JSON string
            JArray itemData = JArray.Parse(item); // It is an array so parse into a JArray
           
            Item obj = new Item();
            obj.ItemName = itemData[0]["name"].ToString();
            obj.ItemCode = itemData[0]["code"].ToString();
            obj.AssetAccountId = int.Parse(itemData[0]["assetAccount"].ToString());
            obj.PurchaseUnitPrice = double.Parse(itemData[0]["purunitprice"].ToString());
            obj.PurchasesAccountId = int.Parse(itemData[0]["purAccount"].ToString());
            obj.PTaxRateId = int.Parse(itemData[0]["purTax"].ToString());
            obj.PurchaseDescription = itemData[0]["purDes"].ToString();
            obj.SellUnitPrice = double.Parse(itemData[0]["salesUnitPrice"].ToString());
            obj.SalesAccountId = int.Parse(itemData[0]["salesAccount"].ToString());
            obj.STaxRateId = int.Parse(itemData[0]["salesTax"].ToString());
            obj.SalesDescription = itemData[0]["salesDescription"].ToString();
            obj.CompanyId = companyId;

            if (ItemSer.AddItem(obj))
            {
                return Json(new { id = obj.Id, name = obj.ItemName, code = obj.ItemCode });
            }
            else 
            {
                return Json(new { msg="failed" });
            }

           


        }
    }
}
