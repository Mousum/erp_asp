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


        private readonly ICompanyProfile iCP = new CompanyProfileService();
        private readonly IContactDetail iCD = new ContactDetailService();

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
                ViewBag.IndustryList = new SelectList(iIndustry.GetAllIndustries(), "Id", "IndustryName");
                ViewBag.CountryList = new SelectList(iCountry.GetAllCountries(), "Id", "CountryName");
                ViewBag.LanguageList = new SelectList(iLang.GetAllLanguages(), "Id", "LanguageName");
                ViewBag.TimeZoneList = new SelectList(iTimeZone.GetAllAreaTimes(), "Id", "ZoneName");
                ViewBag.LegalEntityList = new SelectList(iLegalEntity.GetAllLegalEntities(), "Id", "LegalEntityName");
                return View("Registration");
            }
            else
                return Redirect("~/");
            
        }
        [HttpPost]
        public ActionResult Registraion(Company company)
        {
            
            HttpPostedFileBase logo = Request.Files["logoImage"];
            HttpPostedFileBase seal = Request.Files["sealImage"];
            //HttpPostedFileBase doc = Request.Files["documentLocation[]"];


            int companyTableId = iCompany.GetMaxId() + 1;
            string sealName = "Seal_" + company.TradingName.Replace(" ", "_") + "_" + companyTableId.ToString()+"_" + Path.GetRandomFileName() + ".png";
            string sealLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
      
           
            string logoName = "Logo_" + company.TradingName.Replace(" ", "_") + "_" + companyTableId.ToString() + "_" + Path.GetRandomFileName() + ".png";
            string logoLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
            String msg;
            string documentName;
            string documentLocation;

            if ((fileUpload(logo, logoName, logoLocation) && fileUpload(seal, sealName, sealLocation)))
            {
                

                company.Email = HttpContext.User.Identity.Name;
                company.LogoLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + logoName;
                company.SealLocation= "Uploads/" + company.TradingName.Replace(" ", "_")+"/"+sealName;

                var tt = uService.GetSingleUserByEmail(company.Email);
                company.Users = new User { Id = tt.Id, Email = tt.Email };
                try
                {
                    if (iCompany.AddCompany(company))
                    {
                        msg = "Success";

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            if ("documentLocation[]" == Request.Files.GetKey(i))
                            {
                                documentName = "Document_" + company.TradingName.Replace(" ", "_") + "_" + companyTableId.ToString() + "_" + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                                documentLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
                                if (fileUpload(Request.Files[i], documentName, documentLocation))
                                {
                                    CompanyDocument cd = new CompanyDocument();
                                    cd.CompanyId = company.Id;
                                    cd.DocumentLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + documentName;
                                    iCompanyDocument.AddCompanyDocument(cd);
                                }
                            }

                        }
                    }
                    else
                        msg = "Registration Failed";

                }
                catch (Exception)
                {
                    msg = "Failed";
                }


                
            }
            else
                msg= "Failed";

            ViewBag.msg = msg;

            return RedirectToAction("MyMhasb", "Users", new { Area="UserManagement"});
        }



        public ActionResult update()
        {
            int id = 3;

            
            var company=iCompany.GetSingleCompany(id);




            ViewBag.IndustryList = new SelectList(iIndustry.GetAllIndustries(), "Id", "IndustryName");
            ViewBag.CountryList = new SelectList(iCountry.GetAllCountries(), "Id", "CountryName");
            ViewBag.LanguageList = new SelectList(iLang.GetAllLanguages(), "Id", "LanguageName");
            ViewBag.TimeZoneList = new SelectList(iTimeZone.GetAllAreaTimes(), "Id", "ZoneName");
            ViewBag.LegalEntityList = new SelectList(iLegalEntity.GetAllLegalEntities(), "Id", "LegalEntityName");

            return View("update",company);
        }

        [HttpPost]
        public ActionResult Update(Company company )
        {
            HttpPostedFileBase logo = Request.Files["logoImage"];
            HttpPostedFileBase seal = Request.Files["sealImage"];
            //HttpPostedFileBase doc = Request.Files["documentLocation[]"];
            if (logo.ContentLength > 0)
            {
                string logoName = company.LogoLocation.Split('/').Last();
                string logoLocation = company.LogoLocation;
                logoLocation = Path.GetDirectoryName(logoLocation);// logoLocation.TrimEnd('\\');
                //logoLocation = logoLocation.Remove(logoLocation.LastIndexOf('\\') + 1);
                logoLocation = Server.MapPath("~/" +logoLocation+"/");
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
                    if (System.IO.File.Exists(tempPath+logoName))
                    {
                        System.IO.File.Delete(tempPath+logoName);
                    }
                }
                else
                {
                    System.IO.File.Move( tempPath + logoName, logoLocation + logoName);
                    return Content("Problem in logo Replacing");
                }
            }

            if (seal.ContentLength > 0)
            {
                string sealName = company.SealLocation.Split('/').Last();
                string sealLocation = company.SealLocation;
                sealLocation = Path.GetDirectoryName(sealLocation); ;
                sealLocation = Server.MapPath("~/" + sealLocation+"/");
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
                }
                else
                {
                    System.IO.File.Move(tempPath + sealName, sealLocation + sealName);
                    return Content("Problem in Seal Replacing");
                }
            }

            
            String msg;
            string documentName;
            string documentLocation;

            try
            {
                if (iCompany.UpdateCompany(company))
                {
                    msg = "Success";

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if ("documentLocation[]" == Request.Files.GetKey(i))
                        {
                            documentName = "Document_" + company.TradingName.Replace(" ", "_") +  "_" + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                            documentLocation = Server.MapPath("~/Uploads/" + company.TradingName.Replace(" ", "_") + "/");
                            if (fileUpload(Request.Files[i], documentName, documentLocation))
                            {
                                CompanyDocument cd = new CompanyDocument();
                                cd.CompanyId = company.Id;
                                cd.DocumentLocation = "Uploads/" + company.TradingName.Replace(" ", "_") + "/" + documentName;
                                iCompanyDocument.AddCompanyDocument(cd);
                            }
                        }

                    }
                }
                else
                    msg = "Registration Failed";

            }
            catch (Exception)
            {
                msg = "Failed";
            }

            ViewBag.msg = msg;

            return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
        }

        public string deleteCompanyDocument(int id)
        {
            var companyDocument = iCompanyDocument.GetCompanyDocumentById(id);
            if (iCompanyDocument.DeleteCompanyDocument(id))
            {
                
                string prevFile= Request.MapPath("~/" + companyDocument.DocumentLocation);
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
            CompanyProfileCustom cpc = iCP.GetCompanyProfile(2);
            if (cpc != null)
                return View("EditProfile", cpc);
            return View();
        }

        [HttpPost]
        public ActionResult AddProfile(CompanyProfileCustom companyProfileCustom)
        {


            try
            {
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                //HttpPostedFileBase doc = Request.Files["documentLocation[]"];

                string profilePicName = "Company_" + "_" + user.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                string profilePicLocation = Server.MapPath("~/Uploads/CompanyProfiles/");
                if (fileUpload(profilePic, profilePicName, profilePicLocation))
                {
                    CompanyProfile cp = new CompanyProfile();
                    cp = companyProfileCustom.companyProfile;
                    cp.UserId = user.Id;

                    /// Static CompanyId Will dynamic next day 
                    var myCompany=iCompany.GetSingleCompany(2);
                    cp.Companies = new Company { Id=myCompany.Id};



                    cp.ImageLocation = "Uploads/CompanyProfiles/" + profilePicName;
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

                            iCD.AddContactDetail(phone);
                            iCD.AddContactDetail(fax);
                            iCD.AddContactDetail(website);
                            iCD.AddContactDetail(facebook);
                            iCD.AddContactDetail(twitter);
                            iCD.AddContactDetail(google);
                            iCD.AddContactDetail(linkedin);
                            iCD.AddContactDetail(skype);
                        }
                        catch (Exception e)
                        {
                            var tt = e;
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }




            return View();
        }



        public ActionResult UpdateProfile(CompanyProfileCustom companyProfileCustom) 
        {
            try
            {
                CompanyProfile cp = new CompanyProfile();
                cp = companyProfileCustom.companyProfile;
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                if (profilePic.ContentLength > 0)
                {
                    string prevImage = Request.MapPath("~/" + cp.ImageLocation);

                    string profilePicName = "Employee_" + "_" + user.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                    string profilePicLocation = Server.MapPath("~/Uploads/CompanyProfiles/");
                    if (fileUpload(profilePic, profilePicName, profilePicLocation))
                    {
                        cp.ImageLocation = "Uploads/EmployeeProfiles/" + profilePicName;
                        if (System.IO.File.Exists(prevImage))
                        {
                            System.IO.File.Delete(prevImage);
                        }
                    }
                    else
                    {
                        return Content("Photo Upload Unsuccessfull!!!...");
                    }


                }

                if (iCP.UpdateCompanyProfile(cp))
                {
                    try
                    {
                        iCD.UpdateContactDetail(companyProfileCustom.Phone);
                        iCD.UpdateContactDetail(companyProfileCustom.Fax);
                        iCD.UpdateContactDetail(companyProfileCustom.Website);
                        iCD.UpdateContactDetail(companyProfileCustom.Facebook);
                        iCD.UpdateContactDetail(companyProfileCustom.Twitter);
                        iCD.UpdateContactDetail(companyProfileCustom.Google);
                        iCD.UpdateContactDetail(companyProfileCustom.LinkedIn);
                        iCD.UpdateContactDetail(companyProfileCustom.Skype);
                    }
                    catch (Exception ex)
                    {
                        return Content("One or more Contact Field Updating Unsuccessfull!!!!");
                    }

                }
                else
                {
                    return Content("Profile Updating cannot done successfully!!!!");
                }
            }
            catch (Exception ex)
            {
                return Content("Failed");
            }
            return Content("Success");
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
            catch(Exception e)
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
                string[] fileExtensions = { ".bmp", ".jpg", ".png", ".jpeg", ".pdf", ".doc", ".txt", ".docx" , ".xlsx" };
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