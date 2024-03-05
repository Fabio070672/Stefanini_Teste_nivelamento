using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Constants;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Repositories.Queries;

namespace Questao5.Application.Handlers
{
    public class ConsultHandler : IConsultHandler
    {
        private readonly IAccountQueriesRepository _repository;

        public ConsultHandler(IAccountQueriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConsultBalanceResponse> Handle(ConsultRequest request)
        {
            ConsultAccountResponse? account = await _repository.ConsultAccountAsync(request.Number);
            if (account is null)
                return new ConsultBalanceResponse() { ErrorMessage = ErrorMessages.INVALID_ACCOUNT_CONSULT };

            if (account.Active == AccountSituation.Inativa)
                return new ConsultBalanceResponse() { ErrorMessage = ErrorMessages.INACTIVE_ACCOUNT_CONSULT };

            ConsultBalanceQueryResponse result = await _repository.ConsultBalanceAsync(new ConsultBalanceQueryRequest { IdAccount = account.IdAccount });
            if (result is null)
                return new ConsultBalanceResponse() { ErrorMessage = ErrorMessages.ERROR_CONSULT };

            return new()
            {
                Number = account.Number.ToString(),
                Name = account.Name,
                DateTimeConsult = DateTime.Now,
                ValueBalance = result.ValueBalance
            };
        }
    }
}
