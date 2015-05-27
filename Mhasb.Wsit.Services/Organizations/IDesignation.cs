using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Organizations;

namespace Mhasb.Services.Organizations
{
   public interface IDesignation
   {
       bool AddDesignation(Designation designation);
       bool UpdateDesignation(Designation designation);
       List<Designation> GetDesignations();
       Designation GetSingleDesignationById(int id);

       Designation GetSingleDesignationByDesignationName(string designation);

   }
}
