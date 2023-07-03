using Dapper.FluentMap.Dommel.Mapping;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Mapping
{
    public class AccountMovementRepositoryMap : DommelEntityMap<AccountMovement>
    {
        public AccountMovementRepositoryMap()
        {
            ToTable("movimento");
            Map(p => p.MovementId).ToColumn("idmovimento").IsKey();
            Map(p => p.AccountId).ToColumn("idcontacorrente");
            Map(p => p.MovementDate).ToColumn("datamovimento");
            Map(p => p.MovementType).ToColumn("tipomovimento");
            Map(p => p.MovementValue).ToColumn("valor");
        }
    }
}