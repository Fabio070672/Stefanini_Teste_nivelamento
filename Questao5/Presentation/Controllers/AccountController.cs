using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using System.ComponentModel;

namespace Questao5.Presentation.Controllers
{
    [ApiController]
    [Route("Conta-Corrente")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Obtém saldo da conta corrente
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="numero"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Saldo")]
        [Description("Obtém saldo da conta corrente")]
        [ProducesResponseType(typeof(ConsultBalanceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConsultBalanceResponse>> GetBalance(
                   [FromServices] IConsultHandler handler,
                   [FromQuery] AccountCommand command
               )
        {
            if (command.Conta <= 0)
                return BadRequest();

            var response = await handler.Handle(new ConsultRequest() { Number = command.Conta });
            if (response.ErrorMessage is not null)
                return BadRequest(response.ErrorMessage);

            return Ok(response);
        }

        /// <summary>
        /// Gera Movimentação da Conta Corrente
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Movimento")]
        [Description("Gera Movimentação da Conta Corrente")]
        [ProducesResponseType(typeof(MovimentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovimentResponse>> PostMoviment(
                   [FromServices] IMovimentHandler handler,
                   [FromBody] MovimentRequest request
               )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await handler.Handle(request);
            if (response.ErrorMessage is not null)
                return BadRequest(response.ErrorMessage);

            return Ok(response);
        }
    }
}
