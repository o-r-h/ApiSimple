using Base.Domain.Interfaces.DbApp.RepositoryInterface;
using Base.Infrastructure.Context;
using System.Threading.Tasks;

namespace Base.Infrastructure.Repositories.DbApp
{
    public class HealthRepository : IHealthRepository
    {
        protected readonly DbAppContext dbAppContext;

        public HealthRepository(DbAppContext dbAppContext)
        {
            this.dbAppContext = dbAppContext;
        }

        public async Task<bool> CanConnect()
        {
            return await dbAppContext.Database.CanConnectAsync();

        }
    }
}
