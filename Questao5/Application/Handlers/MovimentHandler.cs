using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Constants;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Repositories.Commands;
using Questao5.Infrastructure.Repositories.Queries;
using System.Text;

namespace Questao5.Application.Handlers
{
    public class MovimentHandler : IMovimentHandler
    {
        private readonly IAccountCommandsRepository _repositoryCommands;
        private readonly IAccountQueriesRepository _repositoryQueries;

        public MovimentHandler(IAccountCommandsRepository repository, IAccountQueriesRepository repositoryQueries)
        {
            _repositoryCommands = repository;
            _repositoryQueries = repositoryQueries;
        }

        public async Task<MovimentResponse> Handle(MovimentRequest command)
        {
            ConsultIdemPotentResponse? idemPotent = await CheckIdemPotent(command);
            if (idemPotent is not null)
                return new MovimentResponse() { IdMoviment = idemPotent.IdMovimentProcessed };

            var conta = await _repositoryQueries.ConsultAccountAsync(command.Number);
            if (conta is null)
                return new MovimentResponse() { ErrorMessage = ErrorMessages.INVALID_ACCOUNT_MOVEMENT };

            if (conta.Active == AccountSituation.Inativa)
                return new MovimentResponse() { ErrorMessage = ErrorMessages.INACTIVE_ACCOUNT_MOVIMENT };

            var idMovimento = await _repositoryCommands.MovimentAccountAsync(
                new MovimentAccountCommandRequest()
                {
                    IdAccount = conta.IdAccount,
                    MovimetType = command.MovimentType,
                    Value = command.Value,
                });

            await AddIdemPotencia(command, conta, idMovimento);

            return new MovimentResponse() { IdMoviment = idMovimento };
        }

        private async Task AddIdemPotencia(MovimentRequest command, ConsultAccountResponse? conta, Guid idMovimento)
        {
            StringBuilder requestMovimento = new StringBuilder();
            requestMovimento.Append(conta!.IdAccount);
            requestMovimento.Append(" | ");
            requestMovimento.Append(command.MovimentType.ToString());
            requestMovimento.Append(" | ");
            requestMovimento.Append(command.Value.ToString());
            requestMovimento.Append(" | ");
            requestMovimento.Append(DateTime.Now.ToString());
            await _repositoryCommands.AddIdemPotentMovimentAsync(
                new IdemPotentMovimentRequest
                {
                    Chave_IdemPotencia = command.IdIdemPotent,
                    Request = requestMovimento.ToString(),
                    Result = idMovimento.ToString()
                });
        }

        private async Task<ConsultIdemPotentResponse?> CheckIdemPotent(MovimentRequest command)
        {
            return await _repositoryQueries.ConsultIdemPotentMovimentAsync(new ConsultIdemPotentRequest { IdIdemPotent = command.IdIdemPotent });
        }
    }
}
