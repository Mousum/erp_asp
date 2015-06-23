using Mhasb.Domain.Contacts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
    public class NotesService : INotesService
    {
        private readonly CrudOperation<Notes> _rep = new CrudOperation<Notes>();
        public bool CreateNote(Notes note)
        {
            try { 
             note.State=ObjectState.Added;
                  _rep.AddOperation(note);
             return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateNote(Notes note)
        {
            try
            {
                note.State = ObjectState.Modified;
                _rep.UpdateOperation(note);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteNOtes(long Id)
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

        public List<Notes> getAllNotes()
        {
            try
            {

                var _obj = _rep.GetOperation()
                    .Get().ToList();
                return _obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public Notes GetNotesById(long Id)
        {
            try
            {

                var _obj = _rep.GetOperation()
                    .Filter(i=>i.Id==Id)
                    .Get().SingleOrDefault();
                return _obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}
