using Mhasb.Domain.OrgSettings;
using Mhasb.Services.OrgSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class CurrencyController : Controller
    {
        //
        // GET: /OrgSettings/Currency/
        private ICurrency cService = new CurrencyService();
        //
        // GET: /OrgSettings/Currency/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(cService.GetAllCurrency());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Currency cr)
        {
            cService.AddCurrency(cr);
            return RedirectToAction("Index", "Currency", new { Area = "OrgSettings" });
        }

        public ActionResult Edit(int id)
        {
            return View(cService.GetCurrencyById(id));
        }

        [HttpPost]
        public ActionResult Edit(Currency cr)
        {
            cService.UpdateCurrency(cr);
            return RedirectToAction("Index", "Currency", new { Area = "OrgSettings" });
        }

        public ActionResult Delete(int id)
        {
            cService.DeleteCurrency(id);
            return RedirectToAction("Index", "Currency", new { Area = "OrgSettings" });
        }

	
	}
}
