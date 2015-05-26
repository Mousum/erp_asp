using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Users
{
  public interface IActionListService
    {
      bool AddActionList(ActionList actionlist);
      bool AddActionListFromBaseController(ActionList actionlist);
      bool UpdateActionList(ActionList actionlist);
      bool DeleteActionList(int id);

      ActionList GetSingleActionList(int id);

      ActionList GetActionListByActionList(ActionList actionlist);

      List<ActionList> GetAllActionList();
    }
}
