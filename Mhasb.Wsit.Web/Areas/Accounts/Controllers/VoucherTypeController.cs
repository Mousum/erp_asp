using Mhasb.Domain.Accounts;
using Mhasb.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;
using Mhasb.Wsit.CustomModel.Accounts;

namespace Mhasb.Wsit.Web.Areas.Accounts.Controllers
{
    public class VoucherTypeController : Controller
    {
        private readonly IVoucherType vtService = new VoucherTypeService();

        // must be delete before publish
        private readonly ICurrency cService = new CurrencyService();
        private readonly IVoucherService vService = new VoucherService();
        private readonly IVoucherDetailService vdService = new VoucherDetailService();
        private readonly IFinalcialSetting fService = new FinalcialSettingService();
        private readonly IChartOfAccountService coaService = new ChartOfAccountService();
        private readonly ISettingsService sService = new SettingsService();
        private readonly IUserService uService = new UserService();

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



        public ActionResult DebitVoucher()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = AccSet.Companies.Id;

            // int branchId = 2;

            string str = "G";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);


            var code = "Gj-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = fService.GetCurrentFinalcialSettingByComapny(branchId);

            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult DebitVoucher(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            voucher.VoucherTypeId = 1;

            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = AccSet.Companies.Id;

            if (vService.CreateVoucher(voucher))
            {
                List<VoucherDetail> vds = vc.voucherDetails;

                foreach (var voucherDetail in vds)
                {
                    voucherDetail.VoucherId = voucher.Id;
                    try
                    {
                        if (!vdService.CreateVoucherDetail(voucherDetail))
                            return Content("One or more Voucher details could not added Successfully");
                    }
                    catch (Exception)
                    {
                        return Content("Voucher Details problem");
                    }
                }
            }

            else
            {
                return Content("Failed To Add Voucher!!!!");
            }



            return Content("Success");
        }
	}
}