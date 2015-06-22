using Mhasb.Domain.Organizations;
using Mhasb.Services.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Mhasb.Wsit.Web.Admin.Controllers
{
    public class CompaniesController : Controller
    {
        private ICompanyService compSer = new CompanyService();
        // GET: Companies
        public ActionResult Index()
        {
            return View();
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
            List<Company> company = compSer.GetAllCompanies();
            if (!String.IsNullOrEmpty(searchString))
            {
                company = company.Where(s => s.TradingName.Contains(searchString)).ToList();
            }


            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return PartialView(company.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Details(int id) 
        {
            var model = compSer.GetSingleCompany(id);
            return View(model);
        }

        public ActionResult Block() 
        {
            return null;
        }
    }
}