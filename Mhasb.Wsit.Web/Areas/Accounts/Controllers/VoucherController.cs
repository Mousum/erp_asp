using Mhasb.Domain.Accounts;
using Mhasb.Services.Accounts;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;
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
        private readonly IVoucherService vService = new VoucherService();
        private readonly IVoucherDetailService vdService = new VoucherDetailService();
        private readonly IFinalcialSetting fService = new FinalcialSettingService();
        private readonly IChartOfAccountService coaService = new ChartOfAccountService();
        private readonly ISettingsService sService = new SettingsService();
        private readonly IUserService uService = new UserService();
        //
        // GET: /Accounts/Voucher/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
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


            var code = "Gj-"+branchId.ToString()+"-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = fService.GetCurrentFinalcialSettingByComapny(branchId);

            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(VoucherCustom vc)
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

        public ActionResult ManualJournals() 
        {
            var model = vService.GetAllVoucherByBranchId(3);
            return View(model);
        }

	}
}