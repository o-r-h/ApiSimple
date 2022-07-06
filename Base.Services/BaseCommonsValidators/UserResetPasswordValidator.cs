using System.Linq;
using Base.Domain.Models.User;
using Base.Services.Validators;
using FluentValidation;


namespace Web.Service.Validators
{
    public class UserResetPasswordValidator  //: CustomValidator<UserResetPassword>
    {
        //public UserResetPasswordValidator()
        //{
        //    RuleFor(u => u.NewPassword).NotEmpty().WithErrorCode("password").WithMessage("The password is required");
        //    RuleFor(u => u.NewPassword).Must(p => p.Length >= UserRegisterValidator.minLength).WithErrorCode("password").WithMessage($"The password must be at least {UserRegisterValidator.minLength} characters long");
        //    RuleFor(u => u.NewPassword).Must(p => p.Any(c => UserRegisterValidator.uppers.Contains(c))).WithErrorCode("password").WithMessage("The password must contain at least one capital letter");
        //    RuleFor(u => u.NewPassword).Must(p => p.Any(c => UserRegisterValidator.lowers.Contains(c))).WithErrorCode("password").WithMessage("The password must contain at least one lowercase letter");
        //    RuleFor(u => u.NewPassword).Must(p => p.Any(c => UserRegisterValidator.numbers.Contains(c))).WithErrorCode("password").WithMessage("The password must contain at least one number");
        //    RuleFor(u => u.NewPassword).Must(p => p.Any(c => UserRegisterValidator.symbols.Contains(c))).WithErrorCode("password").WithMessage("The password must contain at least one symbol");
        //}
    }
}