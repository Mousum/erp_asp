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
using Mhasb.Wsit.Web.Controllers;
using Mhasb.Services.Loggers;

namespace Mhasb.Wsit.Web.Areas.Accounts.Controllers
{
    public class VoucherController : BaseController
    {
        private readonly ICurrency _cService = new CurrencyService();
        private readonly IVoucherService _vService = new VoucherService();
        private readonly IVoucherDetailService _vdService = new VoucherDetailService();
        private readonly IFinalcialSetting _fService = new FinalcialSettingService();
        private readonly IChartOfAccountService _coaService = new ChartOfAccountService();
        private readonly ISettingsService _sService = new SettingsService();
        private readonly IUserService _uService = new UserService();
        private readonly IVoucherType _voucherType = new VoucherTypeService();

        private readonly IEmployeeService _empService = new EmployeeService();

        private readonly IVoucherDocument _vDocSar = new VoucherDocumentService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        //
        // GET: /Accounts/Voucher/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult NewJournal()
        {

            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);
            ViewBag.User = user.FirstName + "  " + user.LastName;
            if (AccSet == null)
            {
                //  return Content("Please add company financial settings ");
                TempData.Add("errMsg", "Please Go To Your Account Seetings to set Default Company");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }

            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }

            //int branchId = AccSet.Companies.Id;
            int branchId = companyId;

            string str = "G";

            ViewBag.CurrencyList = new SelectList(_cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = _vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
            {
                maxBrach = 1;
            }

            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");
            var tt = _coaService.GetAllChartOfAccountByCompanyId(branchId);

            ViewBag.coaList = _coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 3);


            var code = "Gj-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = _fService.GetCurrentFinalcialSettingByComapny(branchId);
            if (fsObj == null) 
            {
                TempData.Add("errMsg", "Please add company financial settings");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }   
            
            
            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult NewJournal(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;
            // 

            var vtObj = _voucherType.GetVoucherTypeByCode("01");
            if (vtObj != null)
            {
                voucher.VoucherTypeId = vtObj.Id; 
            }
            else
            {
                TempData.Add("errMsg", "Please Add voucher type as New Journal with 01 Code.");
                return RedirectToAction("NewJournal", "Voucher", new { area = "Accounts" });
            }
            // This EmpId is static must be changed by Emp table 

            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);

            var empObj = _empService.GetEmployeeByUserIdAndCompanyId(user.Id,(int)AccSet.CompanyId);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "User must be a employee for this Transaction.");
                return RedirectToAction("NewJournal", "Voucher", new { area = "Accounts" });
                //   return Content("User must be a employee for this Transaction.");
            }

            
            if (AccSet.CompanyId != null) voucher.BranchId = (int)AccSet.CompanyId;
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
            if (_vService.CreateVoucher(voucher))
            {
                List<VoucherDetail> vds = vc.voucherDetails;

                foreach (var voucherDetail in vds)
                {
                    voucherDetail.VoucherId = voucher.Id;
                    try
                    {
                        if (!_vdService.CreateVoucherDetail(voucherDetail))
                        {
                            TempData.Add("errMsg", "One or more Voucher details could not added Successfully");
                            return RedirectToAction("NewJournal", "Voucher", new { area = "Accounts" });
                            //return Content("One or more Voucher details could not added Successfully");
                        }

                    }
                    catch (Exception)
                    {
                        TempData.Add("errMsg", "Voucher Details problem");
                        return RedirectToAction("NewJournal", "Voucher", new { area = "Accounts" });
                        //  return Content("Voucher Details problem");
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
                        VDoc.EmployeeId = empObj.Id;
                        VDoc.VoucherId = voucher.Id;
                        _vDocSar.AddDocument(VDoc);
                    }
                }

                string documentName;
                string documentLocation;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if ("documentLocation[]" == Request.Files.GetKey(i))
                    {
                        documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                        documentLocation = Server.MapPath("~/Uploads/"+AccSet.Companies.DisplayName.Replace(" ","_")+"/VoucherDocuments/");
                        if (fileUpload(Request.Files[i], documentName, documentLocation))
                        {
                            VoucherDocument VDoc = new VoucherDocument();
                            VDoc.CreatedDate = DateTime.Now;
                            VDoc.DocumentType = "File";
                            VDoc.EmployeeId = empObj.Id;
                            VDoc.VoucherId = voucher.Id;
                            VDoc.FileLocation = "Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/" + documentName;
                            _vDocSar.AddDocument(VDoc);
                        }
                    }

                }





            }

            else
            {
                TempData.Add("errMsg", "Failed To Add Voucher.");
                return RedirectToAction("NewJournal", "Voucher", new { area = "Accounts" });
                // return Content("Failed To Add Voucher!!!!");
            }


            TempData.Add("SucMasg", "Journal Added Successfully");
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
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);
            ViewBag.User = user.FirstName + "  " + user.LastName;
            if (AccSet == null)
            {
                TempData.Add("errMsg", "Please Go To Your Account Seetings to set Default Company");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
                
            }

            int branchId = AccSet.Companies.Id;

            string str = "D";

            ViewBag.CurrencyList = new SelectList(_cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = _vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            // ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);
            ViewBag.coaList = _coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 4);


            var code = "Dr-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = _fService.GetCurrentFinalcialSettingByComapny(branchId);
            if (fsObj == null) 
            {
                TempData.Add("errMsg", "Please add company financial settings");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });

            }
            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult DebitVoucher(VoucherCustom vc)
        {
            
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            var vtObj = _voucherType.GetVoucherTypeByCode("02");
            if (vtObj != null)
            {
                voucher.VoucherTypeId = vtObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "Please Add voucher type as Debit Voucher with 02 Code.");
                return RedirectToAction("DebitVoucher", "Voucher", new { area = "Accounts" });
            }


            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);

            var empObj = _empService.GetEmployeeByUserIdAndCompanyId(user.Id, (int)AccSet.CompanyId);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "User must be a employee for this Transaction.");
                return RedirectToAction("DebitVoucher", "Voucher", new { area = "Accounts" });
                //   return Content("User must be a employee for this Transaction.");
            }

           

            if (AccSet.CompanyId != null) voucher.BranchId = (int)AccSet.CompanyId;
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
            if (_vService.CreateVoucher(voucher))
            {
                List<VoucherDetail> vds = vc.voucherDetails;

                foreach (var voucherDetail in vds)
                {
                    voucherDetail.VoucherId = voucher.Id;
                    try
                    {
                        if (!_vdService.CreateVoucherDetail(voucherDetail))
                        {
                            TempData.Add("errMsg", "One or more Voucher details could not added Successfully");
                            return RedirectToAction("DebitVoucher", "Voucher", new { area = "Accounts" });
                        }
                        //return Content("One or more Voucher details could not added Successfully");
                    }
                    catch (Exception)
                    {
                        // return Content("Voucher Details problem");
                        TempData.Add("errMsg", "Voucher Details problem");
                        return RedirectToAction("DebitVoucher", "Voucher", new { area = "Accounts" });
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
                        VDoc.EmployeeId = empObj.Id;
                        VDoc.VoucherId = voucher.Id;
                        _vDocSar.AddDocument(VDoc);
                    }
                }

                string documentName;
                string documentLocation;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if ("documentLocation[]" == Request.Files.GetKey(i))
                    {
                        documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                        documentLocation = Server.MapPath("~/Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/");
                        if (fileUpload(Request.Files[i], documentName, documentLocation))
                        {
                            VoucherDocument VDoc = new VoucherDocument();
                            VDoc.CreatedDate = DateTime.Now;
                            VDoc.DocumentType = "File";
                            VDoc.EmployeeId = empObj.Id;
                            VDoc.VoucherId = voucher.Id;
                            VDoc.FileLocation = "Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/" + documentName;
                            _vDocSar.AddDocument(VDoc);
                        }
                    }

                }
            }

            else
            {
                TempData.Add("errMsg", "Voucher Could Not Be Added");
                return RedirectToAction("DebitVoucher", "Voucher", new { area = "Accounts" });
            }


            TempData.Add("SucMasg", "Voucher  Added Successfully");
            return RedirectToAction("ManualJournals");
        }
        //Credit Voucher
        public ActionResult CreditVoucher()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);
            ViewBag.User = user.FirstName + "  " + user.LastName;
            if (AccSet == null)
            {
                TempData.Add("errMsg", "Please add company financial settings ");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }

            int branchId = AccSet.Companies.Id;

            // int branchId = 2;

            string str = "C";

            ViewBag.CurrencyList = new SelectList(_cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = _vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            //ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);
            ViewBag.coaList = _coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 4);


            var code = "Cr-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = _fService.GetCurrentFinalcialSettingByComapny(branchId);
            if (fsObj == null)
            {
                TempData.Add("errMsg", "Please add company financial settings");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }
            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult CreditVoucher(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            var vtObj = _voucherType.GetVoucherTypeByCode("03");
            if (vtObj != null)
            {
                voucher.VoucherTypeId = vtObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "Please Add voucher type as Credit Voucher with 03 Code.");
                return RedirectToAction("CreditVoucher", "Voucher", new { area = "Accounts" });
            }

            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);

            var empObj = _empService.GetEmployeeByUserIdAndCompanyId(user.Id, (int)AccSet.CompanyId);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "User must be a employee for this Transaction.");
                return RedirectToAction("CreditVoucher", "Voucher", new { area = "Accounts" });
                //return Content("User must be a employee for this Transaction.");
            }

          
            if (AccSet.CompanyId != null) voucher.BranchId = (int)AccSet.CompanyId;
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
            if (_vService.CreateVoucher(voucher))
            {
                List<VoucherDetail> vds = vc.voucherDetails;

                foreach (var voucherDetail in vds)
                {
                    voucherDetail.VoucherId = voucher.Id;
                    try
                    {
                        if (!_vdService.CreateVoucherDetail(voucherDetail))
                        {
                            TempData.Add("errMsg", "One or more Voucher details could not added Successfully");
                            return RedirectToAction("CreditVoucher", "Voucher", new { area = "Accounts" });
                        }
                        // return Content("One or more Voucher details could not added Successfully");
                    }
                    catch (Exception)
                    {
                        TempData.Add("errMsg", "Voucher Details problem");
                        return RedirectToAction("CreditVoucher", "Voucher", new { area = "Accounts" });
                        // return Content("Voucher Details problem");
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
                            VDoc.EmployeeId = empObj.Id;
                            VDoc.VoucherId = voucher.Id;
                            _vDocSar.AddDocument(VDoc);
                        }
                    }

                    string documentName;
                    string documentLocation;
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if ("documentLocation[]" == Request.Files.GetKey(i))
                        {
                            documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                            documentLocation = Server.MapPath("~/Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/");
                            if (fileUpload(Request.Files[i], documentName, documentLocation))
                            {
                                VoucherDocument VDoc = new VoucherDocument();
                                VDoc.CreatedDate = DateTime.Now;
                                VDoc.DocumentType = "File";
                                VDoc.EmployeeId = empObj.Id;
                                VDoc.VoucherId = voucher.Id;
                                VDoc.FileLocation = "Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/" + documentName;
                                _vDocSar.AddDocument(VDoc);
                            }
                        }

                    }
                }
            }

            else
            {
                TempData.Add("errMsg", "Failed To Add Voucher");
                return RedirectToAction("CreditVoucher", "Voucher", new { area = "Accounts" });
                // return Content("Failed To Add Voucher!!!!");
            }


            TempData.Add("SucMasg", "Voucher Added Successfully");
            return RedirectToAction("ManualJournals");
        }
        // Account Voucher


        public ActionResult AccountVoucher()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);
            ViewBag.User = user.FirstName + "  " + user.LastName;
            if (AccSet == null)
            {
                TempData.Add("errMsg", "Please add company financial settings ");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }

            int branchId = AccSet.Companies.Id;

            string str = "O";

            ViewBag.CurrencyList = new SelectList(_cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = _vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            //ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);
            ViewBag.coaList = _coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 4);


            var code = "Op-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = _fService.GetCurrentFinalcialSettingByComapny(branchId);
            if (fsObj == null) 
            {
                TempData.Add("errMsg", "Please add company financial settings");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }
            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }

        [HttpPost]
        public ActionResult AccountVoucher(VoucherCustom vc)
        {
            // openning voucher

            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            var vtObj = _voucherType.GetVoucherTypeByCode("04");
            if (vtObj != null)
            {
                voucher.VoucherTypeId = vtObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "Please Add voucher type as Openning Voucher with 04 Code.");
                return RedirectToAction("AccountVoucher", "Voucher", new { area = "Accounts" });
            }


            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);

            var empObj = _empService.GetEmployeeByUserIdAndCompanyId(user.Id, (int)AccSet.CompanyId);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "User must be a employee for this Transaction.");
                return RedirectToAction("AccountVoucher", "Voucher", new { area = "Accounts" });
                // return Content("User must be a employee for this Transaction.");
            }

           

            if (AccSet.CompanyId != null) voucher.BranchId = (int)AccSet.CompanyId;
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
            if (_vService.CreateVoucher(voucher))
            {
                List<VoucherDetail> vds = vc.voucherDetails;

                foreach (var voucherDetail in vds)
                {
                    voucherDetail.VoucherId = voucher.Id;
                    try
                    {
                        if (!_vdService.CreateVoucherDetail(voucherDetail))
                        {
                            TempData.Add("errMsg", "One or more Voucher details could not added Successfully");
                            return RedirectToAction("AccountVoucher", "Voucher", new { area = "Accounts" });
                        }
                        //  return Content("One or more Voucher details could not added Successfully");
                    }
                    catch (Exception)
                    {
                        TempData.Add("errMsg", "Voucher Details problem");
                        return RedirectToAction("AccountVoucher", "Voucher", new { area = "Accounts" });
                        // return Content("Voucher Details problem");
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
                        VDoc.EmployeeId = empObj.Id;
                        VDoc.VoucherId = voucher.Id;
                        _vDocSar.AddDocument(VDoc);
                    }
                }

                string documentName;
                string documentLocation;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if ("documentLocation[]" == Request.Files.GetKey(i))
                    {
                        documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                        documentLocation = Server.MapPath("~/Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/");
                        if (fileUpload(Request.Files[i], documentName, documentLocation))
                        {
                            VoucherDocument VDoc = new VoucherDocument();
                            VDoc.CreatedDate = DateTime.Now;
                            VDoc.DocumentType = "File";
                            VDoc.EmployeeId = empObj.Id;
                            VDoc.VoucherId = voucher.Id;
                            VDoc.FileLocation = "Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/"+ documentName;
                            _vDocSar.AddDocument(VDoc);
                        }
                    }

                }
            }

            else
            {
                TempData.Add("errMsg", "Failed To Add Voucher");
                return RedirectToAction("AccountVoucher", "Voucher", new { area = "Accounts" });
                //  return Content("Failed To Add Voucher!!!!");
            }

            TempData.Add("SucMasg", "Voucher Added Successfully");
            return RedirectToAction("ManualJournals");
        }
        //Repeating Journal
        public ActionResult RepeatingJournal()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);
            if (AccSet == null)
            {
                TempData.Add("errMsg", "Please add company financial settings ");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }

            int branchId = AccSet.Companies.Id;

            // int branchId = 2;

            string str = "C";

            ViewBag.CurrencyList = new SelectList(_cService.GetAllCurrency(), "Id", "Name");
            long maxBrach = _vService.CountByBranchIdAndPrefix(branchId, str) + 1;

            if (maxBrach < 1)
                maxBrach = 1;
            //return Content("Referencing Problem. No Branch found of your Company. Please Create a company First");

            //ViewBag.coaList = coaService.GetAllChartOfAccountByCompanyId(branchId);
            ViewBag.coaList = _coaService.GetAllChartOfAccountByCompanyId(branchId).Where(c => c.Level == 4);


            var code = "RJ-" + branchId.ToString() + "-" + maxBrach.ToString().PadLeft(5, '0') + "-" + DateTime.Now.ToString("yy");
            ViewBag.RefferenceNo = code;
            var fsObj = _fService.GetCurrentFinalcialSettingByComapny(branchId);
            if (fsObj == null)
            {
                TempData.Add("errMsg", "Please add company financial settings");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }
            ViewBag.FinancialSettingId = fsObj.Id;
            return View();
        }
        [HttpPost]
        public ActionResult RepeatingJournal(VoucherCustom vc)
        {
            VoucherCustom v = vc;
            Voucher voucher = vc.voucher;

            var vtObj = _voucherType.GetVoucherTypeByCode("05");
            if (vtObj != null)
            {
                voucher.VoucherTypeId = vtObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "Please Add voucher type as RepeatingJournal with 05 Code.");
                return RedirectToAction("RepeatingJournal", "Voucher", new { area = "Accounts" });
            }


            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);

            var empObj = _empService.GetEmployeeByUserIdAndCompanyId(user.Id, (int)AccSet.CompanyId);
            if (empObj != null)
            {
                voucher.EmployeeId = empObj.Id;
            }
            else
            {
                TempData.Add("errMsg", "User must be a employee for this Transaction.");
                return RedirectToAction("RepeatingJournal", "Voucher", new { area = "Accounts" });
                //return Content("User must be a employee for this Transaction.");
            }

           
            if (AccSet.CompanyId != null) voucher.BranchId = (int)AccSet.CompanyId;
            if (Request.Form["post"] != null)
            {
                voucher.Posted = 1;
            }
            else if (Request.Form["draft"] != null)
            {
                voucher.Posted = 0;
            }
            voucher.BillNo = Request.Form["billnop1"] + " " + Request.Form["billnop2"]; //I have no Idea why??
            if (_vService.CreateVoucher(voucher))
            {
                List<VoucherDetail> vds = vc.voucherDetails;

                foreach (var voucherDetail in vds)
                {
                    voucherDetail.VoucherId = voucher.Id;
                    try
                    {
                        if (!_vdService.CreateVoucherDetail(voucherDetail))
                        {
                            TempData.Add("errMsg", "One or more Voucher details could not added Successfully");
                            return RedirectToAction("RepeatingJournal", "Voucher", new { area = "Accounts" });

                        }
                          //  return Content("One or more Voucher details could not added Successfully");
                    }
                    catch (Exception)
                    {
                        TempData.Add("errMsg", "Voucher Details problem");
                        return RedirectToAction("RepeatingJournal", "Voucher", new { area = "Accounts" });
                        //return Content("Voucher Details problem");
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
                        VDoc.EmployeeId = empObj.Id;
                        VDoc.VoucherId = voucher.Id;
                        _vDocSar.AddDocument(VDoc);
                    }
                }

                string documentName;
                string documentLocation;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if ("documentLocation[]" == Request.Files.GetKey(i))
                    {
                        documentName = "Document_" + voucher.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                        documentLocation = Server.MapPath("~/Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/");
                        if (fileUpload(Request.Files[i], documentName, documentLocation))
                        {
                            VoucherDocument VDoc = new VoucherDocument();
                            VDoc.CreatedDate = DateTime.Now;
                            VDoc.DocumentType = "File";
                            VDoc.EmployeeId =  empObj.Id;
                            VDoc.VoucherId = voucher.Id;
                            VDoc.FileLocation = "Uploads/" + AccSet.Companies.DisplayName.Replace(" ", "_") + "/VoucherDocuments/"+ documentName;
                            _vDocSar.AddDocument(VDoc);
                        }
                    }

                }
            }

            else
            {
                TempData.Add("errMsg", "Failed To Add Voucher");
                return RedirectToAction("RepeatingJournal", "Voucher", new { area = "Accounts" });
                //return Content("Failed To Add Voucher!!!!");
            }

            TempData.Add("SucMasg", "Voucher Added Successfully");
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


            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = _sService.GetAllByUserId(user.Id);
            if (AccSet == null)
            {
                TempData.Add("errMsg", "Please add company financial settings ");
                return RedirectToAction("Index", "Voucher", new { area = "Accounts" });
            }
            int branchId = AccSet.Companies.Id;
            List<Voucher> Voucher = _vService.GetAllVoucherByBranchId(branchId);
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
            var model = _vService.GetSingleVoucher(id);
            ViewBag.data = model;
            return PartialView("_manualJournalDetails", model);
        }

        public ActionResult GeneralLedgerSettings()
        {
            return View("ledgersettings");
        }
    }
}