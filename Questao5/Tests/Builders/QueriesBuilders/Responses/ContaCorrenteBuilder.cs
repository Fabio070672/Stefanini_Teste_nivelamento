using Bogus;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Tests.Builders.QueriesBuilders.Responses
{
    public class ContaCorrenteBuilder
    {
        private readonly ContaCorrente instance;
        private readonly Faker _faker = new("pt_BR");

        public ContaCorrenteBuilder()
        {
            instance = new ContaCorrente()
            {
                Numero = _faker.Random.Number(1, 999).ToString(),
                Nome = _faker.Person.FullName,
                Ativo = (int)AccountSituation.Ativa,
                IdContaCorrente = Guid.NewGuid().ToString()
            };
        }

        public ContaCorrenteBuilder(AccountSituation situacao)
        {
            instance = new ContaCorrente()
            {
                Numero = _faker.Random.Number(1, 999).ToString(),
                Nome = _faker.Person.FullName,
                Ativo = (int)situacao,
                IdContaCorrente = Guid.NewGuid().ToString()
            };
        }

        public ContaCorrente Buid() => instance;
    }
}
