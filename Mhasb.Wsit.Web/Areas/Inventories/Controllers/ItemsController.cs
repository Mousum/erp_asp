using Mhasb.Services.Accounts;
using Mhasb.Services.Commons;
using Mhasb.Services.Loggers;
using Mhasb.Services.Users;
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
        public PartialViewResult CreateItem() 
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;

            var coalist = _coaService.GetAllChartOfAccountByComIdCostCentre(companyId);
            ViewBag.CoaList = coalist;
            var lookups = _luSer.GetLookupByType("Tax");//.Select(u => new { u.Id, TValue = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = lookups;
            return PartialView();
        }
        //public ActionResult CreateItemAjax() 
        //{

        //}
    }
}
