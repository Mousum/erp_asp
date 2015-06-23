using Mhasb.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Subscriptions
{
    public interface IPackagesServices
    {
        bool CreatePackages(Package package);
        bool UpdatePackage(Package package);
        Package GetSinglePackage(int Id);
        List<Package> GetAllPackages();
    }
}
