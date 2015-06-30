using Mhasb.Domain.Contacts;
using Mhasb.Services.Contact;
using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Inventories
{
    public class PurchasesController : Controller
    {

        private IContactInformationService _repcontact = new ContactInformationService();


        //
        // GET: /Inventories/Purchases/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditBill()
        {
            return View();
        }
        public JsonResult GetServiceNames(string term) {
            var results = _repcontact.GetAllContactInformation().Where(s => term == null || s.ContactName.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Id, value = x.ContactName }).Take(5).ToList();
            
            return Json(results, JsonRequestBehavior.AllowGet);
        }
	}
}