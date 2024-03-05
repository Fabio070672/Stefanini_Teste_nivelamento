using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class ConsultAccountResponse
    {
        public Guid IdAccount { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public AccountSituation Active { get; set; }

        public ConsultAccountResponse() { }

        public static ConsultAccountResponse ConvertTo(ContaCorrente contaCorrente)
        {
            return new ConsultAccountResponse()
            {
                IdAccount = new Guid(contaCorrente.IdContaCorrente),
                Number = Convert.ToInt32(contaCorrente.Numero),
                Name = contaCorrente.Nome,
                Active = contaCorrente.Ativo == 1 ? AccountSituation.Ativa : AccountSituation.Inativa
            };
        }
    }
}
