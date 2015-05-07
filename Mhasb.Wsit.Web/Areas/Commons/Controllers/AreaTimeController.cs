using Mhasb.Domain.Commons;
using Mhasb.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Commons.Controllers
{
            
 
    public class AreaTimeController : Controller
    {
        private IAreaTimeService arService = new AreaTimeService();
        //
        // GET: /Commons/AreaTime/
        public ActionResult Index()
        {
            var model = arService.GetAllAreaTimes();
            if (model==null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //
        // GET: /Commons/AreaTime/Details/5
        public ActionResult Details(int id)
        {
            var model = arService.GetSingleAreaTime(id);
            return View(model);
        }

        //
        // GET: /Commons/AreaTime/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Commons/AreaTime/Create
        [HttpPost]
        public ActionResult Create(AreaTime areaTime)
        {
            try
            {
                arService.AddAreaTime(areaTime);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Commons/AreaTime/Edit/5
        public ActionResult Edit(int id)
        {
            var model = arService.GetSingleAreaTime(id);
            if (model==null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //
        // POST: /Commons/AreaTime/Edit/5
        [HttpPost]
        public ActionResult Edit(AreaTime areaTime)
        {
            try
            {
                arService.UpdateAreaTime(areaTime);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Commons/AreaTime/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Commons/AreaTime/Delete/5
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
