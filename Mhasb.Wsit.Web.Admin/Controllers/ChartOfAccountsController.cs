using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mhasb.Domain.Accounts;
using Mhasb.Wsit.DAL.Data;
using Mhasb.Services.Accounts;

namespace Mhasb.Wsit.Web.Admin.Controllers
{
    public class ChartOfAccountsController : Controller
    {
        private WsDbContext db = new WsDbContext();
        private IChartOfAccountService chser = new ChartOfAccountService();
        // GET: ChartOfAccounts
        public ActionResult Index()
        {
            var chartOfAccounts = chser.GetAllChartOfAccount();
            return View(chartOfAccounts.ToList());
        }

        // GET: ChartOfAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChartOfAccount chartOfAccount = db.ChartOfAccounts.Find(id);
            if (chartOfAccount == null)
            {
                return HttpNotFound();
            }
            return View(chartOfAccount);
        }

        // GET: ChartOfAccounts/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "TradingName");
            ViewBag.TaxId = new SelectList(db.Lookups, "Id", "LookupType");
            return View();
        }

        // POST: ChartOfAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyId,TaxId,ACode,AName,Description,ShowInDashboard,ShowInExpenseClaims,IsCostCenter,Level")] ChartOfAccount chartOfAccount)
        {
            if (ModelState.IsValid)
            {
                chser.AddChartOfAccount(chartOfAccount);
                //db.ChartOfAccounts.Add(chartOfAccount);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "TradingName", chartOfAccount.CompanyId);
            ViewBag.TaxId = new SelectList(db.Lookups, "Id", "LookupType", chartOfAccount.TaxId);
            return View(chartOfAccount);
        }

        // GET: ChartOfAccounts/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChartOfAccount chartOfAccount = chser.GetSingleChartOfAccount(id);
            if (chartOfAccount == null)
            {
                return HttpNotFound();
            }
           
            return View(chartOfAccount);
        }

        // POST: ChartOfAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyId,TaxId,ACode,AName,Description,ShowInDashboard,ShowInExpenseClaims,IsCostCenter,Level")] ChartOfAccount chartOfAccount)
        {
            if (ModelState.IsValid)
            {
                chser.UpdateChartOfAccount(chartOfAccount);
                return RedirectToAction("Index");
            }
            
            return View(chartOfAccount);
        }

       

        // POST: ChartOfAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
      
        public string DeleteConfirmed(int id)
        {
            if (chser.DeleteChartOfAccount(id))
            {
                return "Success";
            }
            return "Failed";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
