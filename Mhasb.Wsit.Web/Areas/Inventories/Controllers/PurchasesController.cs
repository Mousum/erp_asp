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
        public PartialViewResult ItemRow()
        {

            var Items = itemService.GetAllItems().Select(u => new { Id = u.Id, Name = u.ItemName });
            ViewBag.Items = new SelectList(Items, "Id", "Name");
            return PartialView();
        }
    }
}