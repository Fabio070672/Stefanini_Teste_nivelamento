using Questao5.Application.Queries.Requests;

namespace Questao5.Tests.Builders.QueriesBuilders.Requests
{
    public class ConsultIdemPotentRequestBuilder
    {
        private readonly ConsultIdemPotentRequest instance;

        public ConsultIdemPotentRequestBuilder()
        {
            instance = new ConsultIdemPotentRequest()
            {
                IdIdemPotent = Guid.NewGuid()
            };
        }

        public ConsultIdemPotentRequestBuilder(Guid id)
        {
            instance = new ConsultIdemPotentRequest()
            {
                IdIdemPotent = id
            };
        }

        public ConsultIdemPotentRequest Build() => instance;
    }
}
