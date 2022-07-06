using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Models.Register
{
    public class RegisterUser
    {

        public string Email { get; set; }
        public string CompanyRegisterNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyZipCode { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyEmail { get; set; }

    }
}
