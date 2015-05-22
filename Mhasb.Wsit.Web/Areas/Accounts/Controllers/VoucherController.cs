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
using Mhasb.Services.Organizations;

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

        private readonly IEmployeeService empService = new EmployeeService();
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
            ViewBag.User = user.FirstName + "  " + user.LastName;
            if (AccSet == null)
            {
                return Content("Please add company financial settings ");
            }

            int branchId = AccSet.Companies.Id;

            string str = "G";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
            {
                maxBrach = 1;
            }

            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");
            var tt = coaService.GetAllChartOfAccountByCompanyId(branchId);

            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 4);


            var code = "Gj-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = fService.GetCurrentFinalcialSettingByComapny(branchId);

            ViewBag.FinancialSettingId = fsObj.Id;
            return View("NewJournal_new");
        }

        [HttpPost]
        public ActionResult NewJournal(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            voucher.VoucherTypeId = 1;
            // This EmpId is static must be changed by Emp table 
            

            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var empObj = empService.GetEmployeeByUserId(user.Id);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;    
            }
            else
            {
                return Content("User must be a employee for this Transaction.");
            }
            
            var AccSet = sService.GetAllByUserId(user.Id);

            if (AccSet.CompanyId != null) voucher.BranchId = (int)AccSet.CompanyId;

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



            return RedirectToAction("ManualJournals");
        }



        //DebitVoucher
        public ActionResult DebitVoucher()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            if (AccSet == null)
            {
                return Content("Please add company financial settings ");
            }

            int branchId = AccSet.Companies.Id;

            string str = "D";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

           // ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);
            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 4);


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

            voucher.VoucherTypeId = 3;
            // This EmpId is static must be changed by Emp table 
          

            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var empObj = empService.GetEmployeeByUserId(user.Id);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;
            }
            else
            {
                return Content("User must be a employee for this Transaction.");
            }
            var AccSet = sService.GetAllByUserId(user.Id);
            if (AccSet.CompanyId != null) voucher.CurrencyId = (int)AccSet.CompanyId;


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



            return RedirectToAction("ManualJournals");
        }
        //Credit Voucher
        public ActionResult CreditVoucher()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            if (AccSet == null)
            {
                return Content("Please add company financial settings ");
            }

            int branchId = AccSet.Companies.Id;

            // int branchId = 2;

            string str = "C";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            //ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);
            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 4);


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

            voucher.VoucherTypeId = 2;
            // This EmpId is static must be changed by Emp table 
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var empObj = empService.GetEmployeeByUserId(user.Id);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;
            }
            else
            {
                return Content("User must be a employee for this Transaction.");
            }
            var accSet = sService.GetAllByUserId(user.Id);
            if (accSet.CompanyId != null) voucher.CurrencyId = (int)accSet.CompanyId;


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



            return RedirectToAction("ManualJournals");
        }
        // Account Voucher


        public ActionResult AccountVoucher()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            if (AccSet == null)
            {
                return Content("Please add company financial settings ");
            }

            int branchId = AccSet.Companies.Id;

            string str = "O";

            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            //ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);
            ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 4);


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

            voucher.VoucherTypeId = 4;
            // This EmpId is static must be changed by Emp table 
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var empObj = empService.GetEmployeeByUserId(user.Id);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;
            }
            else
            {
                return Content("User must be a employee for this Transaction.");
            }
            var accSet = sService.GetAllByUserId(user.Id);
            if (accSet.CompanyId != null) voucher.CurrencyId = (int)accSet.CompanyId;


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



            return RedirectToAction("ManualJournals");
        }
        public ActionResult ManualJournals() 
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            if (AccSet == null)
            {
                return Content("Please add company financial settings ");
            }

            int branchId = AccSet.Companies.Id;


            var model = vService.GetAllVoucherByBranchId(branchId);
            return View(model);
        }
        [HttpPost]
        public PartialViewResult GetManualJournalDetails(int id) 
        {
            var model = vService.GetSingleVoucher(id);
            ViewBag.data = model;
            return PartialView("_manualJournalDetails",model);
        }

        public ActionResult GeneralLedgerSettings() 
        {
            return View("ledgersettings");
        }
	}
}