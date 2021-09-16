
using Business.Handlers.Territories.Commands;
using FluentValidation;

namespace Business.Handlers.Territories.ValidationRules
{

    public class CreateTerritoryValidator : AbstractValidator<CreateTerritoryCommand>
    {
        public CreateTerritoryValidator()
        {
            RuleFor(x => x.RegionId).NotEmpty();
            RuleFor(x => x.TerritoryDescription).NotEmpty();

        }
    }
    public class UpdateTerritoryValidator : AbstractValidator<UpdateTerritoryCommand>
    {
        public UpdateTerritoryValidator()
        {
            RuleFor(x => x.RegionId).NotEmpty();
            RuleFor(x => x.TerritoryDescription).NotEmpty();

        }
    }
}