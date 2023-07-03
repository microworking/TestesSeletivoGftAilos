using Microsoft.Data.Sqlite;
using Dommel;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class AccountMovementRepository : IAccountMovementRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public AccountMovementRepository(DatabaseConfig DatabaseConfig) => _databaseConfig = DatabaseConfig;

        public string DoAccountDebit(string AccountId, double Value)
        {
            AccountMovement accountMovement = new()
            {
                MovementId = Guid.NewGuid().ToString().ToUpper(),
                AccountId = AccountId.ToString(),
                MovementDate = DateTime.Today.ToString("dd/MM/yyyy"),
                MovementType = 'D',
                MovementValue = Value
            };

            using var connection = new SqliteConnection(_databaseConfig.Name);
            string? movementRequestIdentifier = connection.Insert<AccountMovement>(accountMovement).ToString();

            return movementRequestIdentifier ?? string.Empty;
        }

        // TODO: a chave não está sendo inserida na coluna idmovimento da tabela movimento
        // Executando a instrução INSERT com os mesmos exatos valores pela extensão SQLite
        // no VSCode o problema não ocorre. Pode ser um problema mo FluentMap do Dommel ou
        // no próprio SQLite. A coluna idmovimento é uma PK porém está aceitando NULL.
        // Exemplo de GUID passado aqui pelo membro MovementId do DTO AccountMovement:
        // B6BAFC09-6967-ED11-A567-055DFA4A16C9
        public string DoAccountCredit(string AccountId, double Value)
        {
            AccountMovement accountMovement = new()
            {
                MovementId = Guid.NewGuid().ToString().ToUpper(),
                AccountId = AccountId.ToString(),
                MovementDate = DateTime.Today.ToString("dd/MM/yyyy"),
                MovementType = 'C',
                MovementValue = Value
            };

            using var connection = new SqliteConnection(_databaseConfig.Name);
            string? movementRequestIdentifier = connection.Insert<AccountMovement>(accountMovement).ToString();

            //return movementRequestIdentifier ?? string.Empty;
            return accountMovement.MovementId;
        }
    }
}