using Base.Domain.Models.User;
using System;
using System.Linq;
using FluentValidation;

namespace Base.Services.Validators
{
    public class UserRegisterValidator // : CustomValidator<SessionUserModel>
    {
        public static int minLength = 8;
        public static string uppers = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
        public static string lowers = "abcdefghijklmnñopqrstuvwxyz";
        public static string numbers = "1234567890";
        public static string symbols = "|°¬!¡%.:,;$#&()='?¿*¨´+~^`{}[]-_.\"/\\<>@";

        public UserRegisterValidator()
        {
            //RuleFor(u => u.Email).NotEmpty().WithErrorCode("email").WithMessage("The email is required");
            //RuleFor(u => u.Password).NotEmpty().WithErrorCode("password").WithMessage("The password is required");
            //RuleFor(u => u.Password).Must(p => p.Length >= minLength).WithErrorCode("password").WithMessage($"The password must be at least {minLength} characters long");
            //RuleFor(u => u.Password).Must(p => p.Any(c => uppers.Contains(c))).WithErrorCode("password").WithMessage("The password must contain at least one capital letter");
            //RuleFor(u => u.Password).Must(p => p.Any(c => lowers.Contains(c))).WithErrorCode("password").WithMessage("The password must contain at least one lowercase letter");
            //RuleFor(u => u.Password).Must(p => p.Any(c => numbers.Contains(c))).WithErrorCode("password").WithMessage("The password must contain at least one number");
            //RuleFor(u => u.Password).Must(p => p.Any(c => symbols.Contains(c))).WithErrorCode("password").WithMessage("The password must contain at least one symbol");
        }
    }
}
