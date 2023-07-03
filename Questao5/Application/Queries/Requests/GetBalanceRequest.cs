using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class GetBalanceRequest : IRequest<IGetBalance>
    {
        public int AccountNumber { get; set; }
    }
}