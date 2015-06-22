using Mhasb.Domain.Subscriptions;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Subscriptions
{
    public class PackageServices :IPackagesServices
    {
        private readonly CrudOperation<Package> _finalCrud = new CrudOperation<Package>();

        public bool CreatePackages(Package package) 
        {
            try
            {
                package.State = ObjectState.Added;
                _finalCrud.AddOperation(package);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public bool UpdatePackage(Package package)
        {
            try
            {
                package.State = ObjectState.Modified;
                _finalCrud.UpdateOperation(package);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public Package GetSinglePackage(int Id) 
        {
            try
            {
                var packageObj = _finalCrud.GetOperation()
                    .Filter(c => c.Id == Id)
                    .Get().SingleOrDefault();
                return packageObj;
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
                return null;

            }
        }
        public List<Package> GetAllPackages()
        {
            try
            {
                var packageObj = _finalCrud.GetOperation()
                    .Get().ToList();
                return packageObj;
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
                return null;

            }
        }
    }
}
