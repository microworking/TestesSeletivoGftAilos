using Dapper;
using Dommel;
using Dapper.FluentMap.Dommel;
using Dapper.FluentMap.Mapping;
using Dapper.FluentMap.Dommel.Mapping;

namespace Questao5.Infrastructure.Database.Mapping
{
    public class SqlBuilder : Dommel.ISqlBuilder
    {
        public SqlBuilder() {  }

        public string BuildInsert(Type type, string tableName, string[] columnNames, string[] paramNames)
        {
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2}); SELECT LAST_INSERT_ROWID() idmovimento",
                tableName,
                string.Join(", ", columnNames),
                string.Join(", ", paramNames));
        }

        public string BuildPaging(string? orderBy, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public string LimitClause(int count)
        {
            throw new NotImplementedException();
        }

        public string PrefixParameter(string paramName)
        {
            throw new NotImplementedException();
        }

        public string QuoteIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }
    }
}
