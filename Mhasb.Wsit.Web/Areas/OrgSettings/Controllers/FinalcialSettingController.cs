using System;
using System.Linq;
using System.Web.Mvc;
using Mhasb.Domain.OrgSettings;
using Mhasb.Services.Organizations;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;

namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class FinalcialSettingController : Controller
    {
        private readonly ICompanyService iCompany = new CompanyService();
        private readonly ICurrency cService = new CurrencyService();
        private readonly IFinalcialSetting fService = new FinalcialSettingService();
        private readonly ISettingsService sService = new SettingsService();
        private readonly IUserService uService = new UserService();
        //
        // GET: /OrgSettings/FinancialSetting/
        public ActionResult Index(int id)
        {
            return View(fService.GetFinalcialSetting(id));
        }
        public ActionResult Create()
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

        [HttpPost]
        public ActionResult Create(FinancialSetting fs)
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            fs.CompanyId = AccSet.Companies.Id;
            fs.IsActive = true;
            fService.AddFinalcialSetting(fs);
            return RedirectToAction("Index", "FinalcialSetting", new { Area = "OrgSettings", id = fs.Id });
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