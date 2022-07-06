using Base.Domain.Entities.BaseCommons;
using System.Threading.Tasks;

namespace Base.Domain.Interfaces.BaseCommons.ServiceInterface
{
    public interface IUserService
    {
        Task<User> GetUser(long userId);
        void SendMailTest();


    }
}