
using Business.Handlers.CustomerCustomerDemoes.Commands;
using FluentValidation;

namespace Business.Handlers.CustomerCustomerDemoes.ValidationRules
{

    public class CreateCustomerCustomerDemoValidator : AbstractValidator<CreateCustomerCustomerDemoCommand>
    {
        public CreateCustomerCustomerDemoValidator()
        {
            RuleFor(x => x.CustomerTypeId).NotEmpty();

        }
    }
    public class UpdateCustomerCustomerDemoValidator : AbstractValidator<UpdateCustomerCustomerDemoCommand>
    {
        public UpdateCustomerCustomerDemoValidator()
        {
            RuleFor(x => x.CustomerTypeId).NotEmpty();

        }
    }
}