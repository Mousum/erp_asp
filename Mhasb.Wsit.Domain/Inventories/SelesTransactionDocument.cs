﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Inventories
{
    public class SelesTransactionDocument : IObjectStateLong
    {
        public long PurchaseTransactionId { get; set; }
        public long EmployeeId { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public string FileLocation { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Employee Employees { get; set; }
        public virtual SelesTransaction SelesTransactions { get; set; }
        public ObjectState State { get; set; }
        public long Id { get; set; }
    }
}
