﻿using Mhasb.Domain.Inventories;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Inventories
{
   public class ItemService:IItemService
    {
       private readonly CrudOperation<Item> _rep = new CrudOperation<Item>();
       private readonly CrudOperation<PurchaseTransaction> _repPt = new CrudOperation<PurchaseTransaction>();
       private readonly CrudOperation<PurchaseTransactionDetail> _repPtdl = new CrudOperation<PurchaseTransactionDetail>();
       private readonly CrudOperation<PurchaseTransactionDocument> _repPtdc = new CrudOperation<PurchaseTransactionDocument>();
        public bool AddItem(Item item)
        {
            try { 
            item.State=ObjectState.Added;
            _rep.AddOperation(item);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateItem(Item item)
        {
            try {
                item.State = ObjectState.Modified;
                _rep.UpdateOperation(item);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteItem(long Id)
        {
            try { 
            _rep.DeleteOperation(Id);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            
            }
        }
        public List<Item> GetAllItems()
        {
            try { 
                var _obj=_rep.GetOperation()
                    .Get()
                    .ToList();
                return _obj;
            }catch(Exception ex){
                var msg = ex.Message;
                return null;
            }
        }




       
    }
}