
using Business.Handlers.Employees.Commands;
using FluentValidation;

namespace Business.Handlers.Employees.ValidationRules
{

    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.ReportsTo).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.TitleOfCourtesy).NotEmpty();
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.BirtHireDatehDate).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Region).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.HomePhone).NotEmpty();
            RuleFor(x => x.Extension).NotEmpty();
            RuleFor(x => x.Notes).NotEmpty();
            RuleFor(x => x.PhotoPath).NotEmpty();

        }
    }
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.ReportsTo).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.TitleOfCourtesy).NotEmpty();
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.BirtHireDatehDate).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Region).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.HomePhone).NotEmpty();
            RuleFor(x => x.Extension).NotEmpty();
            RuleFor(x => x.Notes).NotEmpty();
            RuleFor(x => x.PhotoPath).NotEmpty();

        }
    }
}