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
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using PagedList;

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

        private readonly IVoucherDocument vDocSar = new VoucherDocumentService();
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
            return View();
        }

        [HttpPost]
        public ActionResult NewJournal(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            voucher.VoucherTypeId = 1;
            // This EmpId is static must be changed by Emp table 

            //string data = Request.Form["note_data"];



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
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
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
                string data = Request.Form["note_data"];

                if (Request.Form["note_data"] != "")
                {
                    JArray noteData = JArray.Parse(data);

                    var VDoc = new VoucherDocument();
                    for (int i = 0; i < noteData.Count(); i++)
                    {

                        VDoc.CreatedDate = Convert.ToDateTime(noteData[i]["date"].ToString());
                        VDoc.Description = noteData[i]["des"].ToString();
                        VDoc.DocumentType = noteData[i]["type"].ToString();
                        VDoc.EmployeeId = voucher.EmployeeId;
                        VDoc.VoucherId = voucher.Id;
                        vDocSar.AddDocument(VDoc);
                    }
                }
               
                    string documentName;
                    string documentLocation;
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if ("documentLocation[]" == Request.Files.GetKey(i))
                        {
                            documentName = "Document_" + voucher.Id+"_"+i+ Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                            documentLocation = Server.MapPath("~/Uploads/VoucherDocuments/");
                            if (fileUpload(Request.Files[i], documentName, documentLocation))
                            {
                                VoucherDocument VDoc = new VoucherDocument();
                                VDoc.CreatedDate = DateTime.Now;
                                VDoc.DocumentType = "File";
                                VDoc.EmployeeId = voucher.EmployeeId;
                                VDoc.VoucherId = voucher.Id;
                                VDoc.FileLocation = documentLocation + "/" + documentName;
                                vDocSar.AddDocument(VDoc);
                            }
                        }

                    }
                
               



            }

            else
            {
                return Content("Failed To Add Voucher!!!!");
            }



            return RedirectToAction("ManualJournals");
        }

        public bool fileUpload(HttpPostedFileBase file, string fileName, string filePath)
        {
            try
            {

                string uploadPath = filePath;
                bool isValid = false;
                string[] fileExtensions = { ".bmp", ".jpg", ".png", ".jpeg", ".pdf", ".doc", ".txt", ".docx", ".xlsx" };
                for (int i = 0; i < fileExtensions.Length; i++)
                {

                    if (file.FileName.Contains(fileExtensions[i]))
                    {

                        isValid = true;
                        break;
                    }
                }

                if (isValid)
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    file.SaveAs(uploadPath + fileName);


                    return true;
                    //System.IO.File.Move(uploadPath + file.FileName, uploadPath + fileName + ".png");
                    //return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
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
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
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
                string data = Request.Form["note_data"];

                if (Request.Form["note_data"] != "")
                {
                    JArray noteData = JArray.Parse(data);

                    var VDoc = new VoucherDocument();
                    for (int i = 0; i < noteData.Count(); i++)
                    {

                        VDoc.CreatedDate = Convert.ToDateTime(noteData[i]["date"].ToString());
                        VDoc.Description = noteData[i]["des"].ToString();
                        VDoc.DocumentType = noteData[i]["type"].ToString();
                        VDoc.EmployeeId = voucher.EmployeeId;
                        VDoc.VoucherId = voucher.Id;
                        vDocSar.AddDocument(VDoc);
                    }
                }

                string documentName;
                string documentLocation;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if ("documentLocation[]" == Request.Files.GetKey(i))
                    {
                        documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                        documentLocation = Server.MapPath("~/Uploads/VoucherDocuments/");
                        if (fileUpload(Request.Files[i], documentName, documentLocation))
                        {
                            VoucherDocument VDoc = new VoucherDocument();
                            VDoc.CreatedDate = DateTime.Now;
                            VDoc.DocumentType = "File";
                            VDoc.EmployeeId = voucher.EmployeeId;
                            VDoc.VoucherId = voucher.Id;
                            VDoc.FileLocation = documentLocation + "/" + documentName;
                            vDocSar.AddDocument(VDoc);
                        }
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
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
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
                    string data = Request.Form["note_data"];

                    if (Request.Form["note_data"] != "")
                    {
                        JArray noteData = JArray.Parse(data);

                        var VDoc = new VoucherDocument();
                        for (int i = 0; i < noteData.Count(); i++)
                        {

                            VDoc.CreatedDate = Convert.ToDateTime(noteData[i]["date"].ToString());
                            VDoc.Description = noteData[i]["des"].ToString();
                            VDoc.DocumentType = noteData[i]["type"].ToString();
                            VDoc.EmployeeId = voucher.EmployeeId;
                            VDoc.VoucherId = voucher.Id;
                            vDocSar.AddDocument(VDoc);
                        }
                    }

                    string documentName;
                    string documentLocation;
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if ("documentLocation[]" == Request.Files.GetKey(i))
                        {
                            documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                            documentLocation = Server.MapPath("~/Uploads/VoucherDocuments/");
                            if (fileUpload(Request.Files[i], documentName, documentLocation))
                            {
                                VoucherDocument VDoc = new VoucherDocument();
                                VDoc.CreatedDate = DateTime.Now;
                                VDoc.DocumentType = "File";
                                VDoc.EmployeeId = voucher.EmployeeId;
                                VDoc.VoucherId = voucher.Id;
                                VDoc.FileLocation = documentLocation + "/" + documentName;
                                vDocSar.AddDocument(VDoc);
                            }
                        }

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
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
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
                string data = Request.Form["note_data"];

                if (Request.Form["note_data"] != "")
                {
                    JArray noteData = JArray.Parse(data);

                    var VDoc = new VoucherDocument();
                    for (int i = 0; i < noteData.Count(); i++)
                    {

                        VDoc.CreatedDate = Convert.ToDateTime(noteData[i]["date"].ToString());
                        VDoc.Description = noteData[i]["des"].ToString();
                        VDoc.DocumentType = noteData[i]["type"].ToString();
                        VDoc.EmployeeId = voucher.EmployeeId;
                        VDoc.VoucherId = voucher.Id;
                        vDocSar.AddDocument(VDoc);
                    }
                }

                string documentName;
                string documentLocation;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if ("documentLocation[]" == Request.Files.GetKey(i))
                    {
                        documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                        documentLocation = Server.MapPath("~/Uploads/VoucherDocuments/");
                        if (fileUpload(Request.Files[i], documentName, documentLocation))
                        {
                            VoucherDocument VDoc = new VoucherDocument();
                            VDoc.CreatedDate = DateTime.Now;
                            VDoc.DocumentType = "File";
                            VDoc.EmployeeId = voucher.EmployeeId;
                            VDoc.VoucherId = voucher.Id;
                            VDoc.FileLocation = documentLocation + "/" + documentName;
                            vDocSar.AddDocument(VDoc);
                        }
                    }

                }
            }

            else
            {
                return Content("Failed To Add Voucher!!!!");
            }


            return RedirectToAction("ManualJournals");
        }
        //Repeating Journal
        public ActionResult RepeatingJournal()
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


            var code = "RJ-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = fService.GetCurrentFinalcialSettingByComapny(branchId);

            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }
        [HttpPost]
        public ActionResult RepeatingJournal(VoucherCustom vc)
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
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
            voucher.BillNo = Request.Form["billnop1"] +" "+Request.Form["billnop2"]; //I have no Idea why??
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
                string data = Request.Form["note_data"];

                if (Request.Form["note_data"] != "")
                {
                    JArray noteData = JArray.Parse(data);

                    var VDoc = new VoucherDocument();
                    for (int i = 0; i < noteData.Count(); i++)
                    {

                        VDoc.CreatedDate = Convert.ToDateTime(noteData[i]["date"].ToString());
                        VDoc.Description = noteData[i]["des"].ToString();
                        VDoc.DocumentType = noteData[i]["type"].ToString();
                        VDoc.EmployeeId = voucher.EmployeeId;
                        VDoc.VoucherId = voucher.Id;
                        vDocSar.AddDocument(VDoc);
                    }
                }

                string documentName;
                string documentLocation;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if ("documentLocation[]" == Request.Files.GetKey(i))
                    {
                        documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                        documentLocation = Server.MapPath("~/Uploads/VoucherDocuments/");
                        if (fileUpload(Request.Files[i], documentName, documentLocation))
                        {
                            VoucherDocument VDoc = new VoucherDocument();
                            VDoc.CreatedDate = DateTime.Now;
                            VDoc.DocumentType = "File";
                            VDoc.EmployeeId = voucher.EmployeeId;
                            VDoc.VoucherId = voucher.Id;
                            VDoc.FileLocation = documentLocation + "/" + documentName;
                            vDocSar.AddDocument(VDoc);
                        }
                    }

                }
            }

            else
            {
                return Content("Failed To Add Voucher!!!!");
            }


            return RedirectToAction("ManualJournals");
        }
        public ActionResult ManualJournals(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            if (AccSet == null)
            {
                return Content("Please add company financial settings ");
            }
            int branchId = AccSet.Companies.Id;
            List<Voucher> Voucher = vService.GetAllVoucherByBranchId(branchId);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            short searchPosted = 0;
            if (searchString == "draft")
            {
                searchPosted = 0;
            }
            else
            {
                searchPosted = 1;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                Voucher = Voucher.Where(s => s.Posted == searchPosted).ToList();
            }
            switch (sortOrder)
            {
                case "Date":
                    Voucher = Voucher.OrderBy(s => s.VoucherDate).ToList();
                    break;
                case "date_desc":
                    Voucher = Voucher.OrderByDescending(s => s.VoucherDate).ToList();
                    break;
                default:
                    Voucher = Voucher.OrderBy(s => s.Id).ToList();
                    break;
            }
            return View(Voucher.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public PartialViewResult GetManualJournalDetails(int id)
        {
            var model = vService.GetSingleVoucher(id);
            ViewBag.data = model;
            return PartialView("_manualJournalDetails", model);
        }

        public ActionResult GeneralLedgerSettings()
        {
            return View("ledgersettings");
        }
    }
}