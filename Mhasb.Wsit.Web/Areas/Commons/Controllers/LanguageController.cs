using Mhasb.Domain.Commons;
using Mhasb.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Commons.Controllers
{
    public class LanguageController : Controller
    {
        private ILanguageService lService = new LanguageService();

        //
        // GET: /Commons/Language/
        public ActionResult Index()
        {
            var model = lService.GetAllLanguages();
            return View(model);
        }

        //
        // GET: /Commons/Language/Details/5
        public ActionResult Details(int id)
        {
            var model = lService.GetSingleLanguage(id);
            return View(model);
        }

        //
        // GET: /Commons/Language/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Commons/Language/Create
        [HttpPost]
        public ActionResult Create(Language language)
        {
            try
            {
                // TODO: Add insert logic here
                lService.CreateLanguage(language);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Commons/Language/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var model=lService.GetSingleLanguage(id);
           if (model == null)
           {
               return HttpNotFound();
           }

           return View(model);
        }

        //
        // POST: /Commons/Language/Edit/5
        [HttpPost]
        public ActionResult Edit(Language language)
        {
            try{
                lService.UpdateLanguage(language);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Commons/Language/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Commons/Language/Delete/5
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
