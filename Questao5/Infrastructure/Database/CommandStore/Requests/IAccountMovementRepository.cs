
namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public interface IAccountMovementRepository
    {
        string DoAccountDebit(string AccountId, double Value);

        string DoAccountCredit(string AccountId, double Value);
    }
}