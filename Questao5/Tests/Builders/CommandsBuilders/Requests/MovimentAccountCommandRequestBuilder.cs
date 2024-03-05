using Bogus;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore.Requests;

namespace Questao5.Tests.Builders.CommandsBuilders.Requests
{
    public class MovimentAccountCommandRequestBuilder
    {
        private readonly MovimentAccountCommandRequest instance;
        private readonly Faker _faker = new("pt_BR");

        public MovimentAccountCommandRequestBuilder()
        {
            instance = new MovimentAccountCommandRequest()
            {
                IdAccount = Guid.NewGuid(),
                MovimetType = MovimentType.Credito,
                Value = _faker.Random.Decimal(1, (decimal)9999.99)
            };
        }

        public MovimentAccountCommandRequestBuilder(Guid idContaCorrente, MovimentType tipoMovimento)
        {
            instance = new MovimentAccountCommandRequest()
            {
                IdAccount = idContaCorrente,
                MovimetType = tipoMovimento,
                Value = _faker.Random.Decimal(1, (decimal)9999.99)
            };
        }

        public MovimentAccountCommandRequest Buid() => instance;
    }
}