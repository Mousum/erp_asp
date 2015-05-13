using Mhasb.Domain.OrgSettings;
using Mhasb.Services.OrgSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class CurrencyController : Controller
    {
        //
        // GET: /OrgSettings/Currency/
        private ICurrency cService = new CurrencyService();
        //
        // GET: /OrgSettings/Currency/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(cService.GetAllCurrency());
        }

        public ActionResult Create()
        {
            return View();
        }

	
	}
}
