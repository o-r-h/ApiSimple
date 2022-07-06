using Base.Domain.Helpers;
using Base.Domain.Interfaces.BaseCommons.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSimple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Session session;
        private readonly IUserService userService;
        public UserController(IUserService userService, Session session)
        {
            this.userService = userService;
            this.session = session;
        }

        

        [HttpGet("SendMailTest")]
        public ActionResult SendMailTest()
        {
             userService.SendMailTest();
            return Ok(0);
        }






       
    }
}
