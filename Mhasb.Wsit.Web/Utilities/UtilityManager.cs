using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Utilities
{
    public static class UtilityManager
    {
        //public static List<SelectList> 

        public static int GetLedgerLevel(string code)
        {
            int level = 0;
            if (code.Length == 1)
            {
                level = 1;
            }
            else if (code.Length == 3)
            {
                level = 2;
            }

            else if (code.Length == 5)
            {
                level = 3;
            }
            else if (code.Length == 10)
            {
                level = 4;
            }
            return level;
        }

        public static bool CheckingNull(object obj, out string msg)
        {
            msg = "Success";
            if(obj==null){
                msg = "Object Not Found";
                return false;
            }
                
            return true;
        }
    }
}