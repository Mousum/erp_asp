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
    public class FoundersController : BaseController
    {
        private readonly IFounder fService = new FounderService();
        private readonly ICountryService iCountry = new CountryService();
        private readonly ILanguageService iLang = new LanguageService();
        private ISettingsService setService = new SettingsService();
        private IUserService uService = new UserService();
        private readonly IShareTransferService stService = new ShareTransferService();
        // GET: OrganizationManagement/Founders
        public ActionResult Index()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            if (AccSet != null) 
            { 
                 var model = fService.GetFounders(AccSet.Companies.Id);
                 return View(model); 
            }
            else
            {
                return RedirectToAction("MyMhasb", "Users", new { area = "Usermanagement" });
            }
           
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
                TempData.Add("Msg", "Founder Added Successfully!");
                return RedirectToAction("Index");
            }
            else 
            {
                TempData.Add("Msg", "Founder Add Failed!");
                return RedirectToAction("Index");
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
              
                TempData.Add("Msg", "Founder Updated Successfully!");
                return RedirectToAction("Index");
            }
            else
            {
               
                TempData.Add("errMsg", "Founder Update Failed!");
               return RedirectToAction("Index");
            }
        }

        public ActionResult ShareTransfer()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            int comId = AccSet.Companies.Id;
            var FounderList=fService.GetFounders(comId);
            ViewBag.FounderList = FounderList;



            var dbObj = FounderList.Select(e => new { fid = e.Id,shares=e.SharesOwned,tel=e.Tel,fax=e.Fax,pobox=e.PoBoax,email=e.Email,totalval=(e.ShareUnitValue*e.SharesOwned),residence=e.FounderResidence,nationality=e.Countries.CountryName,language=e.Languages.LanguageName }).ToList();

            //ViewBag.Employees = eService.GetEmpByCompanyId(AccSet.Companies.Id);
            ViewBag.dataSet = Json(dbObj);

            return View();
        }

        [HttpPost]
        public ActionResult ShareTransfer(ShareTransfer st)
        {
            var s = st;
            st.TransferTime = DateTime.Now;
            if (stService.AddShareTransferTransection(st))
            {
                var sender = fService.GetSingleFounder(st.FromFounderId);
                sender.SharesOwned -= st.TransferAmount;
                fService.UpdateFounder(sender);

                var reciever = fService.GetSingleFounder(st.ToFounderId);
                reciever.SharesOwned += st.TransferAmount;
                fService.UpdateFounder(reciever);

                return Content("Success");
            }
                
            return Content("Failed");
        }



    }
}