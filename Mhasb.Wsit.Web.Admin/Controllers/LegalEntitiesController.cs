using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mhasb.Domain.Commons;
using Mhasb.Services.Commons;
//using Mhasb.Wsit.DAL.Data;

namespace Mhasb.Wsit.Web.Areas.Commons.Controllers
{
    public class LegalEntitiesController : Controller
    {
        private  ILegalEntityService lEser = new LegalEntityService();

        // GET: Commons/LegalEntities
        public ActionResult Index()
        {
            return View(lEser.GetAllLegalEntities());
        }

        // GET: Commons/LegalEntities/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LegalEntity legalEntity = lEser.
        //    if (legalEntity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(legalEntity);
        //}

        // GET: Commons/LegalEntities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commons/LegalEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LegalEntityName")] LegalEntity legalEntity)
        {
            if (ModelState.IsValid)
            {

                lEser.AddLegalEntiy(legalEntity);
                TempData.Add("SucMsg", "Added Successfully");
                return RedirectToAction("Index");
            }

            return View(legalEntity);
        }

        // GET: Commons/LegalEntities/Edit/5
        public ActionResult Edit(int id)
        {
            
            LegalEntity legalEntity = lEser.GetSingleLegalEntity(id);
            if (legalEntity == null)
            {
                return HttpNotFound();
            }
            return View(legalEntity);
        }

        // POST: Commons/LegalEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LegalEntityName")] LegalEntity legalEntity)
        {
            if (ModelState.IsValid)
            {
                lEser.UpdateLegalEntiy(legalEntity);
                return RedirectToAction("Index");
            }
            return View(legalEntity);
        }

        // GET: Commons/LegalEntities/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LegalEntity legalEntity = db.LegalEntities.Find(id);
        //    if (legalEntity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(legalEntity);
        //}

        // POST: Commons/LegalEntities/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public string DeleteConfirmed(int id)
        {
            if (lEser.DeleteLegalEntity(id)) 
            {
                return "Success";
            }
            return "Failed";
            //return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
