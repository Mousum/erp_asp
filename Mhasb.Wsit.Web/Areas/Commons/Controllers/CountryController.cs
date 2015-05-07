using Mhasb.Domain.Commons;
using Mhasb.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Commons.Controllers
{
    public class CountryController : Controller
    {
        private ICountryService coService = new CountryService();
        //
        // GET: /Commons/Country/
        public ActionResult Index()
        {
            var model=coService.GetAllCountries();
            return View(model);
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

        //
        // GET: /Commons/Country/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Commons/Country/Delete/5
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
    }
}
