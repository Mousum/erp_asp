using System;
using System.Linq;
using System.Web.Mvc;
using Mhasb.Domain.OrgSettings;
using Mhasb.Services.OrgSettings;

namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class TaxSettingController : Controller
    {
        private readonly ITaxSetting _taxSettingService=new TaxSettingService();

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
                taxSetting.CompanyId = 1;

                _taxSettingService.AddTaxSetting(taxSetting);
                return Content("Data insert Successfully");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        public ActionResult Details()
        {

            var taxObj = _taxSettingService.GeTaxSetting(1);

            return View(taxObj);
        }

        public ActionResult Edit()
        {
            return null;
        }
	}
}