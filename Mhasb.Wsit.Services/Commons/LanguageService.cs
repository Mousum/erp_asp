using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public class LanguageService : ILanguageService
    {
        private readonly CrudOperation<Language> languageRep = new CrudOperation<Language>();

        public List<Language> GetAllLanguages()
        {
            try
            {
                var languageObj = languageRep.GetOperation()
                                        .Get().ToList();

                return languageObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }
    }
}
