using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Handlers
{
    public interface IConsultHandler
    {
        Task<ConsultBalanceResponse> Handle(ConsultRequest request);
    }
}
