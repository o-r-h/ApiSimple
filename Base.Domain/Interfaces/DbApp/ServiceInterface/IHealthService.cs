using System.Threading.Tasks;

namespace Base.Domain.Interfaces.DbApp.ServiceInterface
{
    public interface IHealthService
    {
        Task<bool> CanConnect();
    }
}