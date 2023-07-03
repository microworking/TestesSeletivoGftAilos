
namespace Questao5.Application.Queries.Responses
{
    public interface IGetBalance
    {
        int AccountNumber { get; set; }

        string AccountOwnerName { get; set; }

        DateTime MovimentDateTime { get; set; }

        double CurrentValueBalance { get; set; }

        string Message { get; set; }

        string ResultType { get; set; }
    }
}