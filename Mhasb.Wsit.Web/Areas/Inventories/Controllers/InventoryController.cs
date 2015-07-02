using Mhasb.Services.Accounts;
using Mhasb.Services.Commons;
using Mhasb.Services.Inventories;
using Mhasb.Services.Loggers;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Inventories.Controllers
{
    public class InventoryController : BaseController
    {
        private readonly IUserService _uService = new UserService();
        private readonly IItemService _itemService = new ItemService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ISettingsService _sService = new SettingsService();
        private readonly IChartOfAccountService _coaService = new ChartOfAccountService();
        private readonly ILookupService _luSer = new LookupService();
        //
        // GET: /Inventories/Inventory/
        public ActionResult Index()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
            var items = _itemService.GetItemsByCompanyId(companyId);
            return View(items);
        }
    }
}