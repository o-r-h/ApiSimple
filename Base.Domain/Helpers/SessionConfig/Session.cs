using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace Base.Domain.Helpers
{
    public class Session
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private IEnumerable<Claim> Claims => httpContextAccessor.HttpContext.User?.Claims;
        public int UserId => Convert.ToInt32(Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value);
        public string UserEmail => Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        public long CompanyID => Convert.ToInt64(Claims?.FirstOrDefault(x => x.Type == "CompanyId")?.Value);
        public string CompanyName => Claims?.FirstOrDefault(x => x.Type == "CompanyName")?.Value;
        public string CompanyEmail => Claims?.FirstOrDefault(x => x.Type == "CompanyEmail")?.Value;
        public string CompanyRegisterNumber => Claims?.FirstOrDefault(x => x.Type == "RegisterNumber")?.Value;
        public string ClientIP => httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
        public string RequestURL => httpContextAccessor.HttpContext.Request.Host.Value;

        public Session(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
