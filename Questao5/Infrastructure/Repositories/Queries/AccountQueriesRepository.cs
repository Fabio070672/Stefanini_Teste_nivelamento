using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Database.SqlStatements;
using Questao5.Infrastructure.Services;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories.Queries
{
    public class AccountQueriesRepository : IAccountQueriesRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public AccountQueriesRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<ConsultIdemPotentResponse> ConsultIdemPotentMovimentAsync(ConsultIdemPotentRequest request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                {
                    var idemPotencia = (await connection.QueryAsync<IdemPotencia>(
                        AccountStatements.SQL_SELECT_IDEMPOTENT_BY_KEY,
                        new { Chave_Idempotencia = request.IdIdemPotent.ToString() })).FirstOrDefault();

                    ConsultIdemPotentResponse? response = idemPotencia is null ? null : ConsultIdemPotentResponse.ConvertTo(idemPotencia);

                    return response!;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<ConsultAccountResponse> ConsultAccountAsync(int numero)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                {
                    var conta = (await connection.QueryAsync<ContaCorrente>(
                        AccountStatements.SQL_SELECT_ACCOUNT_BY_NUMBER,
                        new { Number = numero.ToString() })).FirstOrDefault();

                    ConsultAccountResponse? response = conta is null ? null : ConsultAccountResponse.ConvertTo(conta);

                    return response!;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<ConsultAccountResponse> ConsultAccountAsync(Guid idContaCorrente)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                {
                    var conta = (await connection.QueryAsync<ContaCorrente>(
                       AccountStatements.SQL_SELECT_ACCOUNT_BY_ID,
                       new { IdAccount = idContaCorrente.ToString() })).FirstOrDefault();

                    ConsultAccountResponse? response = conta is null ? null : ConsultAccountResponse.ConvertTo(conta);

                    return response!;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<ConsultBalanceQueryResponse> ConsultBalanceAsync(ConsultBalanceQueryRequest request)
        {
            try
            {
                using var connection = new SqliteConnection(databaseConfig.Name);
                {
                    List<Movimento> movimentos = (await connection.QueryAsync<Movimento>(
                        AccountStatements.SQL_SELECT_MOVIMENTS,
                        new { IdAccount = request.IdAccount }))
                        .ToList();

                    decimal credits = AccountServices.SumMoviments(movimentos, MovimentType.Credito);
                    decimal debits = AccountServices.SumMoviments(movimentos, MovimentType.Debito);

                    return new ConsultBalanceQueryResponse { ValueBalance = credits - debits };
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
