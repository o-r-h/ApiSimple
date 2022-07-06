﻿using System.Collections.Generic;
#nullable disable
namespace Base.Domain.Entities.BaseCommons

{
    public partial class Rol
    {
        public Rol()
        {
            Users = new HashSet<User>();
        }

        public long RolId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
