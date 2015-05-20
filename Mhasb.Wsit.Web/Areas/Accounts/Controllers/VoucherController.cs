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
        private readonly IFinalcialSetting fService = new FinalcialSettingService();
        private readonly IChartOfAccountService coaService = new ChartOfAccountService();
        //
        // GET: /Accounts/Voucher/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
         
            int branchId = 3;


            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach=vService.GetMaxCountByBranchId(branchId) + 1;

            if(maxBrach<1)
                return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);


            var code = "GF-"+branchId.ToString()+"-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            ViewBag.FinancialSettingId = fService.GetCurrentFinalcialSettingByComapny(branchId).Id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;
            voucher.VoucherTypeId = 1;

            voucher.BranchId = 3;
            
            vService.CreateVoucher(voucher);
            List<VoucherDetail> vds = vc.voucherDetails;

            foreach(var vd in vds)
            {
                VoucherDetail voucherDetail = vd;
                vd.VoucherId = voucher.Id;
                try
                {
                    vdService.CreateVoucherDetail(vd);
                }
                catch(Exception)
                {
                    return Content("Voucher Details problem");
                }
            }


            return Content("sdfsd");
        }

        public ActionResult ManualJournals() 
        {
            var model = vService.GetAllVoucherByBranchId(3);
            return View(model);
        }

	}
}