
namespace Questao5.Domain.Entities
{
    public class AccountMovement
    {
        public AccountMovement()
        {
            this.MovementId = string.Empty;
            this.AccountId = string.Empty;
            this.MovementDate = string.Empty;
        }

        public string MovementId { get; set; }
        
        public string AccountId { get; set; }

        public string MovementDate { get; set; }
 
        public char MovementType { get; set; }

        public Double MovementValue { get; set; }
    }
}