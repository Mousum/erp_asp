using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Services.Commons;
using Mhasb.Services.Loggers;
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
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        // GET: OrganizationManagement/Founders
        public ActionResult Index()
        {
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            var companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            //var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            //var AccSet = setService.GetAllByUserId(user.Id);
            if (logObj != null) 
            {
                var model = fService.GetFounders(companyId);
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
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            var companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            founder.CompanyId = companyId;

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
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            var companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            var FounderList = fService.GetFounders(companyId);
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

                TempData.Add("Msg", "Share Transffered Successfully");
                return RedirectToAction("Index");
            }

            TempData.Add("errMsg", "Transfer Failed!");
            return RedirectToAction("Index");
        }



    }
}