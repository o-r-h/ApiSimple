using System.Collections.Generic;
#nullable disable
namespace Base.Domain.Entities.BaseCommons
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            Users = new HashSet<User>();
        }

        public long UserStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
