using Mhasb.Domain.Contacts;
using Mhasb.Domain.Users;
using Mhasb.Services.Contact;
using Mhasb.Services.Loggers;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Contacts.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly IContactGroupService ConGSer = new ContactGroupService();
        private readonly IUserService uService = new UserService();
        //
        // GET: /Contacts/Contact/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Contacts/Contact/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Contacts/Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contacts/Contact/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contacts/Contact/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Contacts/Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contacts/Contact/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contacts/Contact/Delete/5
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
        [HttpPost]
        public ActionResult CreateGroup(string name)
        {
            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            var group = new ContactGroup();
            group.GroupName = name;
            group.CompanyId = logObj.Companies.Id;
            if (ConGSer.CreateContactGroup(group)) 
            {
                return   Json(new { msg = "Success"});
            }
            return Json(new { msg = "Failed" });

        }
    }
}
