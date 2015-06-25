using Mhasb.Domain.Contacts;
using Mhasb.Services.Accounts;
using Mhasb.Services.Commons;
using Mhasb.Services.Contact;
using Mhasb.Services.Loggers;
using Mhasb.Services.Organizations;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.Contacts.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactInformationService contactInfoService = new ContactInformationService();
        private readonly IContactDetailsService contactDetailsService = new ContactDetailsService();
        private readonly IPersonService personService = new PersonService();
        private readonly IFinancialDetailsService financialDetailsService = new FinancialDetailsService();
        private readonly INotesService noteService = new NotesService();
        private readonly ITelePhoneService telephoneService = new TelePhoneService();
        private readonly IUserService uService = new UserService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ICompanyService cService = new CompanyService();
        private readonly IContactInformationService ConInSer = new ContactInformationService();
        private readonly IPersonService pSer = new PersonService();
        private ILookupService luSer = new LookupService();
        private IChartOfAccountService cSer = new ChartOfAccountService();
        private readonly ICurrency currencyService = new CurrencyService();
        //
        // GET: /Contacts/Contact/
        public ActionResult Index()
        {
            var model = ConInSer.GetAllContactInformation();
            return View(model);
        }
        [HttpGet]
        public ActionResult FilterContact(string Filter, string SearchString, string Type)
        {
            
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            EnumContactType ContactType = EnumContactType.Archive;
            var IsNum =false;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            Regex regex = new Regex(@"^\d");
            if (Filter != null) 
            {
                IsNum = regex.IsMatch(Filter);
            }
           
           
            if (Type != null)
            {
                ContactType = (EnumContactType)Enum.Parse(typeof(EnumContactType), Type);
            }
            var contacts = pSer.GetAllContactsByCompany(companyId);
            ViewBag.AllCount = contacts.Count();
            ViewBag.CustomerCount = contacts.Where(r => r.ContactInformations.ContactType == EnumContactType.Customer).Count();
            ViewBag.SupllierCount = contacts.Where(r => r.ContactInformations.ContactType == EnumContactType.Supplier).Count();
            ViewBag.EmployeeCount = contacts.Where(r => r.ContactInformations.ContactType == EnumContactType.Employee).Count();
            ViewBag.ArchiveCount= contacts.Where(r => r.ContactInformations.ContactType == EnumContactType.Archive).Count();
 
            //Take As binary ,we have 3 oparents
            //1 1 0
            if (Filter != null && SearchString != null && Type == null)
            {
                if (!IsNum)
                {
                    contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(SearchString) && r.ContactInformations.ContactName.StartsWith(Filter)).ToList();
                }
                else
                {
                    contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(SearchString) && r.ContactInformations.ContactName.Contains(Filter)).ToList();
                }
             

            }
            //0 1 0
            else if (Filter == null && SearchString != null && Type == null)
            {
                contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(SearchString)).ToList();
            }
            //1 0 0
            else if (Filter != null && SearchString == null && Type == null)
            {

                if (!IsNum)
            {
                contacts = contacts.Where(r => r.ContactInformations.ContactName.StartsWith(Filter)).ToList();
            }
                else
                {
                    contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(Filter)).ToList();
                }
            }
            //0 0 1
            else if (Filter == null && SearchString == null && Type != null)
            {
                contacts = contacts.Where(r => r.ContactInformations.ContactType == ContactType).ToList();
            }
            //0 1 0 
            else if (Filter == null && SearchString != null && Type != null)
            {
                contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(SearchString) && r.ContactInformations.ContactType == ContactType).ToList();
            }
            //1 0 1
            else if (Filter != null && SearchString == null && Type != null)
            {
                if (!IsNum)
                {
                    contacts = contacts.Where(r => r.ContactInformations.ContactName.StartsWith(Filter) && r.ContactInformations.ContactType == ContactType).ToList();
                }
                else
                {
                    contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(Filter) && r.ContactInformations.ContactType == ContactType).ToList();
                }


            }
            //1 1 1
            else if (Filter != null && SearchString != null && Type != null)
            {
                if (!IsNum)
                {
                    contacts = contacts.Where(r => r.ContactInformations.ContactName.StartsWith(Filter) && r.ContactInformations.ContactName.Contains(SearchString) && r.ContactInformations.ContactType == ContactType).ToList();
                }
                else 
                {
                    contacts = contacts.Where(r => r.ContactInformations.ContactName.Contains(Filter) && r.ContactInformations.ContactName.Contains(SearchString) && r.ContactInformations.ContactType == ContactType).ToList();
                }

            }



            return View(contacts);
            

           
        }
        //
        // GET: /Contacts/Contact/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Contacts/Contact/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);

            ViewBag.CompanyCompleteFlag = logObj.Companies.CompleteFlag;
            /// var AccSet = setService.GetAllByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            var Atypes = cSer.GetAllChartOfAccountByCompanyIdForSecondLevel(companyId);

            if (Atypes.Count == 0)
            {
                cSer.AddBaseAccountTypes();
                Atypes = cSer.GetAllChartOfAccountByCompanyId(companyId);
            }


            ViewBag.CurrencyList = new SelectList(currencyService.GetAllCurrency(), "Id", "Name");
            var lookups = luSer.GetLookupByType("Tax").Select(u => new { Id = u.Id, Value = u.Value + "(" + u.Quantity + "%)" });
            ViewBag.Lookups = new SelectList(lookups, "Id", "Value");

            return View();
        }


        [HttpPost]
        public ActionResult Create(CustomModel.Contact.ContactCustome cc)
        {
            ContactInformation ContactInfo = cc.ContactInformation;
            ContactDetails PostalAddress = cc.PostalAddress;
            ContactDetails PhysicalAddress = cc.PhysicalAddress;
            Person PrimaryPerson = cc.PrimaryPerson;
            List<Person> Peoples = cc.Person;
            FinancialDetails FinancialDetail = cc.FinancialDetails;
            Notes Note = cc.Notes;
            TelePhone Telephone = cc.TelePhone;

            try
            {
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            var activatedCompany = cService.GetSingleCompany(companyId);

                ContactInfo.CompanyId = companyId;
                ContactInfo.CreateBy = user.Id;
                ContactInfo.UpdateBy = user.Id;
                ContactInfo.CreateDate = DateTime.Now;
                ContactInfo.UpdateDate = DateTime.Now;
                if (contactInfoService.CreateContInfo(ContactInfo))
                {

                    PostalAddress.ContactInfoId = ContactInfo.Id;
                    PhysicalAddress.ContactInfoId = ContactInfo.Id;
                    if (!(contactDetailsService.CreateContactDetails(PostalAddress) && contactDetailsService.CreateContactDetails(PhysicalAddress)))
                        TempData.Add("errMsg","Postal Address and Physical Address not set properly.");
                    PrimaryPerson.ContactInfoId = ContactInfo.Id;
                    personService.CreatePersons(PrimaryPerson);

                    foreach (var people in Peoples)
        {
                        people.ContactInfoId = ContactInfo.Id;
                        personService.CreatePersons(people);
        }

                    FinancialDetail.ContactInfoId = ContactInfo.Id;
                    //FinancialDetail.

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData.Add("errMsg", "Please FillUp Every Field");
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                return Content("Somthing Wrong");
            }
        }


        //
        // GET: /Contacts/Contact/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Contacts/Contact/Edit/5
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
        // GET: /Contacts/Contact/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contacts/Contact/Delete/5
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
