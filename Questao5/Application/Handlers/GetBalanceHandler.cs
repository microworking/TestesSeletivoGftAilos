using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Application.Handlers
{
    public class GetBalanceHandler : IRequestHandler<GetBalanceRequest, IGetBalance>
    {
        private readonly IAccountRepository _accountRepository;

        public GetBalanceHandler(IAccountRepository AccountRepository) => _accountRepository = AccountRepository;

        public async Task<IGetBalance> Handle(GetBalanceRequest Request, CancellationToken CancellationToken)
        {
            GetBalanceResponse getBalanceResponse = new();

            Account account = _accountRepository.GetAccount(Request.AccountNumber);
            if (account != null)
            {
                if (account.IsActiveAccount == (int)AccountStatusEnum.ActiveAccount)
                {
                    getBalanceResponse.AccountNumber = account.AccountNumber;
                    getBalanceResponse.AccountOwnerName = account.AccountOwnerName;
                    getBalanceResponse.MovimentDateTime = DateTime.UtcNow;

                    List<AccountMovement> accountMovementByClient = _accountRepository.GetBalance(account.AccountNumber);

                    if (accountMovementByClient.Count > 0)
                    {
                        double TotalCreditValue = accountMovementByClient.Where(x => x.MovementType == 'C').Sum(x => x.MovementValue);
                        double TotalDebitValue = accountMovementByClient.Where(x => x.MovementType == 'D').Sum(x => x.MovementValue);
                        double balance = TotalCreditValue - TotalDebitValue;

                        getBalanceResponse.CurrentValueBalance = balance;
                    }
                    else
                        getBalanceResponse.CurrentValueBalance = 0;

                    getBalanceResponse.ResultType = ResultTypeEnum.SUCCESS.ToString();
                }
                else
                {
                    getBalanceResponse.ResultType = ResultTypeEnum.INACTIVE_ACCOUNT.ToString();
                    getBalanceResponse.Message = $"A conta {Request.AccountNumber} está inativa.";
                }
            }
            else
            {
                getBalanceResponse.ResultType = ResultTypeEnum.INVALID_ACCOUNT.ToString();
                getBalanceResponse.Message = $"A conta informada é inválida. A conta informada foi {Request.AccountNumber}.";
            }

            return getBalanceResponse;
        }
    }
}