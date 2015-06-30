using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
   public interface INotesService 
    {
       bool CreateNote(Notes note);
       bool UpdateNote(Notes note);
       bool DeleteNOtes(long Id);
       List<Notes> getAllNotes();
       Notes GetNotesById(long Id);
    }
}
