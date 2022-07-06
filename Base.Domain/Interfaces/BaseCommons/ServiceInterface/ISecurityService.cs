using Base.Domain.Entities.BaseCommons;
using Base.Domain.Helpers;
using Base.Domain.Models.Register;
using Base.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Interfaces.BaseCommons.ServiceInterface
{
    public interface ISecurityService
    {
        Task<bool> AproveEmailIntivations(long userId);
        Task<long> DeleteUserAsync(long userId);
        string GenerateToken(SessionUserModel user);
        Task<string> GetUserEmailFromRecoveryToken(Guid token);
        Task<bool> RecoverPasswordFromEmail(string email);
        Task<SessionUserModel> RegisterNewUser(RegisterUser data);
        Task<bool> RejectedEmailInvitation(long userId);
        Task<bool> ResetPassword(UserResetPassword model);
        Task<List<Tuple<string, string>>> SendInviteMailAsync(string emailInvite);
        void SendRegisterMail(SessionUserModel user);
        Task<User> UpdateUserInfo(long userId, UserInfoUpdate data);
        Task<User> UpdateUserPassword(long userId, UserInfoPassword data);
        Task<SessionUserModel> ValidateLogin(LoginUser loginUser);
    }
}