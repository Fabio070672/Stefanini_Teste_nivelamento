using Bogus;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Enumerators;

namespace Questao5.Tests.Builders.CommandsBuilders.Requests
{
    public class MovimentRequestBuilder
    {
        private readonly MovimentRequest instance;
        private readonly Faker _faker = new("pt_BR");
        public MovimentRequestBuilder()
        {
            instance = new MovimentRequest()
            {
                Number = _faker.Random.Number(1, 999),
                MovimentType = MovimentType.Credito,
                Value = _faker.Random.Decimal(1, 9999),
                IdIdemPotent = Guid.NewGuid()
            };
        }

        public MovimentRequest Setup_Debito()
        {
            return new MovimentRequest()
            {
                Number = _faker.Random.Number(1, 999),
                MovimentType = MovimentType.Debito,
                Value = _faker.Random.Decimal(1, 9999),
                IdIdemPotent = Guid.NewGuid()
            };
        }

        public MovimentRequest Setup_IdemPotencia(Guid idIdemPotencia)
        {
            return new MovimentRequest()
            {
                Number = _faker.Random.Number(1, 999),
                MovimentType = MovimentType.Debito,
                Value = _faker.Random.Decimal(1, 9999),
                IdIdemPotent = idIdemPotencia
            };
        }

        public MovimentRequest Build() => instance;
    }
}
