using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Enumerators;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/CheckingAccount")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]

    public class CheckingAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CheckingAccountController(IMediator Mediator) => _mediator = Mediator;

        /// <summary>Obter o saldo da conta corrente pelo número da conta</summary>
        /// <param name="AccountNumber"></param>
        /// <remarks>
        /// {
        ///     "AccountNumber": int - Número da conta
        /// }
        /// </remarks>
        /// <returns>Retorna o número da conta corrente, nome do titular da conta corrente, data e hora da resposta da consulta e o valor do saldo atual.</returns>
        /// <response code="200">Retorna o número da conta corrente, nome do titular da conta corrente, data e hora da resposta da consulta e o valor do saldo atual</response>
        /// <response code="400">Retorna um código e uma mensagem de erro</response>
        [HttpGet("AccountBalance/{AccountNumber}")]
        public async Task<IActionResult> AccountBalance(int AccountNumber)
        {
            GetBalanceRequest getBalanceRequest = new() { AccountNumber = AccountNumber };
            var retorno = await _mediator.Send(getBalanceRequest);

            if (retorno.ResultType == ResultTypeEnum.SUCCESS.ToString())
                return Ok(retorno);
            else
                return BadRequest(retorno);
        }

        /// <summary>Movimenta a conta corrente mediante saques e depósitos.</summary>
        /// <param name="request"></param>
        /// <remarks>
        /// {
        ///     "AccountNumber": int - Número da conta,\r\n
        ///     "MovementValue": double - Valor a ser movimentado,\r\n
        ///     "MovementType": string - Modalidade da operação. Aceita os valores C para crédito e D para débito
        /// }
        /// </remarks>
        /// <response code="200">Retorna o UID gerado pela movimentação da conta corrente.</response>
        /// <response code="400">Retorna um código e uma mensagem de erro.</response>
        [HttpPost("AccountMovement")]
        public async Task<IActionResult> AccountMovement([FromBody] AccountMovementRequest request)
        {
            var retorno = await _mediator.Send(request);

            if (retorno.ResultType == ResultTypeEnum.SUCCESS.ToString())
                return Ok(retorno);
            else
                return BadRequest(retorno);
        }
    }
}