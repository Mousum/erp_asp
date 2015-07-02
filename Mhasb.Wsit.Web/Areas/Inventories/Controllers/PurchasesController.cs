using System.Linq;
using System.Web.Mvc;
using Mhasb.Services.Accounts;
using Mhasb.Services.Commons;
using Mhasb.Services.Contact;
using Mhasb.Services.Inventories;
using Mhasb.Services.Loggers;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;
using Mhasb.Domain.Inventories;
using System;

namespace Mhasb.Wsit.Web.Areas.Inventories.Controllers
{
    public class PurchasesController : Controller
    {
        private IContactInformationService _contactService = new ContactInformationService();
        private readonly IItemService _itemService = new ItemService();
        private readonly ICurrency _currencycService = new CurrencyService();
        private readonly IUserService _uService = new UserService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ISettingsService _sService = new SettingsService();
        private readonly IChartOfAccountService _coaService = new ChartOfAccountService();
        private readonly ILookupService _luSer = new LookupService();

        //
        // GET: /Inventories/Purchases/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditBill()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            //var accSet = _sService.GetAllByUserId(user.Id);
            //ViewBag.User = user.FirstName + "  " + user.LastName;
            //if (accSet == null)
            //{
            //    //  return Content("Please add company financial settings ");
            //    TempData.Add("errMsg", "Please Go To Your Account Seetings to set Default Company");
            //    return RedirectToAction("Index", "Purchases", new { area = "Inventories" });
            //}
            
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            var  companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;

            var coalist = _coaService.GetAllChartOfAccountByComIdCostCentre(companyId);
            ViewBag.CoaList = coalist;

            var currencyList = _currencycService.GetAllCurrency();
            ViewBag.CurrencyList = new SelectList(currencyList, "Id", "Name");

            var amountsareenum = Enum.GetValues(typeof(EnumTransactionType))
                                        .Cast<EnumTransactionType>()
                                        .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                        .ToList();
            ViewBag.AmountsareList = new SelectList(amountsareenum, "Id", "Name");

            var lookups = _luSer.GetLookupByType("Tax");//.Select(u => new { u.Id, TValue = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = lookups;

            



            return View();
        }
        public ActionResult PartialAddAccount()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            ViewBag.CompanyCompleteFlag = logObj.Companies.CompleteFlag;

            // var AccSet = setService.GetAllByUserId(user.Id);
            int companyId = 0;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (logObj != null)
            {
                if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
            }
            var atypes = _coaService.GetAllChartOfAccountByCompanyIdForSecondLevel(companyId);
           

            if (atypes.Count == 0)
            {
                _coaService.AddBaseAccountTypes();
                atypes = _coaService.GetAllChartOfAccountByCompanyId(companyId);
            }
            var lookups = _luSer.GetLookupByType("Tax").Select(u => new { Id = u.Id, Value = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = new SelectList(lookups, "Id", "Value");

            ViewBag.ATypes = atypes;
            return PartialView("_AddAccount");
        }

        public ActionResult RepeatTransection()
         {

             return View();
         }
        public JsonResult GetServiceNames(string term) {
            var results = _contactService.GetAllContactInformation().Where(s => term == null || s.ContactName.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Id, value = x.ContactName }).Take(5).ToList();
            
            return Json(results, JsonRequestBehavior.AllowGet);
          
        }
        public PartialViewResult ItemRow()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);

            

            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;



            var coalist = _coaService.GetAllChartOfAccountByComIdCostCentre(companyId);
            ViewBag.CoaList = coalist;

            var itemList = _itemService.GetAllItems();
            ViewBag.Items = itemList;
            var lookups = _luSer.GetLookupByType("Tax");//.Select(u => new { u.Id, TValue = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = lookups;


            return PartialView();
        }

        public ActionResult GetjsonItem(int Id)
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
            var itemList = _itemService.GetAllItemsByConmanyId(companyId, Id)
                .Select(u => new {
                    Id=u.Id,
                    ItemCode=u.ItemCode,
                    ItemName=u.ItemName,
                    Description=u.PurchaseDescription,
                    Quantity=u.Quantity,
                    UnitPrice=u.PurchaseUnitPrice,
                    AccountId = u.PurchasesAccount.Id,
                    AccountCode=u.PurchasesAccount.ACode,
                    AccountName = u.PurchasesAccount.AName,
                    PtxtValue=u.PTaxRate.Key,
                    PtxtQuantity =  u.PTaxRate.Quantity,
                });
            if (itemList != null)
            {
                return Json(new { itemList });
            }
            else {
                return Json(new { success = "False" });
            }
            
        }
	}
}