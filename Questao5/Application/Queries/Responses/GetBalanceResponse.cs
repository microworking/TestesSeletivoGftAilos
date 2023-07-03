
namespace Questao5.Application.Queries.Responses
{
    public class GetBalanceResponse : IGetBalance
    {
        public GetBalanceResponse()
        {
            this.AccountOwnerName = string.Empty;
            this.Message = string.Empty;
            this.ResultType = string.Empty;
        }

        public int AccountNumber { get; set; }

        public string AccountOwnerName { get; set; }

        public DateTime MovimentDateTime { get; set; }

        public double CurrentValueBalance { get; set; }

        public string Message { get; set; }

        public string ResultType { get; set; }
    }
}