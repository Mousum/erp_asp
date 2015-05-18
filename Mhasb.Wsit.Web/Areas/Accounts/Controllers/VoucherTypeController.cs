using Mhasb.Domain.Accounts;
using Mhasb.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Accounts.Controllers
{
    public class VoucherTypeController : Controller
    {
        private readonly IVoucherType vtService = new VoucherTypeService();
        //
        // GET: /Accounts/VoucherType/
        public ActionResult Index()
        {
            return View(vtService.GetAllVoucherType());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VoucherType vt)
        {
            string msg = "Failed To add Voucher Type";
            if (vtService.AddVoucherType(vt))
                msg = "Successfully added.";

            ModelState.AddModelError("msg",msg);

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            return View(vtService.GetVoucherTypeById(id));
        }

        [HttpPost]
        public ActionResult Edit(VoucherType vt)
        {
            string msg = "Failed To Update Voucher Type";
            if (vtService.UpdateVoucherType(vt))
                msg = "Successfully Updated.";

            ModelState.AddModelError("msg", msg);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            string msg = "Failed To Delete Voucher Type";
            if (vtService.DeleteVoucherTypeById(id))
                msg = "Successfully Deleted.";

            ModelState.AddModelError("msg", msg);

            return RedirectToAction("Index");
        }

	}
}