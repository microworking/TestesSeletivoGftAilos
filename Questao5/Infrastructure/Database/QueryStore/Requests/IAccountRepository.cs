using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public interface IAccountRepository
    {
        Account GetAccount(int AccountId);

        List<AccountMovement> GetBalance(int AccountId);
    }
}