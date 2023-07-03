using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public AccountRepository(DatabaseConfig DatabaseConfig) => _databaseConfig = DatabaseConfig;

        public Account GetAccount(int AccountId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            Account account = connection.QueryFirstOrDefault<Account>($"SELECT idcontacorrente, numero, nome, ativo FROM contacorrente WHERE numero = {AccountId};");
            return account;
        }

        public List<AccountMovement> GetBalance(int AccountId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            IEnumerable<AccountMovement> accountMovement = connection.Query<AccountMovement>($"SELECT M.idmovimento, M.idcontacorrente, M.datamovimento, M.tipomovimento, M.valor FROM contacorrente C, movimento M WHERE C.numero = {AccountId} AND M.idcontacorrente = C.idcontacorrente;");
            return accountMovement.ToList();
        }
    }
}