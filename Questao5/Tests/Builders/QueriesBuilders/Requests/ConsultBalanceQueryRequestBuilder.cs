using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Tests.Builders.QueriesBuilders.Requests
{
    public class ConsultBalanceQueryRequestBuilder
    {
        private readonly ConsultBalanceQueryRequest instance;

        public ConsultBalanceQueryRequestBuilder()
        {
            instance = new ConsultBalanceQueryRequest()
            {
                IdAccount = Guid.NewGuid()
            };
        }

        public ConsultBalanceQueryRequestBuilder(Guid id)
        {
            instance = new ConsultBalanceQueryRequest()
            {
                IdAccount = id
            };
        }

        public ConsultBalanceQueryRequest Build() => instance;
    }
}
