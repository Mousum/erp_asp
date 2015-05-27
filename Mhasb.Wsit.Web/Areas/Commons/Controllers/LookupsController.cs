using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Data;
using Mhasb.Services.Commons;
using PagedList;
using PagedList.Mvc;

namespace Mhasb.Wsit.Web.Areas.Commons.Controllers
{
    public class LookupsController : Controller
    {
        private ILookupService luSer = new LookupService(); 

        // GET: Commons/Lookups
        public ActionResult Index()
        {
            return View(luSer.GetAllLookups());
        }

        // GET: Commons/Lookups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookup lookup = luSer.GetSingleLookup(Int32.Parse(id.ToString()));
            if (lookup == null)
            {
                return HttpNotFound();
            }
            return View(lookup);
        }

        // GET: Commons/Lookups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commons/Lookups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LookupType,Key,Quantity,Value,Description,Order")] Lookup lookup)
        {
            if (ModelState.IsValid)
            {
                luSer.AddLookup(lookup);
                return RedirectToAction("Index");
            }

            return View(lookup);
        }

        // GET: Commons/Lookups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lookup lookup = luSer.GetSingleLookup(Int32.Parse(id.ToString()));
            if (lookup == null)
            {
                return HttpNotFound();
            }
            return View(lookup);
        }

        // POST: Commons/Lookups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LookupType,Key,Quantity,Value,Description,Order")] Lookup lookup)
        {
            if (ModelState.IsValid)
            {
                luSer.UpdateLookup(lookup);
                return RedirectToAction("Index");
            }
            return View(lookup);
        }

        // GET: Commons/Lookups/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Lookup lookup = db.Lookups.Find(id);
        //    if (lookup == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lookup);
        //}

        //// POST: Commons/Lookups/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Lookup lookup = db.Lookups.Find(id);
        //    db.Lookups.Remove(lookup);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

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
            List<Lookup> Lookup = luSer.GetAllLookups();
            if (!String.IsNullOrEmpty(searchString))
            {
                Lookup = Lookup.Where(s => s.LookupType.Contains(searchString)).ToList();
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return PartialView(Lookup.ToPagedList(pageNumber, pageSize));

        }
    }
}
