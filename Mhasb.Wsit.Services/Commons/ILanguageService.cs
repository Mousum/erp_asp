using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public interface ILanguageService
    {
        List<Language> GetAllLanguages();
        bool CreateLanguage(Language language);
        bool UpdateLanguage(Language language);
        bool UpdateLanguage(int Id);

        Language GetSingleLanguage(int Id);

    }
}
