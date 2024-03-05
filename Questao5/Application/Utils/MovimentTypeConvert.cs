using Questao5.Domain.Enumerators;

namespace Questao5.Application.Utils
{
    public static class MovimentTypeConvert
    {
        public static char ConvertToChar(MovimentType movimentType)
        {
            switch (movimentType)
            {
                case MovimentType.Credito:
                    return 'C';
                case MovimentType.Debito:
                    return 'D';
                default: //Error
                    return 'E';
            }
        }

        public static MovimentType ConvertToEnum(char movimentType)
        {
            switch (movimentType)
            {
                case 'C':
                    return MovimentType.Credito;
                case 'D':
                    return MovimentType.Debito;
                default://Error
                    throw new Exception();
            }
        }
    }
}
