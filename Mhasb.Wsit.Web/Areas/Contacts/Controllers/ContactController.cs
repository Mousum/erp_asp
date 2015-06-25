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
    public class ContactController : Controller
    {
        private readonly IContactInformationService _sContact = new ContactInformationService();
        private readonly IUserService uService = new UserService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ICompanyService cService = new CompanyService();
        private readonly IContactInformationService ConInSer = new ContactInformationService();
        private readonly IPersonService pSer = new PersonService();
        //
        // GET: /Contacts/Contact/
        public ActionResult Index()
        {
            var model = ConInSer.GetAllContactInformation();
            return View(model);
        }
        [HttpGet]
        public ActionResult FilterContact(string Filter, string SearchString) 
        {
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            var contacts = pSer.GetAllContactsByCompany(companyId);
            if (Filter != null && SearchString!=null) 
            {
                contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(SearchString)&&r.ContactInformations.ContactName.StartsWith(Filter)).ToList();
             
            }
            else if (Filter == null && SearchString != null)
            {
                contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(SearchString)).ToList();
            }
            else if (Filter != null && SearchString == null) 
            {
                contacts = contacts.Where(r => r.ContactInformations.ContactName.StartsWith(Filter)).ToList();
            }



            return View(contacts);
            

           
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
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            var activatedCompany = cService.GetSingleCompany(companyId);

            ViewData["Company"] = activatedCompany;
            return View();
        }

        //
        // POST: /Contacts/Contact/Create
        //[HttpPost]
        //public ActionResult Create(ContactInformation ContactData)
        //{
        //    try
        //    {
        //        var tt = HttpContext.User.Identity.Name;
        //        var user = uService.GetSingleUserByEmail(tt);
        //        var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
        //        int companyId = 0;
        //        if (logObj != null)
        //        {
        //            companyId = (int)logObj.CompanyId;
        //        }
        //        var activatedCompany = cService.GetSingleCompany(companyId);
        //        try{
        //            ContactData.CompanyId = companyId;
        //            ContactData.CreateBy = user.Id;
        //            ContactData.UpdateBy = user.Id;
        //            ContactData.CreateDate = DateTime.Now;
        //            ContactData.UpdateDate = DateTime.Now;
        //            if (_sContact.CreateContInfo(ContactData))
        //            {

        //                return RedirectToAction("Index");
        //            }
        //            else {
        //                TempData.Add("errMsg", "Please FillUp Every Field");
        //                return RedirectToAction("Create");
        //            }
                    
        //        }catch(Exception ex){
        //            return Content("Somthing Wrong");
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



        [HttpPost]
        public ActionResult Create(CustomModel.Contact.ContactCustome cc) 
        {
            return Content("hi");
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

    }
}
