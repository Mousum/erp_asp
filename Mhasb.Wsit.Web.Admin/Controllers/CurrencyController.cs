using Mhasb.Domain.OrgSettings;
using Mhasb.Services.OrgSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class CurrencyController : Controller
    {
        //
        // GET: /OrgSettings/Currency/
        private readonly ICurrency cService = new CurrencyService();
        //
        // GET: /OrgSettings/Currency/
        [AllowAnonymous]
        public ActionResult Index()
        {
            //return View(cService.GetAllCurrency());
            return View("Index1");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Currency cr)
        {
            if (cService.AddCurrency(cr))
                return RedirectToAction("Index", "Currency");
            else
            {
                ModelState.AddModelError("msg", "Currency did not inserted successfully");
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(cService.GetCurrencyById(id));
        }

        [HttpPost]
        public ActionResult Edit(Currency cr)
        {
            if (cService.UpdateCurrency(cr))
                return RedirectToAction("Index", "Currency", new { Area = "OrgSettings" });
            else
            {
                ModelState.AddModelError("msg", "Currency did not updated successfully");
                return View();
            }
        }

        public String Delete(int id)
        {
            try
            {
                cService.DeleteCurrency(id);
                return "Success";
                // return RedirectToAction("Index");
            }
            catch
            {
                return "Failed";
            }

            //if (cService.DeleteCurrency(id))
            //    return RedirectToAction("Index", "Currency", new { Area = "OrgSettings" });
            //else
            //{
            //    ModelState.AddModelError("msg", "Currency did not updated successfully");
            //    return RedirectToAction("Index", "Currency", new { Area = "OrgSettings" });
            //}
        }
        public ActionResult List(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            List<Currency> currency = cService.GetAllCurrency();
            if (!String.IsNullOrEmpty(searchString))
            {
                currency = currency.Where(s => s.Name.Contains(searchString)).ToList();
            }


            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return PartialView(currency.ToPagedList(pageNumber, pageSize));

        }

    }
}