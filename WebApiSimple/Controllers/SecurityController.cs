using Base.Domain.Helpers;
using Base.Domain.Interfaces.BaseCommons.ServiceInterface;
using Base.Domain.Models.Register;
using Base.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApiSimple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SecurityController : ControllerBase
    {
        private readonly Session session;
        private readonly IUserService userService;
        private readonly ISecurityService securityService;
        public SecurityController(IUserService userService, Session session, ISecurityService securityService)
        {
            this.userService = userService;
            this.securityService = securityService;
            this.session = session;
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {
            var response = await securityService.ValidateLogin(loginUser);
            return Ok(response);
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            var response = await securityService.RegisterNewUser(registerUser);
            return Ok(response);
        }

        [HttpPost("updatePassword")]
        public async Task<ActionResult> UpdatePassword(UserInfoPassword userInfoPassword)
        {
            var response = await securityService.UpdateUserPassword(session.UserId, userInfoPassword);
            return Ok(response);
        }

        
        [HttpPost("[action]/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult> CheckPasswordRecoveryToken(Guid token)
        {
            var response = await securityService.GetUserEmailFromRecoveryToken(token);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(UserResetPassword model)
        {
            var response = await securityService.ResetPassword(model);
            return Ok(response);
        }



        [HttpPost("RejectedEmailInvitation")]
        public async Task<ActionResult> RejectedEmailInvitation(long userId)
        {
            var response = await securityService.RejectedEmailInvitation(userId);
            return Ok(response);
        }

        [HttpPost("AproveEmailIntivations")]
        public async Task<ActionResult> AproveEmailIntivations(long userId)
        {
            var response = await securityService.AproveEmailIntivations(userId);
            return Ok(response);
        }


        //[HttpDelete("{userId}")]
        //public async Task<ActionResult> DeleteAsync([FromRoute] long userId)
        //{
        //    var result = await securityService.DeleteUserAsync(userId);
        //    return StatusCode((int)result.StatusCode, result.Current);
        //}

        //[HttpPost("SendInviteMail")]
        //public async Task<ActionResult> PutAsync(string email)
        //{
        //    var response = await securityService.SendInviteMailAsync(email);
        //    return StatusCode((int)response.StatusCode, response.Current);
        //}
    }
}
