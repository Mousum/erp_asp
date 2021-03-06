﻿using Mhasb.Domain.Contacts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
    public class ContactInformationService : IContactInformationService
    {
        private readonly CrudOperation<ContactInformation> _rep = new CrudOperation<ContactInformation>();

        public bool CreateContInfo(ContactInformation ContactInformation)
        {
            try
            {
                ContactInformation.State = ObjectState.Added;
                _rep.AddOperation(ContactInformation);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateContInfo(ContactInformation ContactInformation)
        {
            try
            {
                ContactInformation.State = ObjectState.Modified;
                _rep.UpdateOperation(ContactInformation);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteContInfo(long Id)
        {
            try
            {

                _rep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }



        public List<ContactInformation> GetAllContactInformation()
        {
            try
            {
                var _Obj = _rep.GetOperation()
                    .Get().ToList();
                return _Obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public ContactInformation GetContactInformationById(long Id)
        {
            try
            {
                var _Obj = _rep.GetOperation()
                    .Include(r => r.ContactDtails)
                    .Include(r => r.TelePhones)
                    .Include(r => r.Persons)
                    .Include(r=>r.Notes)
                    .Filter(i => i.Id == Id)
                    .Get().FirstOrDefault();
                return _Obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
        public List<ContactInformation> GetAllContactInfoByCompanyId(int Id) 
        {
            try
            {
                var _Obj = _rep.GetOperation()
                   
                    .Include(r=>r.ContactDtails)
                    .Include(r=>r.TelePhones)
                    .Include(r=>r.Persons)
                    .Filter(r => r.CompanyId == Id)
                    .Get().ToList();
                return _Obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}
