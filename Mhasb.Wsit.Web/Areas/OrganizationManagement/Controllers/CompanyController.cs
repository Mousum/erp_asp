using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Services.Commons;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using Mhasb.Wsit.CustomModel.Organizations;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.OrganizationManagement.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService iCompany = new CompanyService();

        private readonly ICompanyDocument iCompanyDocument = new CompanyDocumentService();


        private readonly IIndustryService iIndustry = new IndustryService();
        private readonly ICountryService iCountry = new CountryService();
        private readonly ILanguageService iLang = new LanguageService();
        private readonly ILegalEntityService iLegalEntity = new LegalEntityService();
        private readonly IAreaTimeService iTimeZone = new AreaTimeService();
        private readonly IUserService uService = new UserService();
        private readonly ISettingsService sService = new SettingsService();


        private readonly ICompanyProfile iCP = new CompanyProfileService();
        private readonly IContactDetail iCD = new ContactDetailService();

        private readonly IDesignation iDesignation = new DesignationService();
        private readonly IEmployeeService iEmployee = new EmployeeService();

        //
        // GET: /OrganizationManagement/Company/
        //public ActionResult Index()
        //{


        //    var myList= iCompany.GetAllCompanies();

        //    int j = 1;
        //    return View("RegistrationResult");
        //}


        public ActionResult Registration()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                try
                {
                    ViewBag.IndustryList = new SelectList(iIndustry.GetAllIndustries(), "Id", "IndustryName");
                    ViewBag.CountryList = new SelectList(iCountry.GetAllCountries(), "Id", "CountryName");
                    ViewBag.LanguageList = new SelectList(iLang.GetAllLanguages(), "Id", "LanguageName");
                    ViewBag.TimeZoneList = new SelectList(iTimeZone.GetAllAreaTimes(), "Id", "ZoneName");
                    ViewBag.LegalEntityList = new SelectList(iLegalEntity.GetAllLegalEntities(), "Id", "LegalEntityName");
                    return View("Registration");
                }
                catch (Exception ex)
                {
                    TempData.Add("errMsg", "Internal Server Error Regarding Commons Entity. Please Contact with Mhasb Team");
                    return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
                }


            }
            else
                return Redirect("~/");

        }
        [HttpPost]
        public ActionResult Registration(Company company)
        {

            HttpPostedFileBase logo = Request.Files["logoImage"];
            HttpPostedFileBase seal = Request.Files["sealImage"];
            //HttpPostedFileBase doc = Request.Files["documentLocation[]"];


            int companyTableId = iCompany.GetMaxId() + 1;
            string sealName = "Seal_" + company.TradingName.Replace(" ", "_") + "_" + companyTableId.ToString() + "_" + Path.GetRandomFileName() + ".png";
            string sealLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");


            string logoName = "Logo_" + company.TradingName.Replace(" ", "_") + "_" + companyTableId.ToString() + "_" + Path.GetRandomFileName() + ".png";
            string logoLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
            string msg = "", logoUploadMsg = "", sealUploadMsg = "", designationInsertMsg = "", employeeAddMsg = "", documentAddMsg = "", settingMsg = "";
            bool success = false, uploadSuccess = true;
            string documentName;
            string documentLocation;
            if (logo.ContentLength > 1)
            {
                if (fileUpload(logo, logoName, logoLocation))
                {
                    company.LogoLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + logoName;
                }
                else
                {
                    logoUploadMsg = "Problem in logo uploading...Please try Again later..";
                    uploadSuccess = false;
                }
            }


            if (seal.ContentLength > 1)
            {
                if (fileUpload(seal, sealName, sealLocation))
                {
                    company.SealLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + sealName;
                }
                else
                {
                    sealUploadMsg = "Problem in Seal uploading...Please try Again later..";
                    uploadSuccess = false;
                }
            }



            company.Email = HttpContext.User.Identity.Name;
            var tt = uService.GetSingleUserByEmail(company.Email);
            company.Users = new User { Id = tt.Id, Email = tt.Email };
            try
            {
                if (iCompany.AddCompany(company))
                {
                    success = true;
                    var ownerDesignation = iDesignation.GetSingleDesignationByDesignationName("Owner");
                    if (ownerDesignation == null)
                    {
                        Designation ds = new Designation();
                        ds.DesignationName = "Owner";
                        if (iDesignation.AddDesignation(ds))
                            ownerDesignation = ds;
                        else
                        {
                            //designation add failed
                            designationInsertMsg = "Internal Server Error regarding Designation...Please consult with Mhasb Team";
                        }
                    }

                    Employee ownerEmp = new Employee();
                    ownerEmp.UserId = tt.Id;
                    ownerEmp.CompanyId = company.Id;
                    ownerEmp.DesignationId = ownerDesignation.Id;
                    ownerEmp.BranchId = company.Id;
                    if (!iEmployee.CreateEmployee(ownerEmp))
                    {
                        //Employee add failed
                        designationInsertMsg = "Internal Server Error regarding Employee...Please consult with Mhasb Team";
                    }

                    var accountSetting = sService.GetAllByUserId(tt.Id);
                    if (accountSetting != null)
                    {
                        accountSetting.Companies = new Company { Id = company.Id };
                        accountSetting.lgcompany = true;
                        if (!sService.UpdateSettings(accountSetting))
                            settingMsg = "Internal Server Error regarding Account Setting...Please consult with Mhasb Team ";
                    }
                    else
                    {
                        var AccSet = new Settings();
                        AccSet.userId = tt.Id;
                        AccSet.lgdash = false;
                        AccSet.lglast = false;
                        AccSet.CompanyId = company.Id;
                        AccSet.lgcompany = true;
                        if (!sService.AddSettings(AccSet))
                            settingMsg = "Internal Server Error regarding Account Setting...Please consult with Mhasb Team";

                    }

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if ("documentLocation[]" == Request.Files.GetKey(i) && Request.Files.GetKey(i).Length > 1)
                        {
                            documentName = "Document_" + company.TradingName.Replace(" ", "_") + "_" + companyTableId.ToString() + "_" + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                            documentLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
                            if (fileUpload(Request.Files[i], documentName, documentLocation))
                            {
                                CompanyDocument cd = new CompanyDocument();
                                cd.CompanyId = company.Id;
                                cd.DocumentOriginalName = Request.Files[i].FileName;
                                cd.DocumentLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + documentName;
                                if (!iCompanyDocument.AddCompanyDocument(cd))
                                {
                                    documentAddMsg = "One or more document didnot upload successfully";
                                    uploadSuccess = false;
                                }
                            }
                        }

                    }
                    msg = "Success";
                }
                else
                    msg = "Registration Failed";

            }
            catch (Exception)
            {
                msg = "Exception Occured. Please Contact with Mhasb Team";
            }

            TempData.Add("errMsg", msg + logoUploadMsg + sealUploadMsg + documentAddMsg + designationInsertMsg + employeeAddMsg + settingMsg);

            if (success && uploadSuccess)
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            else if (!uploadSuccess)
                return RedirectToAction("update");
            else
                return View();
        }



        public ActionResult update()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int id = AccSet.Companies.Id;

            var company = iCompany.GetSingleCompany(id);

            if (company == null)
            {
                return RedirectToAction("Registration");
            }


            try
            {
                ViewBag.IndustryList = new SelectList(iIndustry.GetAllIndustries(), "Id", "IndustryName");
                ViewBag.CountryList = new SelectList(iCountry.GetAllCountries(), "Id", "CountryName");
                ViewBag.LanguageList = new SelectList(iLang.GetAllLanguages(), "Id", "LanguageName");
                ViewBag.TimeZoneList = new SelectList(iTimeZone.GetAllAreaTimes(), "Id", "ZoneName");
                ViewBag.LegalEntityList = new SelectList(iLegalEntity.GetAllLegalEntities(), "Id", "LegalEntityName");
                return View("update", company);
            }
            catch (Exception ex)
            {
                TempData.Add("errMsg", "Internal Server Error Regarding Commons Entity. Please Contact with Mhasb Team");
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            }
        }

        [HttpPost]
        public ActionResult Update(Company company)
        {
            string msg = "", logoUploadMsg = "", sealUploadMsg = "", documentAddMsg = "";
            bool success = false, uploadSuccess = true;
            HttpPostedFileBase logo = Request.Files["logoImage"];
            HttpPostedFileBase seal = Request.Files["sealImage"];
            //HttpPostedFileBase doc = Request.Files["documentLocation[]"];
            if (logo.ContentLength > 0)
            {
                string logoName, logoLocation;
                if (company.LogoLocation == null)
                {
                    logoName = "Logo_" + company.TradingName.Replace(" ", "_") + "_" + company.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                    logoLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
                }
                else
                {
                    logoName = company.LogoLocation.Split('/').Last();
                    logoLocation = company.LogoLocation;
                    logoLocation = Path.GetDirectoryName(logoLocation);// logoLocation.TrimEnd('\\');
                    //logoLocation = logoLocation.Remove(logoLocation.LastIndexOf('\\') + 1);
                    logoLocation = Server.MapPath("~/" + logoLocation + "/");
                }


                string tempPath = Server.MapPath("~/Uploads/Temp");

                if (System.IO.File.Exists(logoLocation + logoName))
                {
                    if (!Directory.Exists(tempPath))
                    {
                        Directory.CreateDirectory(tempPath);
                    }
                    System.IO.File.Move(logoLocation + logoName, tempPath + logoName);
                }

                if (fileUpload(logo, logoName, logoLocation))
                {
                    if (System.IO.File.Exists(tempPath + logoName))
                    {
                        System.IO.File.Delete(tempPath + logoName);
                    }
                    company.LogoLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + logoName;
                }
                else
                {
                    if (System.IO.File.Exists(tempPath + logoName))
                        System.IO.File.Move(tempPath + logoName, logoLocation + logoName);
                    logoUploadMsg = "Problem in logo uploading...Please try Again later..";
                    uploadSuccess = false;

                }
            }

            if (seal.ContentLength > 0)
            {
                string sealName, sealLocation;
                if (company.SealLocation == null)
                {
                    sealName = "Seal_" + company.TradingName.Replace(" ", "_") + "_" + company.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                    sealLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
                }
                else
                {
                    sealName = company.SealLocation.Split('/').Last();
                    sealLocation = company.SealLocation;
                    sealLocation = Path.GetDirectoryName(sealLocation); ;
                    sealLocation = Server.MapPath("~/" + sealLocation + "/");
                }

                string tempPath = Server.MapPath("~/Uploads/Temp");
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }

                if (System.IO.File.Exists(sealLocation + sealName))
                {
                    System.IO.File.Move(sealLocation + sealName, tempPath + sealName);
                }

                if (fileUpload(seal, sealName, sealLocation))
                {
                    if (System.IO.File.Exists(tempPath + sealName))
                    {
                        System.IO.File.Delete(tempPath + sealName);
                    }
                    company.SealLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + sealName;
                }
                else
                {
                    if (System.IO.File.Exists(tempPath + sealName))
                        System.IO.File.Move(tempPath + sealName, sealLocation + sealName);
                    sealUploadMsg = "Problem in Seal uploading...Please try Again later..";
                    uploadSuccess = false;
                }
            }


            string documentName;
            string documentLocation;

            try
            {
                if (iCompany.UpdateCompany(company))
                {
                    msg = "Success";
                    success = true;

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if ("documentLocation[]" == Request.Files.GetKey(i) && Request.Files.GetKey(i).Length > 1)
                        {
                            documentName = "Document_" + company.TradingName.Replace(" ", "_") + "_" + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                            documentLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
                            if (fileUpload(Request.Files[i], documentName, documentLocation))
                            {
                                CompanyDocument cd = new CompanyDocument();
                                cd.CompanyId = company.Id;
                                cd.DocumentOriginalName = Request.Files[i].FileName;
                                cd.DocumentLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + documentName;
                                if (!iCompanyDocument.AddCompanyDocument(cd))
                                {
                                    documentAddMsg = "One or more document did not upload successfully";
                                    uploadSuccess = false;
                                }


                            }
                        }

                    }
                }
                else
                    msg = "Update Failed";

            }
            catch (Exception)
            {
                msg = "Failed";
            }

            TempData.Add("errMsg", msg + logoUploadMsg + sealUploadMsg + documentAddMsg);

            if (success && uploadSuccess)
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            else if (!uploadSuccess)
                return RedirectToAction("update");
            else
                return RedirectToAction("update");
        }

        public string deleteCompanyDocument(int id)
        {
            var companyDocument = iCompanyDocument.GetCompanyDocumentById(id);
            if (iCompanyDocument.DeleteCompanyDocument(id))
            {

                string prevFile = Request.MapPath("~/" + companyDocument.DocumentLocation);
                if (System.IO.File.Exists(prevFile))
                {
                    System.IO.File.Delete(prevFile);
                }
                return "Successfull;y deleted!!!";
            }
            return "Failed";
        }

        public ActionResult AddProfile()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            CompanyProfileCustom cpc = iCP.GetCompanyProfile(AccSet.Companies.Id);
            if (cpc != null)
                return RedirectToAction("updateProfile");
            return View();
        }


        [HttpPost]
        public ActionResult AddProfile(CompanyProfileCustom companyProfileCustom)
        {
            string proPicErrorMsg = "", ContactAddError = "", msg = "";
            try
            {
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                //HttpPostedFileBase doc = Request.Files["documentLocation[]"];
                int companyTableId = iCompany.GetMaxId() + 1;
                CompanyProfile cp = new CompanyProfile();
                cp = companyProfileCustom.companyProfile;
                cp.UserId = user.Id;


                var AccSet = sService.GetAllByUserId(user.Id);


                var myCompany = iCompany.GetSingleCompany(AccSet.Companies.Id);
                cp.Companies = new Company { Id = myCompany.Id };
                if (profilePic.ContentLength > 1)
                {
                    string profilePicName = "Company_" + "_" + companyTableId.ToString() + "_" + Path.GetRandomFileName() + ".png";
                    string profilePicLocation = Server.MapPath("~/Uploads/CompanyProfiles/");
                    if (fileUpload(profilePic, profilePicName, profilePicLocation))
                    {
                        cp.ImageLocation = "Uploads/CompanyProfiles/" + profilePicName;
                    }
                    else
                    {
                        proPicErrorMsg = "Problem in uploading Image";
                    }
                }


                if (iCP.AddCompanyProfile(cp))
                {
                    try
                    {
                        ContactDetail phone = companyProfileCustom.Phone;
                        phone.CompanyProfileId = cp.Id;

                        ContactDetail fax = companyProfileCustom.Fax;
                        fax.CompanyProfileId = cp.Id;

                        ContactDetail facebook = companyProfileCustom.Facebook;
                        facebook.CompanyProfileId = cp.Id;

                        ContactDetail google = companyProfileCustom.Google;
                        google.CompanyProfileId = cp.Id;

                        ContactDetail linkedin = companyProfileCustom.LinkedIn;
                        linkedin.CompanyProfileId = cp.Id;

                        ContactDetail skype = companyProfileCustom.Skype;
                        skype.CompanyProfileId = cp.Id;

                        ContactDetail twitter = companyProfileCustom.Twitter;
                        twitter.CompanyProfileId = cp.Id;

                        ContactDetail website = companyProfileCustom.Website;
                        website.CompanyProfileId = cp.Id;

                        if (!(iCD.AddContactDetail(phone) && iCD.AddContactDetail(fax) && iCD.AddContactDetail(website) && iCD.AddContactDetail(facebook) && iCD.AddContactDetail(twitter) && iCD.AddContactDetail(google) && iCD.AddContactDetail(linkedin) && iCD.AddContactDetail(skype)))
                            ContactAddError = "One or more contact field adding operation failed";
                    }
                    catch (Exception e)
                    {
                        var tt = e;
                        ContactAddError = "One or more contact field adding operation failed...";
                    }

                }
                else
                {
                    msg = "Company Profile adding failed...";
                }

            }
            catch (Exception ex)
            {
                msg = "Something Wrong!!!Please Try again...";

            }
            TempData.Add("errMsg", msg + proPicErrorMsg + ContactAddError);
            return RedirectToAction("updateProfile");
        }

        public ActionResult updateProfile()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            CompanyProfileCustom cpc = iCP.GetCompanyProfile(AccSet.Companies.Id);
            if (cpc != null)
                return View("EditProfile", cpc);
            else
                return RedirectToAction("AddProfile");
        }

        [HttpPost]
        public ActionResult UpdateProfile(CompanyProfileCustom companyProfileCustom)
        {
            string proPicErrorMsg = "", ContactAddError = "", msg = "";
            try
            {
                CompanyProfile cp = new CompanyProfile();
                cp = companyProfileCustom.companyProfile;
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                if (profilePic.ContentLength > 0)
                {
                    string prevImage = Request.MapPath("~/" + cp.ImageLocation);

                    string profilePicName = "Company_" + "_" + cp.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                    string profilePicLocation = Server.MapPath("~/Uploads/CompanyProfiles/");
                    if (fileUpload(profilePic, profilePicName, profilePicLocation))
                    {
                        cp.ImageLocation = "Uploads/CompanyProfiles/" + profilePicName;
                        if (System.IO.File.Exists(prevImage))
                        {
                            System.IO.File.Delete(prevImage);
                        }
                    }
                    else
                    {
                        proPicErrorMsg = "Photo Upload Unsuccessfull";
                    }

                }

                if (iCP.UpdateCompanyProfile(cp))
                {
                    try
                    {
                        if(!(iCD.UpdateContactDetail(companyProfileCustom.Phone) && iCD.UpdateContactDetail(companyProfileCustom.Fax) && iCD.UpdateContactDetail(companyProfileCustom.Website) && iCD.UpdateContactDetail(companyProfileCustom.Facebook) && iCD.UpdateContactDetail(companyProfileCustom.Twitter) && iCD.UpdateContactDetail(companyProfileCustom.Google) && iCD.UpdateContactDetail(companyProfileCustom.LinkedIn) && iCD.UpdateContactDetail(companyProfileCustom.Skype)))
                            ContactAddError = "One or more Contact Field Updating Unsuccessfull";
                    }
                    catch (Exception ex)
                    {
                        ContactAddError = "One or more Contact Field Updating Unsuccessfull";
                    }

                }
                else
                {
                    msg = "Profile Updating cannot done successfully!!!!";
                }
            }
            catch (Exception ex)
            {
                msg = "Profile Updating cannot done successfully!!!!";
            }
            TempData.Add("errMsg",msg+ContactAddError+proPicErrorMsg);
            return RedirectToAction("UpdateProfile");
        }


        public bool imageUpload(HttpPostedFileBase file)
        {
            try
            {
                string uploadPath = Server.MapPath("~/Uploads/");
                file.SaveAs(uploadPath + file.FileName);

                string[] fileExtensions = { ".bmp", ".jpg", ".png", ".jpeg" };
                bool isValid = false;
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
                    string fileName = Path.GetRandomFileName();
                    System.IO.File.Move(uploadPath + file.FileName, uploadPath + fileName);
                    return true;
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




    }
}