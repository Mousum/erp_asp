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


        public ActionResult NewJournal()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            if (AccSet == null)
            {
                return Content("Please add company financial settings ");
            }

            int branchId = AccSet.Companies.Id;

            string str = "G";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1) {
                maxBrach = 1;
            }
               
                //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");
            var tt = coaService.GetAllChartOfAccountByCompanyId(branchId);

            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c=>c.Level==4);


            var code = "Gj-"+branchId.ToString()+"-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = fService.GetCurrentFinalcialSettingByComapny(branchId);

            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult NewJournal(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            voucher.VoucherTypeId = 1;
            // This EmpId is static must be changed by Emp table 
            voucher.EmployeeId = 2;

            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = 6;
            

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



        //DebitVoucher
        public ActionResult DebitVoucher()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = 3;// AccSet.Companies.Id;

            // int branchId = 2;

            string str = "D";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);


            var code = "Dr-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
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
            // This EmpId is static must be changed by Emp table 
            voucher.EmployeeId = 2;

            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = 6;


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
        //Credit Voucher
        public ActionResult CreditVoucher()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = 3;// AccSet.Companies.Id;

            // int branchId = 2;

            string str = "C";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);


            var code = "Cr-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = fService.GetCurrentFinalcialSettingByComapny(branchId);

            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult CreditVoucher(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            voucher.VoucherTypeId = 1;
            // This EmpId is static must be changed by Emp table 
            voucher.EmployeeId = 2;

            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = 6;


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
        // Account Voucher


        public ActionResult AccountVoucher()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = 3;// AccSet.Companies.Id;

            // int branchId = 2;

            string str = "O";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);


            var code = "Op-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = fService.GetCurrentFinalcialSettingByComapny(branchId);

            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult AccountVoucher(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            voucher.VoucherTypeId = 1;
            // This EmpId is static must be changed by Emp table 
            voucher.EmployeeId = 2;

            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int branchId = 6;


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