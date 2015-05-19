using Mhasb.Services.OrgSettings;
using Mhasb.Wsit.CustomModel.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Accounts.Controllers
{
    public class VoucherController : Controller
    {
        private readonly ICurrency cService = new CurrencyService();
        //
        // GET: /Accounts/Voucher/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(VoucherCustom vc)
        {
            VoucherCustom v = vc;

            return Content("sdfsd");
        }

	}
}