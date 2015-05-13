using Mhasb.Domain.Commons;
using Mhasb.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace Mhasb.Wsit.Web.Areas.Commons.Controllers
{
    public class IndustryController : Controller
    {
        private IIndustryService iService = new IndustryService();

        //
        // GET: /Commons/Industry/
        public ActionResult Index()
        {
            //var model = iService.GetAllIndustries();
            return View("Index1");
        }

        public ActionResult List(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            List<Industry> Industry = iService.GetAllIndustries();
            if (!String.IsNullOrEmpty(searchString))
            {
                Industry = Industry.Where(s => s.IndustryName.Contains(searchString)).ToList();
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return PartialView(Industry.ToPagedList(pageNumber, pageSize));

        }


        //
        // GET: /Commons/Industry/Details/5
        public ActionResult Details(int id)
        {
            var model = iService.GetSingleIndustry(id);
            return View(model);
        }

        //
        // GET: /Commons/Industry/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Commons/Industry/Create
        [HttpPost]
        public ActionResult Create(Industry industry)
        {
            try
            {
                iService.CreateIndustry(industry);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Commons/Industry/Edit/5
        public ActionResult Edit(int id)
        {
            var model = iService.GetSingleIndustry(id);
            return View(model);
        }

        //
        // POST: /Commons/Industry/Edit/5
        [HttpPost]
        public ActionResult Edit(Industry industry)
        {
            try
            {
                iService.UpdateIndustry(industry);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public String DeleteConfirmed(int id)
        {
            try
            {
                iService.DeleteIndustry(id);
                return "Success";
                // return RedirectToAction("Index");
            }
            catch
            {
                return "Failed";
            }
        }
    }
}
