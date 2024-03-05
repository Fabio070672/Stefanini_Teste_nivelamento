using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Utils;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.SqlStatements;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories.Commands
{
    public class AccountCommandsRepository : IAccountCommandsRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public AccountCommandsRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task AddIdemPotentMovimentAsync(IdemPotentMovimentRequest request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                {
                    await connection.ExecuteAsync(AccountStatements.SQL_INSERT_IDEMPOTENT_MOVIMENT,
                        new
                        {
                            Chave_Idempotencia = request.Chave_IdemPotencia.ToString(),
                            Requisicao = request.Request,
                            Resultado = request.Result
                        });
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Guid> MovimentAccountAsync(MovimentAccountCommandRequest request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                {
                    var idMoviment = Guid.NewGuid();
                    char movimentType = MovimentTypeConvert.ConvertToChar(request.MovimetType);
                    await connection.ExecuteAsync(AccountStatements.SQL_INSERT_MOVIMENT,
                        new
                        {
                            IdMoviment = idMoviment.ToString(),
                            IdAccount = request.IdAccount,
                            MovimentType = movimentType,
                            Value = request.Value
                        });

                    return idMoviment;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
