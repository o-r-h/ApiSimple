using Base.Domain.Entities.BaseCommons;
using Base.Domain.Entities.Customs;
using Base.Domain.Helpers;
using Base.Domain.Interfaces.BaseCommons.RepositoryInterface;
using Base.Domain.Interfaces.BaseCommons.ServiceInterface;
using Base.Services.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;


namespace Base.Services.BaseCommonsServices
{
    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository  userRepository;
        private readonly Session session;
        
        private static ApplicationConfiguration.ConfigurationEMail eMailConfiguration = ApplicationConfiguration.Current.EMail;

     
        public UserService( IConfiguration configuration,
                             IUserRepository userRepository,
                            Session session)
        {

            this.configuration = configuration;
            this.userRepository = userRepository;
            this.session = session;
        }


        public async Task<User> GetUser(long userId)
        {
            var userEntity = await userRepository.SelectByIdASync(userId);
            return userEntity;
        }



        public void SendMailTest()
        {
            var mailBody = System.IO.File.ReadAllText("assets/webtemplates/invite.html");
            mailBody = mailBody
                .Replace("[[EMAIL]]", "omar.jesus.rodriguez@gmail.com")
                .Replace("[[NAME]]", string.Format("{0} {1}", "Omar", "Rodriguez"))
                .Replace("[[BUTTON_URL]]", $"{eMailConfiguration.HostUrl}/create-account" + "Abc");
            EMail.ThrowWithTemplate(new Message
            {
                To = "Omar.jesus.rodriguez@gmail.com",
                Subject = "Invitation",
                Body = mailBody
            });
           
        }

    }
}
