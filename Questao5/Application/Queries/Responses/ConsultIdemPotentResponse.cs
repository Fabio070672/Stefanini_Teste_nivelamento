using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Responses
{
    public class ConsultIdemPotentResponse
    {
        public Guid IdMovimentProcessed { get; set; }

        public ConsultIdemPotentResponse() { }

        public static ConsultIdemPotentResponse ConvertTo(IdemPotencia idemPotencia)
        {
            return new ConsultIdemPotentResponse()
            {
                IdMovimentProcessed = new Guid(idemPotencia.Resultado)
            };
        }
    }
}
