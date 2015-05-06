using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
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


        public bool CreateLanguage(Language language)
        {
            try
            {
                language.State = ObjectState.Added;
                languageRep.AddOperation(language);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateLanguage(Language language)
        {
            try
            {
                language.State = ObjectState.Modified;
                languageRep.AddOperation(language);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateLanguage(int Id)
        {
            try
            {
                languageRep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }
        public Language GetSingleLanguage(int Id)
        {
            try
            {
                var LangObj = languageRep.GetOperation()
                    .Filter(c => c.Id == Id)
                    .Get().SingleOrDefault();
                return LangObj;
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
                return null;

            }
        }
    }
}
