
using Business.Handlers.Shippers.Commands;
using FluentValidation;

namespace Business.Handlers.Shippers.ValidationRules
{

    public class CreateShipperValidator : AbstractValidator<CreateShipperCommand>
    {
        public CreateShipperValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();

        }
    }
    public class UpdateShipperValidator : AbstractValidator<UpdateShipperCommand>
    {
        public UpdateShipperValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();

        }
    }
}