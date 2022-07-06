using Base.Domain.Interfaces.DbApp.RepositoryInterface;
using Base.Domain.Interfaces.DbApp.ServiceInterface;
using System.Threading.Tasks;

namespace Base.Services.DbAppServices
{
    public class HealthService : IHealthService
    {
        private readonly IHealthRepository healthRepository;

        public HealthService(IHealthRepository healthRepository)
        {
            this.healthRepository = healthRepository;
        }

        public async Task<bool> CanConnect()
        {
            return await healthRepository.CanConnect();

        }
    }
}
