using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Repositories.Queries
{
    public interface IAccountQueriesRepository
    {
        Task<ConsultAccountResponse> ConsultAccountAsync(int numero);
        Task<ConsultAccountResponse> ConsultAccountAsync(Guid idContaCorrente);
        Task<ConsultBalanceQueryResponse> ConsultBalanceAsync(ConsultBalanceQueryRequest request);
        Task<ConsultIdemPotentResponse> ConsultIdemPotentMovimentAsync(ConsultIdemPotentRequest request);

    }
}
