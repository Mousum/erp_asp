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
    public class FoundersController : Controller
    {
        private readonly IFounder fService = new FounderService();
        private readonly ICountryService iCountry = new CountryService();
        private readonly ILanguageService iLang = new LanguageService();
        private ISettingsService setService = new SettingsService();
        private IUserService uService = new UserService();
        // GET: OrganizationManagement/Founders
        public ActionResult Index()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            var model = fService.GetFounders(AccSet.Companies.Id);
            return View(model);
        }

        public ActionResult Settings() 
        {

            ViewBag.CountryList = new SelectList(iCountry.GetAllCountries(), "Id", "CountryName");
            ViewBag.LanguageList = new SelectList(iLang.GetAllLanguages(), "Id", "LanguageName");
            return View();
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(Founder founder)
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            founder.CompanyId = AccSet.Companies.Id;

            if (fService.AddFounder(founder))
            {
                return Content("success");
            }
            else 
            {
                return Content("Failed");
            }
          

        }
        public ActionResult Update(int id)
        {
            ViewBag.CountryList = new SelectList(iCountry.GetAllCountries(), "Id", "CountryName");
            ViewBag.LanguageList = new SelectList(iLang.GetAllLanguages(), "Id", "LanguageName");
            var model = fService.GetSingleFounder(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Founder founder)
        {
           if (fService.UpdateFounder(founder))
            {
                return Content("success");
            }
            else
            {
                return Content("Failed");
            }
        }
    }
}