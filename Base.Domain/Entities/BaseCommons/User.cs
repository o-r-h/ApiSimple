using System;
#nullable disable
namespace Base.Domain.Entities.BaseCommons
{
    public partial class User : BaseEntity
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    //    public long? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? RolId { get; set; }
        public long UserStatusId { get; set; }
      //  public int? AccessFailedCount { get; set; }
        public int? UserLocked { get; set; }
   //     public bool EmailConfirmed { get; set; }
   //     public Guid? RecoveryToken { get; set; }
   //     public DateTime? TokenExpiration { get; set; }
      

        public virtual Rol Rol { get; set; }
        public virtual UserStatus UserStatus { get; set; }
    }
}
