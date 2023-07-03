using Questao5.Application.Queries.Responses;

namespace Questao5.Domain.Entities
{
    public class Balance
    {
        public Balance() => this.AccountOwnerName = string.Empty;

        public int CheckingAccountId { get; set; }

        public string AccountOwnerName { get; set; }

        public DateTime MovimentDateTime { get; set; }

        public double CurrentValueBalance { get; set; }
    }
}