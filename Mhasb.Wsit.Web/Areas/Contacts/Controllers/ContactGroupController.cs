using Mhasb.Domain.Contacts;
using Mhasb.Services.Contact;
using Mhasb.Services.Loggers;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Contacts.Controllers
{
    public class ContactGroupController : Controller
    {

        private readonly IUserService uService = new UserService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ICompanyService cService = new CompanyService();
        private readonly IContactGroupService conGSer = new ContactGroupService();
        // GET: Contacts/ContactGroup
        public ActionResult Index()
        {
            return View();
        }

        // GET: Contacts/ContactGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //// GET: Contacts/ContactGroup/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Contacts/ContactGroup/Create
        [HttpPost]
        public ActionResult Create(string name)
        {
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            ContactGroup Group = new ContactGroup();
            Group.GroupName = name;
            Group.CompanyId = companyId;
            
            if (conGSer.CreateContactGroup(Group))
            {
                return Json(new { msg = "Success" });
            }
            else 
            {
                return Json(new { msg = "Failed" });
            }
        }

        // GET: Contacts/ContactGroup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contacts/ContactGroup/Edit/5
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

        // GET: Contacts/ContactGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contacts/ContactGroup/Delete/5
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
