using Mhasb.Domain.Contacts;
using Mhasb.Services.Contact;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Services.Inventories;
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
        private readonly IItemService itemService = new ItemService();
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
        public ActionResult RepeatTransection()
         {

             return View();
         }
        public JsonResult GetServiceNames(string term) {
            var results = _repcontact.GetAllContactInformation().Where(s => term == null || s.ContactName.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Id, value = x.ContactName }).Take(5).ToList();
            
            return Json(results, JsonRequestBehavior.AllowGet);
          
        }
        public PartialViewResult ItemRow()
        {

            var Items = itemService.GetAllItems().Select(u => new { Id = u.Id, Name = u.ItemName });
            ViewBag.Items = new SelectList(Items, "Id", "Name");
            return PartialView();
        }
	}
}