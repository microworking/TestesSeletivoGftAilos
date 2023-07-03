using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Application.Handlers
{
    public class AccountMovementHandler : IRequestHandler<AccountMovementRequest, IAccountMovement>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountMovementRepository _accountMovementRepository;

        public AccountMovementHandler(IAccountRepository AccountRepository, 
                                      IAccountMovementRepository AccountMovementRepository)
        {
            _accountRepository = AccountRepository;
            _accountMovementRepository = AccountMovementRepository;
        }

        public async Task<IAccountMovement> Handle(AccountMovementRequest Request, CancellationToken CancellationToken)
        {
            AccountMovementResponse accountMovementResponse = new();
            
            if (Request.MovementValue > 0)
            {
                if (Request.MovementType.ToUpper() == MovementTypeEnum.Debit.GetDescription() || Request.MovementType.ToUpper() == MovementTypeEnum.Credit.GetDescription())
                {
                    Account account = _accountRepository.GetAccount(Request.AccountNumber);
                    if (account != null)
                    {
                        if (account.IsActiveAccount == (int)AccountStatusEnum.ActiveAccount)
                        {
                            if (Request.MovementType.ToUpper() == MovementTypeEnum.Debit.GetDescription())
                                accountMovementResponse.MovementId = _accountMovementRepository.DoAccountDebit(account.AccountId, Request.MovementValue);
                            
                            if (Request.MovementType.ToUpper() == MovementTypeEnum.Credit.GetDescription())
                                accountMovementResponse.MovementId = _accountMovementRepository.DoAccountCredit(account.AccountId, Request.MovementValue);

                            accountMovementResponse.ResultType = ResultTypeEnum.SUCCESS.ToString();
                        }
                        else
                        {
                            accountMovementResponse.ResultType = ResultTypeEnum.INACTIVE_ACCOUNT.ToString();
                            accountMovementResponse.Message = $"A conta {Request.AccountNumber} está inativa.";
                        }
                    }
                    else
                    {
                        accountMovementResponse.ResultType = ResultTypeEnum.INVALID_ACCOUNT.ToString();
                        accountMovementResponse.Message = $"A conta informada é inválida. A conta informada foi {Request.AccountNumber}.";
                    }
                }
                else
                {
                    accountMovementResponse.ResultType = ResultTypeEnum.INVALID_TYPE.ToString();
                    accountMovementResponse.Message = $"O tipo de operação informado para a movimentação da conta é inválido. O tipo informado foi {Request.MovementType}";
                }
            }
            else
            {
                accountMovementResponse.ResultType = ResultTypeEnum.INVALID_VALUE.ToString();
                accountMovementResponse.Message = $"O valor informado para a movimentação da conta deve ser um valor positivo. O valor informado foi {Request.MovementValue}.";
            }

            return accountMovementResponse;
        }
    }
}