using Bogus;
using Questao5.Application.Queries.Requests;

namespace Questao5.Tests.Builders.QueriesBuilders.Requests
{
    public class ConsultRequestBuilder
    {
        private readonly ConsultRequest instance;
        private readonly Faker _faker = new("pt_BR");

        public ConsultRequestBuilder()
        {
            instance = new ConsultRequest()
            {
                Number = _faker.Random.Number(0, 999)
            };
        }

        public ConsultRequest Build() => instance;
    }
}
