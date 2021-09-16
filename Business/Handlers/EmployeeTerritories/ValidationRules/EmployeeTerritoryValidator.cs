
using Business.Handlers.EmployeeTerritories.Commands;
using FluentValidation;

namespace Business.Handlers.EmployeeTerritories.ValidationRules
{

    public class CreateEmployeeTerritoryValidator : AbstractValidator<CreateEmployeeTerritoryCommand>
    {
        public CreateEmployeeTerritoryValidator()
        {
            RuleFor(x => x.TerritoryId).NotEmpty();

        }
    }
    public class UpdateEmployeeTerritoryValidator : AbstractValidator<UpdateEmployeeTerritoryCommand>
    {
        public UpdateEmployeeTerritoryValidator()
        {
            RuleFor(x => x.TerritoryId).NotEmpty();

        }
    }
}