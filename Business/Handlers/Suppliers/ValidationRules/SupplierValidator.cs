
using Business.Handlers.Suppliers.Commands;
using FluentValidation;

namespace Business.Handlers.Suppliers.ValidationRules
{

    public class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.ContactName).NotEmpty();
            RuleFor(x => x.ContactTitle).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Region).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Fax).NotEmpty();
            RuleFor(x => x.HomePage).NotEmpty();

        }
    }
    public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.ContactName).NotEmpty();
            RuleFor(x => x.ContactTitle).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Region).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Fax).NotEmpty();
            RuleFor(x => x.HomePage).NotEmpty();

        }
    }
}