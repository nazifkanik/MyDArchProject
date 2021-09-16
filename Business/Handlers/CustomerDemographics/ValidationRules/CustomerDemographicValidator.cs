
using Business.Handlers.CustomerDemographics.Commands;
using FluentValidation;

namespace Business.Handlers.CustomerDemographics.ValidationRules
{

    public class CreateCustomerDemographicValidator : AbstractValidator<CreateCustomerDemographicCommand>
    {
        public CreateCustomerDemographicValidator()
        {
            RuleFor(x => x.CustomerDesc).NotEmpty();

        }
    }
    public class UpdateCustomerDemographicValidator : AbstractValidator<UpdateCustomerDemographicCommand>
    {
        public UpdateCustomerDemographicValidator()
        {
            RuleFor(x => x.CustomerDesc).NotEmpty();

        }
    }
}