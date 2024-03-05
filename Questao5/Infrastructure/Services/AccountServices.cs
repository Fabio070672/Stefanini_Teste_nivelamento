using Questao5.Application.Utils;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services
{
    public static class AccountServices
    {
        public static decimal SumMoviments(List<Movimento> moviments, MovimentType movimentType)
        {
            char movimentTypeChar = MovimentTypeConvert.ConvertToChar(movimentType);

            return moviments.Where(x => x.TipoMovimento == movimentTypeChar.ToString()).Sum(x => x.Valor);
        }
    }
}
