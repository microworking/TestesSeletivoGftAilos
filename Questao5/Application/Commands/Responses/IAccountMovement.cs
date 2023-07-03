
namespace Questao5.Application.Commands.Responses
{
    public interface IAccountMovement
    {
        string MovementId { get; set; }

        string Message { get; set; }

        string ResultType { get; set; }
    }
}