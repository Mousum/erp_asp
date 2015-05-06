using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Services.Commons;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
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
        private readonly ICompanyService iCompany= new CompanyService();
        private readonly IIndustryService iIndustry = new IndustryService();
        private readonly ICountryService iCountry = new CountryService();
        private readonly ILanguageService iLang = new LanguageService();
        private readonly ILegalEntityService iLegalEntity = new LegalEntityService();
        private readonly IAreaTimeService iTimeZone = new AreaTimeService();
        private readonly IUserService uService = new UserService();

        //
        // GET: /OrganizationManagement/Company/
        [AllowAnonymous]
        public ActionResult Index()
        {
 

            var myList= iCompany.GetAllCompanies();

            int j = 1;
            return View("RegistrationResult");
        }



        public ActionResult Registration()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.IndustryList = new SelectList(iIndustry.GetAllIndustries(), "Id", "IndustryName");
                ViewBag.CountryList = new SelectList(iCountry.GetAllCountries(), "Id", "CountryName");
                ViewBag.LanguageList = new SelectList(iLang.GetAllLanguages(), "Id", "LanguageName");
                ViewBag.TimeZoneList = new SelectList(iTimeZone.GetAllAreaTimes(), "Id", "ZoneName");
                ViewBag.LegalEntityList = new SelectList(iLegalEntity.GetAllLegalEntities(), "Id", "LegalEntityName");
                return View();
            }
            else
                return Redirect("~/");
            
        }
        [HttpPost]
        public ActionResult Registraion(Company company)
        {
            
            HttpPostedFileBase logo = Request.Files["logoImage"];
            HttpPostedFileBase seal = Request.Files["sealImage"];
            int companyTableId = iCompany.GetMaxId() + 1;
            string sealName = "Seal_" + company.TradingName.Replace(" ", "_") + "_" + companyTableId.ToString() + ".png";
            //string sealName = "Seal_" + company.TradingName.Replace(" ", "_") +"_" +asd.ToString() + ".png";
            string sealLocation=Server.MapPath("~/Uploads/");
            
            //string logoName = "Logo_" + company.TradingName.Replace(" ", "_") + "_" +iCompany.GetMaxId().ToString() + ".png";
            string logoName = "Logo_" + company.TradingName.Replace(" ", "_") + "_" + companyTableId.ToString() + ".png";
            string logoLocation=Server.MapPath("~/Uploads/");
            String msg;
            //if (imageUpload(logo, logoName, Server.MapPath("~/Uploads/")))
            //    msg = "Logo Success";
            //else
            //    msg = "Logo Failed";
            
            //if (imageUpload(seal, sealName, Server.MapPath("~/Uploads/")))
            //    msg += " Seal Success";
            //else
            //    msg += " Seal Failed";

            if((imageUpload(logo, logoName, logoLocation) && imageUpload(seal, sealName, sealLocation)))
            {
                company.Email = HttpContext.User.Identity.Name;
                company.LogoLocation= logoLocation+"/"+logoName;
                company.SealLocation= sealLocation+"/"+sealName;

                var tt = uService.GetSingleUserByEmail(company.Email);
                company.Users = new User { Id = tt.Id, Email = tt.Email };
                try
                {
                    iCompany.AddCompany(company);
                    msg = "Success";
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
                    }
                }

                if (isValid)
                {
                    string fileName = Path.GetRandomFileName();
                    System.IO.File.Move(uploadPath + file.FileName, uploadPath + fileName + ".png");
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

        public bool imageUpload(HttpPostedFileBase file, string fileName, string filePath)
        {
            try
            {
                string uploadPath = filePath;
                file.SaveAs(uploadPath + file.FileName);

                string[] fileExtensions = { ".bmp", ".jpg", ".png", ".jpeg" };
                bool isValid = false;
                for (int i = 0; i < fileExtensions.Length; i++)
                {

                    if (file.FileName.Contains(fileExtensions[i]))
                    {

                        isValid = true;
                    }
                }

                if (isValid)
                {
                    System.IO.File.Move(uploadPath + file.FileName, uploadPath + fileName + ".png");
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

	}
}