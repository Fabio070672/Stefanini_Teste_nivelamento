namespace Questao5.Domain.Constants
{
    public static class ErrorMessages
    {
        public const string ERROR_CONSULT = "Saldo inválido.";
        public const string VALUE_REQUIDED = "Campo obrigatório.";
        public const string INVALID_VALUE = "Apenas valores positivos podem ser recebidos";
        public const string INVALID_TYPE = "Apenas os tipos “débito” ou “crédito” podem ser aceitos.";
        public const string INVALID_ACCOUNT_MOVEMENT = "Apenas contas correntes cadastradas podem receber movimentação.";
        public const string INACTIVE_ACCOUNT_MOVIMENT = "Apenas contas correntes ativas podem receber movimentação.";
        public const string INVALID_ACCOUNT_CONSULT = "Apenas contas correntes cadastradas podem consultar o saldo.";
        public const string INACTIVE_ACCOUNT_CONSULT = "Apenas contas correntes ativas podem consultar o saldo.";
    }
}
