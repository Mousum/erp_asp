﻿using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.OrganizationManagement.Controllers
{
    public class EmployeeController : BaseController
    {
        //
        // GET: /OrganizationManagement/Employee/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /OrganizationManagement/Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /OrganizationManagement/Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /OrganizationManagement/Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /OrganizationManagement/Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /OrganizationManagement/Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /OrganizationManagement/Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /OrganizationManagement/Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
