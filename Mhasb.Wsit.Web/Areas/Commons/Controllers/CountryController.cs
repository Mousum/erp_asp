using Mhasb.Domain.Commons;
using Mhasb.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Mhasb.Wsit.Web.Areas.Commons.Controllers
{
    public class CountryController : Controller
    {
        private ICountryService coService = new CountryService();
        //
        // GET: /Commons/Country/
        public ActionResult Index()
        {
            //var model=coService.GetAllCountries();
            //return View(model);
            return View("Index1");
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
            List<Country> Country = coService.GetAllCountries();
            if (!String.IsNullOrEmpty(searchString))
            {
                Country = Country.Where(s => s.CountryName.Contains(searchString)).ToList();
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return PartialView(Country.ToPagedList(pageNumber, pageSize));

        }

        //
        // GET: /Commons/Country/Details/5
        public ActionResult Details(int id)
        {
            var model = coService.GetSingleCountry(id);
            return View(model);
        }

        //
        // GET: /Commons/Country/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Commons/Country/Create
        [HttpPost]
        public ActionResult Create(Country country)
        {
            try
            {
                coService.CreateCountry(country);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Commons/Country/Edit/5
        public ActionResult Edit(int id)
        {
            var model = coService.GetSingleCountry(id);
            return View(model);
        }

        //
        // POST: /Commons/Country/Edit/5
        [HttpPost]
        public ActionResult Edit(Country country)
        {
            try
            {
                coService.UpdateCountry(country);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public String DeleteConfirmed(int id)
        {
            try
            {
                coService.DeleteCountry(id);
                return "Success";
                // return RedirectToAction("Index");
            }
            catch
            {
                return "Failed";
            }
        }
    }
}
