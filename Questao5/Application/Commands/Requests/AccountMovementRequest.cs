using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class AccountMovementRequest : IRequest<IAccountMovement>
    {
        public AccountMovementRequest()
        {
            this.MovementType = string.Empty;
        }

        public int AccountNumber { get; set; }

        public double MovementValue { get; set; }

        public string MovementType { get; set; }
    }
}