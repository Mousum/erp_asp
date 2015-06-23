using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mhasb.Domain.Subscriptions;
using Mhasb.Wsit.Web.Admin.Models;
using Mhasb.Services.Subscriptions;

namespace Mhasb.Wsit.Web.Admin.Controllers
{
    public class PackagesController : Controller
    {
        private IPackagesServices db = new PackageServices();

        // GET: Packages
        public ActionResult Index()
        {
            var test = db.GetAllPackages();
            return View(db.GetAllPackages());
        }

        // GET: Packages/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.GetSinglePackage(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Amount,Duration,Status")] Package package)
        {
            if (ModelState.IsValid)
            {
                string[] descriptions = Request.Form.GetValues("descriptions");
                if (descriptions != null) 
                {
                    package.Descriptions = string.Join(",", descriptions);
                }
                db.CreatePackages(package);
                
                return RedirectToAction("Index");
            }

            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.GetSinglePackage(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Amount,Duration,Status")] Package package)
        {
            if (ModelState.IsValid)
            {
                string[] descriptions = Request.Form.GetValues("descriptions");
                if (descriptions != null) 
                {
                    package.Descriptions = string.Join(",", descriptions);
                }
                db.UpdatePackage(package);
                return RedirectToAction("Index");
            }
            return View(package);
        }

    

        //// POST: Packages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
            
        //    return RedirectToAction("Index");
        //}

       
    }
}
