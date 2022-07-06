using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Helpers.Encryptation
{
    public static class EncrpytationHelper
    {

        public static string Encrypt(string password)
        {
            using (var shaM = new SHA512Managed())
            {
                return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public static string TruncateCreditCardString(string creditCardNumber)
        {
            string result;
            result = "XXXX-" + creditCardNumber.Substring((creditCardNumber.Length - 5), 4);
            return result;
        }

        public static string TruncateExpirationDateCard(string expirationDate)
        {
            string result;
            result = "XX-" + expirationDate.Substring((expirationDate.Length - 2), 2);
            return result;
        }
    }
}
