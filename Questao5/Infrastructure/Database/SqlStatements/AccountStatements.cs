namespace Questao5.Infrastructure.Database.SqlStatements
{
    public static class AccountStatements
    {
        public const string SQL_INSERT_MOVIMENT = @"INSERT INTO movimento
                                (idmovimento, 
                                idcontacorrente, 
                                datamovimento, 
                                tipomovimento, 
                                valor) 
                                VALUES
                                (@IdMoviment, 
                                @IdAccount, 
                                DATE('now'), 
                                @MovimentType, 
                                @Value);"
        ;

        public const string SQL_INSERT_IDEMPOTENT_MOVIMENT = @"INSERT INTO idempotencia 
                                (chave_idempotencia, 
                                requisicao, 
                                resultado) 
                                VALUES
                                (@Chave_Idempotencia, 
                                @Requisicao, 
                                @Resultado);"
        ;

        public const string SQL_SELECT_ACCOUNT_BY_NUMBER = @"SELECT * 
                                FROM contacorrente 
                                WHERE numero = @Number;";

        public const string SQL_SELECT_ACCOUNT_BY_ID = @"SELECT * 
                                FROM contacorrente 
                                WHERE idcontacorrente IN (@IdAccount);";

        public const string SQL_SELECT_MOVIMENTS = @"SELECT * 
                                FROM movimento
                                WHERE idcontacorrente IN (@IdAccount);";

        public const string SQL_SELECT_IDEMPOTENT_BY_KEY = @"SELECT * 
                                FROM idempotencia
                                WHERE chave_idempotencia IN (@Chave_Idempotencia);";

    }
}
