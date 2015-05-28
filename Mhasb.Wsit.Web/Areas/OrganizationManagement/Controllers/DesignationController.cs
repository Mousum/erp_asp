using Mhasb.Domain.Organizations;
using Mhasb.Services.Organizations;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.OrganizationManagement.Controllers
{
    public class DesignationController : BaseController
    {
        private readonly IDesignation iDesignation = new DesignationService();
        //
        // GET: /OrganizationManagement/Ddesignation/
        public ActionResult Index()
        {
            return View(iDesignation.GetDesignations());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Designation ds)
        {
            if (iDesignation.AddDesignation(ds))
                return RedirectToAction("Index", "Designation", new { Area = "OrganizationManagement" });
            else
            {
                ModelState.AddModelError("msg", "Designation did not inserted successfully");
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(iDesignation.GetSingleDesignationById(id));
        }

        [HttpPost]
        public ActionResult Edit(Designation ds)
        {
            if (iDesignation.UpdateDesignation(ds))
                return RedirectToAction("Index", "Designation", new { Area = "OrganizationManagement" });
            else
            {
                ModelState.AddModelError("msg", "Designation did not Updated successfully");
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]

        public string DeleteConfirmed(int id)
        {
            if (iDesignation.DeleteDesignation(id))
            {
                return "Success";
            }
            return "Failed";
            //return RedirectToAction("Index");
        }
	}
}