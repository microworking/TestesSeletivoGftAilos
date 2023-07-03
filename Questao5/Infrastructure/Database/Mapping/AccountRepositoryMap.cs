using Dapper.FluentMap.Dommel.Mapping;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Mapping
{
    public class AccountRepositoryMap : DommelEntityMap<Account>
    {
        public AccountRepositoryMap()
        {
            ToTable("contacorrente");
            Map(p => p.AccountId).ToColumn("idcontacorrente").IsKey();
            Map(p => p.AccountNumber).ToColumn("numero");
            Map(p => p.AccountOwnerName).ToColumn("nome");
            Map(p => p.IsActiveAccount).ToColumn("ativo");
        }
    }
}