using Base.Services.Helpers.ErrorHandler;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Services.Helpers.Validator
{
    public class CustomValidator<T> : AbstractValidator<T>
    {
        public void ValidateAndThrowCustomException(T x)
        {
            try
            {
                this.ValidateAndThrow(x);
            }
            catch (ValidationException e)
            {
                ThrowCustomException(e);
            }
        }

        public void ThrowCustomException(ValidationException ve)
        {
            if (ve.Errors.Count() == 1)
            {
                var e = ve.Errors.First();
                throw new AppException(e.ErrorCode + " - " + e.ErrorMessage);
            }
            else
            {
                throw new AppException("Validation Failed");
               
            }
        }
    }
}
