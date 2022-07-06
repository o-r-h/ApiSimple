using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Models.User
{
    public class UserModels
    {
    }

    public class UserFilter
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long UserStatusId { get; set; }
    
    }

    public class UserList
    {

        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Fullname
        {
            get { return string.Format("{0}, {1}", this.FirstName, this.LastName); }
        }
        public long? RolId { get; set; }
        public string RolIdName { get; set; }
        public long UserStatusId { get; set; }
        public string UserStatusIdName { get; set; }
        public int? UserLocked { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }


    public class SessionUserModel
    {
        public long UserId { get; set; }
        public long RolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long CompanyId { get; set; }
        public string RegisterNumber { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string Password { get; set; }
        public long UserStatusId { get; set; }
        public string Token { get; set; }
    }

    public class UserResetPassword
    {
        public Guid Token { get; set; }
        public string NewPassword { get; set; }
    }


    public class UserInfoPassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }


    public class UserInfoUpdate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }

    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
