using Mhasb.Domain.Accounts;
using Mhasb.Services.Accounts;
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
        private readonly IVoucherService vService = new VoucherService();
        private readonly IVoucherDetailService vdService = new VoucherDetailService();
        //
        // GET: /Accounts/Voucher/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            long branchId = 3;


            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            var code = "GF-"+branchId.ToString()+"-" + (vService.GetMaxCountByBranchId(branchId) + 1).ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            return View();
        }

        [HttpPost]
        public ActionResult Create(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;
            voucher.BillDate = DateTime.Now;


            voucher.BranchId = 3;
            
            vService.CreateVoucher(voucher);
            List<VoucherDetail> vds = vc.voucherDetails;


            return Content("sdfsd");
        }

	}
}