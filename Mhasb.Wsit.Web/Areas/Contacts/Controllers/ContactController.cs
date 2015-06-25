﻿using Mhasb.Domain.Contacts;
using Mhasb.Services.Contact;
using Mhasb.Services.Loggers;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //
        // GET: /Contacts/Contact/
        public ActionResult Index()
        {
            return View();
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
            //var tt = HttpContext.User.Identity.Name;
            //var user = uService.GetSingleUserByEmail(tt);
            //var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            //int companyId = 0;
            //if (logObj != null)
            //{
            //    companyId = (int)logObj.CompanyId;
            //}
            //var activatedCompany = cService.GetSingleCompany(companyId);

            //ViewData["Company"] = activatedCompany;
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
