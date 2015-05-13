using System;
using System.Linq;
using System.Web.Mvc;
using Mhasb.Domain.OrgSettings;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;

namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class TaxSettingController : Controller
    {
        private readonly ITaxSetting _taxSettingService=new TaxSettingService();
        private readonly IUserService uService = new UserService();
        private readonly ISettingsService sService = new SettingsService();

        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /OrgSettings/TaxSetting/
        public ActionResult Create()
        {
            var periodList  = Enum.GetValues(typeof(EnumFinalcialPeriod))
                                    .Cast<EnumFinalcialPeriod>()
                                    .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                    .ToList();
            ViewBag.PeriodList = new SelectList(periodList,"Id","Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaxSetting taxSetting)
        {
            try
            {
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                var AccSet = sService.GetAllByUserId(user.Id);
                taxSetting.CompanyId = Int32.Parse( AccSet.Companies.Id.ToString());

                if (_taxSettingService.AddTaxSetting(taxSetting))
                    return Content("Data insert Successfully");
                else
                    return Content("Data insert Failed");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        public ActionResult Edit(int id)
        {
            var periodList = Enum.GetValues(typeof(EnumFinalcialPeriod))
                                    .Cast<EnumFinalcialPeriod>()
                                    .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                    .ToList();
            ViewBag.PeriodList = new SelectList(periodList, "Name", "Name");
            return View(_taxSettingService.GeTaxSetting(id));
        }


        [HttpPost]
        public ActionResult Edit(TaxSetting taxSetting)
        {
            try
            {
                if(_taxSettingService.UpdateTaxSetting(taxSetting))
                    return Content("Data Update Successfully");
                else
                    return Content("Data Update Failed");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        public ActionResult Details(int id)
        {

            var taxObj = _taxSettingService.GeTaxSetting(id);

            return View(taxObj);
        }

	}
}