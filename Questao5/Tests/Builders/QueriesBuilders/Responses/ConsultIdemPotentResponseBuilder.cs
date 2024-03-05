using Questao5.Application.Queries.Responses;

namespace Questao5.Tests.Builders.QueriesBuilders.Responses
{
    public class ConsultIdemPotentResponseBuilder
    {
        private readonly ConsultIdemPotentResponse instance;

        public ConsultIdemPotentResponseBuilder()
        {
            instance = new ConsultIdemPotentResponse()
            {
                IdMovimentProcessed = Guid.NewGuid()
            };
        }

        public ConsultIdemPotentResponse Build() => instance;
    }
}
