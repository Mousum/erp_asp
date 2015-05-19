using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.CustomModel.Accounts
{
   public class TreeViewNode
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }
        public bool hasChildren { get; set; }
        public string classes { get; set; }

        public TreeViewNode[] children { get; set; }
    }
}
