using Bogus;
using Questao5.Application.Queries.Responses;

namespace Questao5.Tests.Builders.QueriesBuilders.Responses
{
    public class ConsultBalanceResponseBuilder
    {
        private readonly ConsultBalanceResponse instance;
        private readonly Faker _faker = new("pt_BR");

        public ConsultBalanceResponseBuilder()
        {
            instance = new ConsultBalanceResponse()
            {
                Number = _faker.Random.Number(1, 999).ToString(),
                Name = _faker.Person.FullName,
                DateTimeConsult = DateTime.Now,
                ValueBalance = _faker.Random.Decimal(0, 99999)
            };
        }

        public ConsultBalanceResponseBuilder(int number, string nome, decimal valorSaldo)
        {
            instance = new ConsultBalanceResponse()
            {
                Number = number.ToString(),
                Name = nome,
                DateTimeConsult = DateTime.Now,
                ValueBalance = valorSaldo
            };
        }

        public ConsultBalanceResponseBuilder(string errorMessage)
        {
            instance = new ConsultBalanceResponse()
            {
                ErrorMessage = errorMessage
            };
        }

        public ConsultBalanceResponse Build() => instance;
    }
}
