using Mhasb.Domain.Accounts;

namespace Mhasb.Services.Accounts
{
   public interface ITransferMoneyService
   {
       bool AddTransferMoney(TransferMoney transferMoney);
       bool UpdateTransferMoney(TransferMoney transferMoney);
       bool DeleteTransferMoney(long id);
   }
}
