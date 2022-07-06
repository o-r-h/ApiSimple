using Base.Domain.Entities.DbApp;
using System.Linq;
using FluentValidation;
using Base.Domain.Models.Example;
using Base.Services.Helpers.Validator;

namespace Base.Services.DbAppValidators
{
    public class ExampleValidator: CustomValidator<ExampleModel>
    {
        public static decimal min = 0;
        public ExampleValidator(ExampleModel exampleModel)
        {
            RuleFor(c => c.PriceExample).Must((c, alfa) => MinPrice(exampleModel.PriceExample)).WithMessage("Price Example must be greather tan 0");
        }

        private bool MinPrice(decimal? price)
        {
            if (price == null)
                return true;

            if (price.Value <= min)
            {
                return false;
            }

            return true;

        }
    }
}
