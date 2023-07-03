
namespace Questao5.Application.Commands.Responses
{
    public class AccountMovementResponse : IAccountMovement
    {
        public AccountMovementResponse()
        {
            this.MovementId = string.Empty;
            this.Message = string.Empty;
        }
        
        public string MovementId { get; set; }

        public string Message { get; set; }

        public string ResultType { get; set; }
    }
}