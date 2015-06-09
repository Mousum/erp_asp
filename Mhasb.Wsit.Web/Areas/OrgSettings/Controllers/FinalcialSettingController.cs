using System;
using System.Linq;
using System.Web.Mvc;
using Mhasb.Domain.OrgSettings;
using Mhasb.Services.Organizations;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Controllers;
using Mhasb.Services.Loggers;

namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class FinalcialSettingController : BaseController
    {
        private readonly ICompanyService iCompany = new CompanyService();
        private readonly ICurrency cService = new CurrencyService();
        private readonly IFinalcialSetting fService = new FinalcialSettingService();
        private readonly ISettingsService sService = new SettingsService();
        private readonly IUserService uService = new UserService();
        private readonly ICompanyService comService = new CompanyService();

        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        //
        // GET: /OrgSettings/FinancialSetting/
        public ActionResult Index(int id)
        {
            var fs=fService.GetFinalcialSetting(id);
            if (fs != null)
                return View(fs);
            else
                TempData.Add("errMsg","Financial Settings not found");
            return RedirectToAction("Create");
        }
        public ActionResult Create()
        {

            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            if (logObj.Companies.CompleteFlag <= 5 && logObj.Companies.CompleteFlag >= 1)
            {
                int yearDiff = DateTime.Now.Year - 1929;
                var list = Enumerable.Range(1930, yearDiff).ToList().Select(r => new
                {
                    Id = r,
                    Name = r
                });
                ViewBag.yearList = new SelectList(list, "Id", "Name");


                var periodList = Enum.GetValues(typeof(EnumFinalcialPeriod))
                                        .Cast<EnumFinalcialPeriod>()
                                        .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                        .ToList();
                ViewBag.PeriodList = new SelectList(periodList, "Id", "Name");


                ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
                return View();
            }
            else
            {
                string absUrl;
                if (!checkCompanyFlow(out absUrl))
                {
                    return Redirect(absUrl);
                }
                TempData.Add("errMsg", "Something Wrong...");
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            }
        }

        [HttpPost]
        public ActionResult Create(FinancialSetting fs)
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);

            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            fs.CompanyId = companyId;
            fs.IsActive = true;
            if (fService.AddFinalcialSetting(fs))
            {
                int flag=2;
                comService.UpdateCompleteFlag(companyId,flag);
            }

            return RedirectToAction("Create", "Invitations", new { Area = "NotificationManagement" });
        }

        public ActionResult Edit(int id)
        {
            int yearDiff = DateTime.Now.Year - 1929;
            var list = Enumerable.Range(1930, yearDiff).ToList().Select(r => new
            {
                Id = r,
                Name = r
            });
            ViewBag.yearList = new SelectList(list, "Id", "Name");


            var periodList = Enum.GetValues(typeof(EnumFinalcialPeriod))
                                    .Cast<EnumFinalcialPeriod>()
                                    .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                    .ToList();
            ViewBag.PeriodList = new SelectList(periodList, "Name", "Name");


            ViewBag.CurrencyList = new SelectList(cService.GetAllCurrency(), "Id", "Name");
            var obj= fService.GetFinalcialSetting(id);
            //obj.FinalcialPeriod = obj.FinalcialPeriod.GetHashCode();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(FinancialSetting fs)
        {
            fs.IsActive = true;
            fService.UpdateFinalcialSetting(fs);
            //return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            return RedirectToAction("Index", "FinalcialSetting", new { Area = "OrgSettings" ,id=fs.Id});
        }

	}
}