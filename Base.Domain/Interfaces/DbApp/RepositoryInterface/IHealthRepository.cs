using System.Threading.Tasks;

namespace Base.Domain.Interfaces.DbApp.RepositoryInterface
{
    public interface IHealthRepository
    {
        Task<bool> CanConnect();
    }
}