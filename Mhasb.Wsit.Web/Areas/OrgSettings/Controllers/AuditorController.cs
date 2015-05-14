using Mhasb.Services.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.OrgSettings.Controllers
{
    public class AuditorController : Controller
    {
        private readonly IDesignation dService = new DesignationService();
        //
        // GET: /OrgSettings/Auditor/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult InternalAuditor()
        {
            ViewBag.Designations = dService.GetDesignations();
            return View("Auditor");
        }

	}
}