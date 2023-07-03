
namespace Questao5.Domain.Entities
{
    public class Account
    {
        public Account()
        {
            this.AccountId = string.Empty; 
            this.AccountOwnerName = string.Empty;
        }

        public string AccountId { get; set; }

        public int AccountNumber { get; set; }

        public string AccountOwnerName { get; set; }

        public int IsActiveAccount { get; set; }
    }
}