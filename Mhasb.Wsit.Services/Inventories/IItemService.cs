using Mhasb.Domain.Inventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Inventories
{
   public interface IItemService
    {
        bool AddItem(Item item);
        bool UpdateItem(Item item);
        bool DeleteItem(long Id);
        List<Item> GetAllItems();
        List<Item> GetAllItemsByConmanyId(int CompanyId, int Id);
        List<Item> GetItemsByCompanyId(int CompanyId);
    }
}
