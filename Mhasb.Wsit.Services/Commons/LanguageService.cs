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

        public bool DeleteLanguage(int Id)
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
                //role.State = ObjectState.Unchanged;
                var labObj = languageRep.GetOperation()
                                        .Filter(r => r.Id == Id)
                                        .Get().SingleOrDefault();

                //roleRep.GetSingleObject(companyId);
                return labObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
    }
}
