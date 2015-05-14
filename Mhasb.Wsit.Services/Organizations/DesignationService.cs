﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.Organizations
{
    public class DesignationService : IDesignation
    {
        private readonly CrudOperation<Designation> _crudOperation = new CrudOperation<Designation>();
        public bool AddDesignation(Domain.Organizations.Designation designation)
        {
            try
            {
                designation.State = ObjectState.Added;
                _crudOperation.AddOperation(designation);
                return true;
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                return false;
            }
           
        }

        public bool UpdateDesignation(Domain.Organizations.Designation designation)
        {
            try
            {
                var dbObj = _crudOperation.GetSingleObject(designation.Id);
                dbObj.DesignationName = designation.DesignationName;
                dbObj.State = ObjectState.Modified;
                _crudOperation.UpdateOperation(dbObj);
                return true;

            }catch (Exception ex)
            {
                var errorMsg = ex.Message;
                return false;
            }
        }

        public List<Domain.Organizations.Designation> GetDesignations()
        {
            throw new NotImplementedException();
        }
    }
}