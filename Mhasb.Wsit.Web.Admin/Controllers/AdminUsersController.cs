using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mhasb.Domain.AdminUsers;
using Mhasb.Wsit.Web.Admin.Models;
using Mhasb.Services.AdminUsers;

namespace Mhasb.Wsit.Web.Admin.Controllers
{
    public class AdminUsersController : Controller
    {
        private IAdminUserServices db = new AdminUserService(); 

        // GET: AdminUsers
        public ActionResult Index()
        {
            return View(db.GetAllAdminUsers());
        }

        // GET: AdminUsers/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUser adminUser = db.GetSingleAdminUser(id);
            if (adminUser == null)
            {
                return HttpNotFound();
            }
            return View(adminUser);
        }

        // GET: AdminUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Password,type,PhoneNo,State")] AdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
                db.AddAdminUser(adminUser);
               
                return RedirectToAction("Index");
            }

            return View(adminUser);
        }

        // GET: AdminUsers/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUser adminUser = db.GetSingleAdminUser(id);
            if (adminUser == null)
            {
                return HttpNotFound();
            }
            return View(adminUser);
        }

        // POST: AdminUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,type,PhoneNo,State")] AdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
              
                db.UpdateAdminUser(adminUser);
                return RedirectToAction("Index");
            }
            return View(adminUser);
        }

    

        // POST: AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public string DeleteConfirmed(int id)
        {

            try
            {
                db.DeleteAdminUser(id);
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
