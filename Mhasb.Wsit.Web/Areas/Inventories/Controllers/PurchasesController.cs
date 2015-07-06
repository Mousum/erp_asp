using System.Linq;
using System.Web.Mvc;
using Mhasb.Services.Accounts;
using Mhasb.Services.Commons;
using Mhasb.Services.Contact;
using Mhasb.Services.Inventories;
using Mhasb.Services.Loggers;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;
using Mhasb.Domain.Inventories;
using System;
using Mhasb.Wsit.CustomModel.Inventories;
using Mhasb.Services.Organizations;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web;

namespace Mhasb.Wsit.Web.Areas.Inventories.Controllers
{
    public class PurchasesController : Controller
    {
        private IContactInformationService _contactService = new ContactInformationService();
        private readonly IItemService _itemService = new ItemService();
        private readonly ICurrency _currencycService = new CurrencyService();
        private readonly IUserService _uService = new UserService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ISettingsService _sService = new SettingsService();
        private readonly IChartOfAccountService _coaService = new ChartOfAccountService();
        private readonly ILookupService _luSer = new LookupService();
        private readonly IEmployeeService empSer = new EmployeeService();
        private readonly IPurchaseTransactionDetailService ptdSer = new PurchaseTransactionDetailService();
        private readonly IPurchaseTransactionService ptSer = new PurchaseTransactionService();
        private readonly IPurchaseTransactionDocumentService ptDocSer = new PurchaseTransactionDocumentService();
        //
        // GET: /Inventories/Purchases/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditBill()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            //var accSet = _sService.GetAllByUserId(user.Id);
            //ViewBag.User = user.FirstName + "  " + user.LastName;
            //if (accSet == null)
            //{
            //    //  return Content("Please add company financial settings ");
            //    TempData.Add("errMsg", "Please Go To Your Account Seetings to set Default Company");
            //    return RedirectToAction("Index", "Purchases", new { area = "Inventories" });
            //}

            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;

            var coalist = _coaService.GetAllChartOfAccountByComIdCostCentre(companyId);
            ViewBag.CoaList = coalist;
            ViewBag.User = user.FirstName + " " + user.LastName;
            var currencyList = _currencycService.GetAllCurrency();
            ViewBag.CurrencyList = new SelectList(currencyList, "Id", "Name");

            var amountsareenum = Enum.GetValues(typeof(EnumTransactionType))
                                        .Cast<EnumTransactionType>()
                                        .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                        .ToList();
            ViewBag.AmountsareList = new SelectList(amountsareenum, "Id", "Name");

            var lookups = _luSer.GetLookupByType("Tax");//.Select(u => new { u.Id, TValue = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = lookups;





            return View();
        }
        public ActionResult PartialAddAccount(string ActionFlag)
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            ViewBag.CompanyCompleteFlag = logObj.Companies.CompleteFlag;

            // var AccSet = setService.GetAllByUserId(user.Id);
            int companyId = 0;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (logObj != null)
            {
                if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
            }
            var atypes = _coaService.GetAllChartOfAccountByCompanyIdForSecondLevel(companyId);


            if (atypes.Count == 0)
            {
                _coaService.AddBaseAccountTypes();
                atypes = _coaService.GetAllChartOfAccountByCompanyId(companyId);
            }
            var lookups = _luSer.GetLookupByType("Tax").Select(u => new { Id = u.Id, Value = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.ActionFlag = ActionFlag;
            ViewBag.Lookups = new SelectList(lookups, "Id", "Value");

            ViewBag.ATypes = atypes;
            return PartialView("_AddAccount");
        }

        public ActionResult RepeatTransection()
        {

            return View();
        }
        public JsonResult GetServiceNames(string term)
        {
            var results = _contactService.GetAllContactInformation().Where(s => term == null || s.ContactName.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Id, value = x.ContactName }).Take(5).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);

        }
        public PartialViewResult ItemRow()
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);



            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;



            var coalist = _coaService.GetAllChartOfAccountByComIdCostCentre(companyId);
            ViewBag.CoaList = coalist;

            var itemList = _itemService.GetAllItems();
            ViewBag.Items = itemList;
            var lookups = _luSer.GetLookupByType("Tax");//.Select(u => new { u.Id, TValue = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = lookups;
            Random rnd = new Random();
            ViewBag.rowId = rnd.Next(100);

            return PartialView();
        }

        public ActionResult GetjsonItem(int Id)
        {
            var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            var companyId = 0;
            if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
            var itemList = _itemService.GetAllItemsByConmanyId(companyId, Id)
                .Select(u => new
                {
                    Id = u.Id,
                    ItemCode = u.ItemCode,
                    ItemName = u.ItemName,
                    Description = u.PurchaseDescription,
                    Quantity = u.Quantity,
                    UnitPrice = u.PurchaseUnitPrice,
                    AccountId = u.PurchasesAccount.Id,
                    AccountCode = u.PurchasesAccount.ACode,
                    AccountName = u.PurchasesAccount.AName,
                    PtxtValue = u.PTaxRate.Key,
                    PtxtQuantity = u.PTaxRate.Quantity,
                    PtxtId = u.PTaxRate.Id,
                });
            if (itemList != null)
            {
                return Json(new { itemList });
            }
            else
            {
                return Json(new { success = "False" });
            }

        }
        [HttpPost]
        public ActionResult PostPurchase(InventoriesEditBill purchase)
        {
            int postcount = Request.Form.GetValues("ItemId").Count();
            if (postcount > 0)
            {
                var user = _uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
                var companyId = 0;
                if (logObj.CompanyId != null) companyId = (int)logObj.CompanyId;
                var empObj = empSer.GetEmployeeByUserId(user.Id);
                purchase.PurchaseTransactions.EmployeeId = empObj.Id;
                purchase.PurchaseTransactions.CompanyId = companyId;


                if (ptSer.AddPurchaseTransaction(purchase.PurchaseTransactions))
                {

                    int counter = 0;
                    var items = Request.Form.GetValues("ItemId");
                    var descriptions = Request.Form.GetValues("description");
                    var quantities = Request.Form.GetValues("quantity");
                    var unitPrices = Request.Form.GetValues("unitPrice");
                    var CoaIds = Request.Form.GetValues("CoaId");
                    var TaxIds = Request.Form.GetValues("TaxId");

                    for (var i = 0; i < postcount; i++)
                    {
                        PurchaseTransactionDetail ptd = new PurchaseTransactionDetail();

                        ptd.ItemId = int.Parse(items[i]);
                        ptd.Quantity = double.Parse(quantities[i]);
                        ptd.TaxId = int.Parse(TaxIds[i]);
                        ptd.UnitPrice = double.Parse(unitPrices[i]);
                        ptd.Description = descriptions[i];
                        ptd.CoaId = int.Parse(CoaIds[i]);
                        ptd.PurchaseTransactionId = purchase.PurchaseTransactions.Id;
                        ptdSer.AddPurchaseTransDetailService(ptd);
                        counter++;
                    }
                    if (postcount == counter)
                    {
                        string data = Request.Form["note_data"];

                        if (Request.Form["note_data"] != "")
                        {
                            JArray noteData = JArray.Parse(data);

                            
                            for (int i = 0; i < noteData.Count(); i++)
                            {
                                var PtDoc = new PurchaseTransactionDocument();

                                PtDoc.CreatedDate = Convert.ToDateTime(noteData[i]["date"].ToString());
                                PtDoc.Description = noteData[i]["des"].ToString();
                                PtDoc.DocumentType = noteData[i]["type"].ToString();
                                PtDoc.EmployeeId = empObj.Id;
                                PtDoc.PurchaseTransactionId = purchase.PurchaseTransactions.Id;
                                ptDocSer.AddPurchaseTransDocument(PtDoc);
                            }
                        }
                        string documentName;
                        string documentLocation;
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            if ("documentLocation[]" == Request.Files.GetKey(i))
                            {
                                documentName = "Document_" + purchase.PurchaseTransactions.Id + "_" + i + Path.GetRandomFileName() + Path.GetExtension(Request.Files[i].FileName);
                                documentLocation = Server.MapPath("~/Uploads/" + empObj.Companies.TradingName.Replace(" ", "_") + "/PurcahsesTransactionDocuments/");
                                if (fileUpload(Request.Files[i], documentName, documentLocation))
                                {
                                    var PtDoc = new PurchaseTransactionDocument();
                                    PtDoc.CreatedDate = DateTime.Now;
                                    PtDoc.DocumentType = "File";
                                    PtDoc.EmployeeId = empObj.Id;
                                    PtDoc.PurchaseTransactionId = purchase.PurchaseTransactions.Id;
                                    PtDoc.FileLocation = "Uploads/" + empObj.Companies.TradingName.Replace(" ", "_") + "/PurcahsesTransactionDocuments/" + documentName;
                                    ptDocSer.AddPurchaseTransDocument(PtDoc);
                                }
                            }

                        }
                        TempData["success"] = "Bill Created Successfully";

                    }
                    else
                    {
                        TempData["success"] = "One or more items could not be added";
                    }

                }
                else
                {
                    TempData["success"] = "Error in bill genaration";
                }

            }
            else 
            {
                TempData["success"] = "No Item was selected";
            }
         

            return RedirectToAction("EditBill", "Purchases");
        }
        public bool fileUpload(HttpPostedFileBase file, string fileName, string filePath)
        {
            try
            {

                string uploadPath = filePath;
                bool isValid = false;
                string[] fileExtensions = { ".bmp", ".jpg", ".png", ".jpeg", ".pdf", ".doc", ".txt", ".docx", ".xlsx" };
                for (int i = 0; i < fileExtensions.Length; i++)
                {

                    if (file.FileName.Contains(fileExtensions[i]))
                    {

                        isValid = true;
                        break;
                    }
                }

                if (isValid)
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    file.SaveAs(uploadPath + fileName);


                    return true;
                    //System.IO.File.Move(uploadPath + file.FileName, uploadPath + fileName + ".png");
                    //return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}