using Bogus;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Tests.Builders.QueriesBuilders.Responses
{
    public class ConsultBalanceQueryResponseBuilder
    {
        private readonly ConsultBalanceQueryResponse instance;
        private readonly Faker _faker = new("pt_BR");

        public ConsultBalanceQueryResponseBuilder()
        {
            instance = new ConsultBalanceQueryResponse()
            {
                ValueBalance = _faker.Random.Decimal(1, (decimal)9999.99)
            };
        }

        public ConsultBalanceQueryResponse Build() => instance;
    }
}
