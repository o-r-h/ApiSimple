using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Helpers.Enums
{
     public static class BaseEnums
    {

        public enum RegisterTypeOptions
        {
            NewUserNewCompany = 1,
            NewUserInvited = 2,
            NewUserPendingForAprove = 3, 
            NoAllowed = 4
        }


        public enum CompanyStatus
        {
            Active = 1,
            Inactive = 2
        }

        public enum RolBase
        {
            Admin = 1,
            NormalUser = 2,
        }

        public enum UserStatus
        {
            Active = 1,
            Inactive = 2,
            PendingForAprove = 3
        }

    }
}
