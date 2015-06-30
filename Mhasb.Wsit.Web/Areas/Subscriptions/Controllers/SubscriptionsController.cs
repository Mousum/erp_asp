using Mhasb.Services.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Subscriptions.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly IPackagesServices packService = new PackageServices();
        // GET: Subscriptions/Subscriptions
        public ActionResult Index()
        {
            var model = packService.GetAllPackages();
            return View(model);
        }
    }
}