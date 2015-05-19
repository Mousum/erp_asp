﻿using Mhasb.Domain.OrgSettings;
using Mhasb.Services.Organizations;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;
using Newtonsoft.Json;
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
        private readonly IEmployeeService eService = new EmployeeService();
        private readonly IUserService uService = new UserService();
        private readonly ISettingsService sService = new SettingsService();
        private readonly IAuditor aService = new AuditorService();
        //
        // GET: /OrgSettings/Auditor/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult InternalAuditor()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int companyId = AccSet.Companies.Id;

            var tt = eService.GetEmpByCompanyId(companyId);       
            var dbObj = eService.GetEmpByCompanyId(AccSet.Companies.Id).Select(e => new { empid = e.Id, empname = e.Users.FirstName + e.Users.LastName, designation = e.Designations.DesignationName, email = e.Users.Email }).ToList();
            ViewBag.Title = "Internal Auditor";
            ViewBag.Employees = tt;
            //ViewBag.Employees = eService.GetEmpByCompanyId(AccSet.Companies.Id);
            ViewBag.dataSet = Json(dbObj);
            //ViewBag.Designations = new SelectList( dService.GetDesignations().Select(e => new { Id=e.Id, Name=e.DesignationName }).ToList(),"Id","Name");
            ViewBag.AuditorType = 1;


            var emplist = aService.GetAllAuditorsByCompanyAndType(companyId,EnumAuditorType.Internal);
            ViewBag.Emplist = emplist;
            
            return View("Auditor");
        }


        public ActionResult ExternalAuditor()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int companyId = AccSet.Companies.Id;
            ViewBag.Title = "External Auditor";
            var tt = eService.GetEmpByCompanyId(companyId);
            var dbObj = eService.GetEmpByCompanyId(AccSet.Companies.Id).Select(e => new { empid = e.Id, empname = e.Users.FirstName + e.Users.LastName, designation = e.Designations.DesignationName, email = e.Users.Email }).ToList();

            ViewBag.Employees = tt;
            //ViewBag.Employees = eService.GetEmpByCompanyId(AccSet.Companies.Id);
            ViewBag.dataSet = Json(dbObj);
            //ViewBag.Designations = new SelectList(dService.GetDesignations().Select(e => new { Id = e.Id, Name = e.DesignationName }).ToList(), "Id", "Name");
            ViewBag.AuditorType = 2;

            var emplist = aService.GetAllAuditorsByCompanyAndType(companyId, EnumAuditorType.External);
            ViewBag.Emplist = emplist;

            return View("Auditor");
        }

        [HttpPost]
        public ActionResult AddAuditor(Auditor ad)
        {
            var etype = Request.Form.GetValues("etype")[0] ;
            int type = Int32.Parse(etype);
            if (type == 1)
            {
                ad.AuditorType = EnumAuditorType.Internal;
            }
            else if (type == 2)
            {
                ad.AuditorType = EnumAuditorType.External;
            }
            else
            {
                return Content("Type Problem");
            }
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = sService.GetAllByUserId(user.Id);
            int companyId = AccSet.Companies.Id;
     
            ad.CompanyId = companyId;
            if (aService.AddAuditor(ad))
            {
                return Content("Success");
            }
            else
            {
                return Content("Failed");
            }
        }

	}
}