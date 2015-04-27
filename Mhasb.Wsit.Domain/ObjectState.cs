using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.Domain
{
    public enum ObjectState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
    public interface IObjectStateInt : IObjectState
    {
        int Id { get; set; }
        //Nullable<System.DateTime> EntryDate { get; set; }
        //Nullable<long> EntryBy { get; set; }        
    }
    public interface IObjectStateLong : IObjectState
    {
        long Id { get; set; }
        //Nullable<System.DateTime> EntryDate { get; set; }
        //Nullable<long> EntryBy { get; set; }       
    }

    public interface IObjectState
    {
        ObjectState State { get; set; }
    }
}

