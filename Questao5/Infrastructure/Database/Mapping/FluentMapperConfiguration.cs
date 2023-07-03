using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace Questao5.Infrastructure.Database.Mapping
{
    public class FluentMapperConfiguration
    {
        public static void Initialize()
        {
            FluentMapper.Initialize(options => {
                options.AddMap(Activator.CreateInstance<AccountRepositoryMap>());
                options.AddMap(Activator.CreateInstance<AccountMovementRepositoryMap>());
                options.ForDommel();
            });
        }
    }
}