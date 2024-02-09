using Base.Domain.Entities.BaseCommons;
using Base.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Domain.Interfaces.BaseCommons.RepositoryInterface
{
    public interface IUserRepository
    {
        Task<long> Insert(User data);
        Task<User> ChangeUserStatusASync(long rolid, long userStatusId);
        Task DeleteASync(long rolid);
        Task<User> GetByEmail(string email);
        Task<List<User>> GetByIdList(List<long> ids);
        Task<List<User>> SelectAll();
        Task<User> SelectByIdASync(long rolid);
        Task<User> UpdateASync(long userId, User obj);
        Task<User> RejectedEmailInvitation(long userId);
        Task<User> AproveEmailIntivations(long userId);
        Task InactiveUser(long userId);
    }
}