using Base.Domain.Entities.BaseCommons;
using Base.Domain.Entities.Customs;
using Base.Domain.Helpers;
using Base.Domain.Helpers.Encryptation;
using Base.Domain.Helpers.Enums;
using Base.Domain.Interfaces;
using Base.Domain.Interfaces.BaseCommons.RepositoryInterface;
using Base.Domain.Interfaces.BaseCommons.ServiceInterface;
using Base.Domain.Models.Register;
using Base.Domain.Models.User;
using Base.Services.Helpers.ErrorHandler;
using Base.Services.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Service.Validators;

namespace Base.Services.BaseCommonsServices
{
    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly Session session;

        private static ApplicationConfiguration.ConfigurationEMail eMailConfiguration = ApplicationConfiguration.Current.EMail;

        public SecurityService(IUserRepository userRepository,
                          IConfiguration configuration,
                          Session session)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.session = session;
        }

        public async Task<SessionUserModel> ValidateLogin(LoginUser loginUser)
        {
            var response = new SessionUserModel();
         
              //  new LoginValidator().ValidateAndThrowCustomException(loginUser);
                var userEntity = await userRepository.GetByEmail(loginUser.Email);

                if (userEntity == null || userEntity.Password != Domain.Helpers.Encryptation.EncrpytationHelper.Encrypt(loginUser.Password))
                {
                    throw new AppException("Incorrect user or password");
                }

                response = new SessionUserModel
                {
                    UserId = userEntity.UserId,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    RolId = userEntity.RolId.Value,
                    CompanyId = 0 ,
                    CompanyName = "",
                    CompanyEmail = "",
                    RegisterNumber = ""
                };
                response.Token = GenerateToken(response);
         
    

            return response;
        }

        public async Task<SessionUserModel> RegisterNewUser(RegisterUser data)
        {
            var response = new SessionUserModel();
           

                User user = await userRepository.GetByEmail(data.Email);

                if (user != null)
                {
                    if (user.UserStatusId == (long)BaseEnums.UserStatus.PendingForAprove)
                    {
                        throw new AppException("Email already registered, pending for aprove!");
                    
                    }
                    throw new AppException("Email already registered");
                }

                User newUser = new()
                {
                    Password = Domain.Helpers.Encryptation.EncrpytationHelper.Encrypt(data.Password),
                    CompanyId = 0,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    EmailConfirmed = false,
                    RolId = (long)BaseEnums.RolBase.Admin,
                    AccessFailedCount = 0,
                    UserLocked = 0,
                    UserStatusId = (long)BaseEnums.UserStatus.Active,
                    CreatedAt = DateTime.Now,
                };
                long newUserId = await userRepository.Insert(newUser);

                response = new SessionUserModel
                {
                    UserId = newUserId,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    RolId = (long)BaseEnums.RolBase.Admin,
                    CompanyId = 0,
                    CompanyName = data.CompanyName,
                    CompanyEmail = data.CompanyEmail,
                    RegisterNumber = data.CompanyRegisterNumber
                };
                response.Token = GenerateToken(response);
                SendRegisterMail(response);
           

           
            return response;

        }

        public async Task<User> UpdateUserInfo(long userId, UserInfoUpdate data)
        {
            var response = new User();
           
                var userEntity = await userRepository.SelectByIdASync(userId);


                if (data.FirstName != null)
                    userEntity.FirstName = data.FirstName;

                if (data.LastName != null)
                    userEntity.LastName = data.LastName;


                if (data.Email != null)
                {
                    MailAddress address = new MailAddress(data.Email);
                    string host = address.Host;

                    userEntity.Email = data.Email;
                }


                response = await userRepository.UpdateASync(userId, userEntity);
           
            return response;
        }

        public async Task<User> UpdateUserPassword(long userId, UserInfoPassword data)
        {
            var response = new User();
           
                var userEntity = await userRepository.SelectByIdASync(userId);

                var CurrentPassword = EncrpytationHelper.Encrypt(data.CurrentPassword);
                var NewPassword = EncrpytationHelper.Encrypt(data.NewPassword);

                if (userEntity.Password != CurrentPassword)
                {
                    throw new AppException("The current password is invalid");
                    
                }

                userEntity.Password = NewPassword;
                response = await userRepository.UpdateASync(userId, userEntity);
          
          

            return response;
        }

        public async Task<List<Tuple<string, string>>> SendInviteMailAsync(string emailInvite)
        {

            List<Tuple<string, string>> listError = new();
            if (session.UserId == 0)
            {
                throw new AppException("not session was active");
                
            }



            if (String.IsNullOrEmpty(emailInvite))
            {
                throw new UnauthorizedAccessException("emailInvite can't be null or empty");
            }



            emailInvite = emailInvite.Replace(";", ",");

            //string[] Emails = emailInvite.Split(',');
            List<String> Emails = emailInvite.Split(',').ToList();
            List<string> list = new List<string>();
            foreach (var item in Emails)
            {
                User userByMail = await userRepository.GetByEmail(item);
                if (userByMail != null && userByMail.UserStatusId != (long)BaseEnums.UserStatus.PendingForAprove)
                {
                    listError.Add(new Tuple<string, string>("emailInvite", "email already registered"));
                }
                else
                {

                    try
                    {
                        User user = await userRepository.SelectByIdASync(session.UserId);
                        string urlParams = string.Format("?email={0}&numeroRegistro={1}", item, session.CompanyRegisterNumber);
                        var mailBody = System.IO.File.ReadAllText("assets/webtemplates/invite.html");
                        mailBody = mailBody
                            .Replace("[[EMAIL]]", session.UserEmail)
                            .Replace("[[USERNAME]]", string.Format("{0} {1}", user.FirstName, user.LastName))
                            .Replace("[[BUTTON_URL]]", $"{eMailConfiguration.HostUrl}/create-account" + urlParams)
                            .Replace("[[APPCOMMERCIALNAME]]", session.CompanyName);
                        EMail.ThrowWithTemplate(new Message
                        {
                            To = item,
                            Subject = "Invitation",
                            Body = mailBody
                        });

                    }
                    catch (Exception ex)
                    {
                        List<Tuple<string, string>> exError = new()
                        {
                            new Tuple<string, string>("UserId", ex.ToString())
                        };

                        listError.Add(new Tuple<string, string>("Exception", ex.Message));

                    }

                }
            }
          
            return listError;

        }

        public async Task<bool> RecoverPasswordFromEmail(string email)
        {
            var user = await userRepository.GetByEmail(email);
            if (user != null)
            {
                var token = Guid.NewGuid();
                var url = $"{eMailConfiguration.HostUrl}/reset-password?token={token}";
                await userRepository.UpdatePasswordRecoveryToken(user.UserId, token);
                SendForgotPasswordEmail(email, url);

                return true;
            }
            throw new TaskCanceledException("This email does not exist");
            
        }

        public async Task<string> GetUserEmailFromRecoveryToken(Guid token)
        {
            var user = await userRepository.GetByPasswordRecoveryToken(token);
            if (user != null)
            {
                if (user.TokenExpiration < DateTime.Now)
                {
                    throw new TaskCanceledException("The token has expired");
                }

                return user.Email;
            }

            throw new TaskCanceledException("This token is not valid");
        }

        public async Task<bool> ResetPassword(UserResetPassword model)
        {
          
              //  new UserResetPasswordValidator().ValidateAndThrowCustomException(model);
                var userEntity = await userRepository.GetByPasswordRecoveryToken(model.Token);

                if (userEntity == null)
                {
                throw new TaskCanceledException("This token is not valid");
            }

                if (userEntity.TokenExpiration < DateTime.Now)
                {
                throw new TaskCanceledException("The token has expired");
            }

                var newPass = Domain.Helpers.Encryptation.EncrpytationHelper.Encrypt(model.NewPassword);

                userEntity.Password = newPass;
                userEntity.TokenExpiration = DateTime.Now;
                return  await userRepository.UpdateASync(userEntity.UserId, userEntity) != null;
         
            
        }

        public async Task<bool> RejectedEmailInvitation(long userId)
        {
            
            {
                var userEntity = await userRepository.SelectByIdASync(userId);


                if (userEntity.UserStatusId != 3)
                {
                    throw new TaskCanceledException("Only rejected user invitation with status 'Pending for Aprove'");
                }

                return userRepository.RejectedEmailInvitation(userId) != null;

            }

        }

        public async Task<bool> AproveEmailIntivations(long userId)
        {
          
             var userEntity = await userRepository.SelectByIdASync(userId);

             if (userEntity.UserStatusId != 3)
             {
                   
                    throw new TaskCanceledException("Only rejected user invitation with status 'Pending for Aprove'");
             }
 
             return await userRepository.AproveEmailIntivations(userId) != null;
            
        }

        public async Task<long> DeleteUserAsync(long userId)
        {
            
                await userRepository.DeleteASync (userId);
                return userId;
                throw new TaskCanceledException("Only rejected user invitation with status 'Pending for Aprove'");
        }



        public string GenerateToken(SessionUserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, $"{user.UserId}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("CompanyId", $"{user.CompanyId}")
            };

            var keygen = configuration.GetSection("Security").GetSection("KeyEncryption").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keygen));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        private void SendForgotPasswordEmail(string email, string recoveryUrl)
        {
            var mailBody = System.IO.File.ReadAllText("assets/webtemplates/forgot_password.html");
            mailBody = mailBody
                .Replace("[[EMAIL]]", email)
                .Replace("[[URL]]", recoveryUrl)
                .Replace("[[APPCOMMERCIALNAME]]", session.CompanyName);


            EMail.ThrowWithTemplate(new Message
            {
                To = email,
                Subject = "Reset Password",
                Body = mailBody
            });
        }

        public void SendRegisterMail(SessionUserModel user)
        {
            var subjectName = configuration.GetSection("Application").GetSection("Name").Value;
            var mailBody = System.IO.File.ReadAllText("assets/webtemplates/welcome.html");
            mailBody = mailBody
                .Replace("[[BUTTON_URL]]", $"{eMailConfiguration.HostUrl}/login")
                .Replace("[[UPGRADE_URL]]", $"{eMailConfiguration.HostUrl}/")
                .Replace("[[APPCOMMERCIALNAME]]", $"{user.CompanyName}");

            EMail.ThrowWithTemplate(new Message
            {
                To = user.Email,
                Subject = string.Format("Welcome to {0}", subjectName),
                Body = mailBody
            });
        }








    }
}
